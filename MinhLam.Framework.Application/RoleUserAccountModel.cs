using MinhLam.Framework.Data;
using MinhLam.Framework.Data.Entities;
using MinhLam.Framework.Data.Repo;
using System;

namespace MinhLam.Framework.Application
{
    public class RoleUserAccountModel : BaseModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int AccountId { get; set; }

        public override void Init()
        {
        }

        public override bool Load(object id)
        {
            try
            {
                var repo = new RoleUserAccountData();
                int key = Convert.ToInt32(id);
                var roleUserAccount = repo.GetById(key);
                if (roleUserAccount != null)
                {
                    MapEntityToProperties(roleUserAccount);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool Save(ref ValidationErrors validationErrors, string userName)
        {
            try
            {
                Validate(ref validationErrors);
                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        var roleUserAccount = new RoleUserAccount();
                        roleUserAccount.RoleId = RoleId;
                        roleUserAccount.AccountId = AccountId;
                        var id = new RoleUserAccountData().Insert(roleUserAccount);
                        Id = id;
                    }
                    else
                    {
                        var roleUserAccount = new RoleUserAccountData().GetById(Id);
                        roleUserAccount.RoleId = RoleId;
                        roleUserAccount.AccountId = AccountId;
                        new RoleUserAccountData().Update(roleUserAccount);
                        Id = roleUserAccount.Id;
                    }

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected override void DeleteForReal()
        {
            var repo = new RoleUserAccountData();
            var roleUserAccount = repo.GetById(Id);
            new RoleUserAccountData().Remove(roleUserAccount);
        }

        protected override string GetDisplayText()
        {
            return "";
        }

        protected override bool IsNewRecord()
        {
            return Id == 0;
        }

        protected override void MapEntityToCustomProperties(BaseEntity entity)
        {
            RoleUserAccount roleUserAccount = (RoleUserAccount)entity;
            Id = roleUserAccount.Id;
            RoleId = roleUserAccount.RoleId;
            AccountId = roleUserAccount.AccountId;
        }

        protected override void Validate(ref ValidationErrors validationErrors)
        {
            if (AccountId == 0)
            {
                validationErrors.Add("Bạn phải chọn một tài khoản");
            }

            if (RoleId == 0)
            {
                validationErrors.Add("Bạn phải chọn một vai trò");
            }

            if (validationErrors.Count > 0)
            {
                return;
            }

            if (new UserAccountData().CheckExisting(AccountId) == false)
            {
                validationErrors.Add("Tài khoản bạn chọn không tồn tại trong hệ thống");
            }

            if (new RoleData().CheckExisting(RoleId) == false)
            {
                validationErrors.Add("Vai trò bạn chọn không tồn tại trong hệ thống");
            }
        }

        protected override void ValidateDelete(ref ValidationErrors validationErrors)
        {
            if (new RoleUserAccountData().CheckExisting(Id) == false)
            {
                validationErrors.Add("Dữ liệu không tồn tại trong hệ thống.");
            }
        }
    }
}
