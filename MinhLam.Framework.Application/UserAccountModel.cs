using System;
using MinhLam.Framework.Data;
using MinhLam.Framework.Data.Entities;
using MinhLam.Framework.Data.Repo;

namespace MinhLam.Framework.Application
{
    [Serializable()]
    public class UserAccountModel : BaseModel
    {
        public int Id { get; set; }
        public string WindowAccountName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public RoleModelList Roles { get; set; }

        public UserAccountModel()
        {
            Roles = new RoleModelList();
        }

        public override void Init()
        {
            IsActive = true;
        }

        public override bool Load(object id)
        {
            try
            {
                var repo = new UserAccountData();
                int key = Convert.ToInt32(id);
                var account = repo.GetById(key);
                if (account != null)
                {
                    MapEntityToProperties(account);
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
                        var userAccount = new UserAccount();
                        userAccount.WindowAccountName = WindowAccountName;
                        userAccount.FullName = FullName;
                        userAccount.Email = Email;
                        userAccount.IsActive = IsActive;
                        userAccount.CreatedBy = userName;
                        userAccount.CreatedDate = DateTime.Now;
                        userAccount.UpdatedBy = userName;
                        userAccount.UpdatedDate = DateTime.Now;
                        var id = new UserAccountData().Insert(userAccount);
                        Id = id;
                    }
                    else
                    {
                        var userAccount = new UserAccountData().GetById(Id);
                        userAccount.WindowAccountName = WindowAccountName;
                        userAccount.FullName = FullName;
                        userAccount.Email = Email;
                        userAccount.IsActive = IsActive;
                        userAccount.UpdatedBy = userName;
                        userAccount.UpdatedDate = DateTime.Now;
                        new UserAccountData().Update(userAccount);
                        Id = userAccount.Id;
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
            var repo = new UserAccountData();
            var userAccount = repo.GetById(Id);
            new UserAccountData().Remove(userAccount);
        }

        protected override string GetDisplayText()
        {
            return WindowAccountName;
        }

        protected override void MapEntityToCustomProperties(BaseEntity entity)
        {
            UserAccount userAccount = (UserAccount)entity;
            Id = userAccount.Id;
            WindowAccountName = userAccount.WindowAccountName;
            FullName = userAccount.FullName;
            Email = userAccount.Email;
            IsActive = userAccount.IsActive;
        }

        protected override void Validate(ref ValidationErrors validationErrors)
        {
            if (string.IsNullOrEmpty(WindowAccountName))
            {
                validationErrors.Add("Bạn phải nhập tên tài khoản");
            }

            if (string.IsNullOrEmpty(FullName))
            {
                validationErrors.Add("Bạn phải nhập họ tên");
            }

            if (string.IsNullOrEmpty(Email))
            {
                validationErrors.Add("Bạn phải nhập địa chỉ email");
            }


            if (new UserAccountData().CheckExistingName(Id, WindowAccountName))
            {
                validationErrors.Add("Tên tài khoản của bạn đã tồn tại");
            }
        }

        protected override void ValidateDelete(ref ValidationErrors validationErrors)
        {
            if (new UserAccountData().CheckExisting(Id) == false)
            {
                validationErrors.Add("Tài khoản này chưa tồn tại");
            }

            if (new RoleUserAccountData().CheckAccountInUse(Id))
            {
                validationErrors.Add("Tài khoản này đã được sử dụng bạn không thể xóa.");
            }
        }

        protected override bool IsNewRecord()
        {
            return Id == 0;
        }

        public RoleCapabilityModel.CapabilityAccessFlagEnum GetAccess(int capabilityId, RoleModelList currentRoles)
        {
            RoleCapabilityModel.CapabilityAccessFlagEnum accessFlag = RoleCapabilityModel.CapabilityAccessFlagEnum.None;
            foreach (var role in Roles)
            {
                var roleWithCapabilities = currentRoles.GetById(role.Id);
                foreach (var capability in roleWithCapabilities.RoleCapabilities)
                {
                    if (capability.Id == capabilityId)
                    {
                        if (capability.AccessFlag == RoleCapabilityModel.CapabilityAccessFlagEnum.Edit)
                        {
                            return RoleCapabilityModel.CapabilityAccessFlagEnum.Edit;
                        }
                        else if (capability.AccessFlag == RoleCapabilityModel.CapabilityAccessFlagEnum.ReadOnly)
                        {
                            accessFlag = RoleCapabilityModel.CapabilityAccessFlagEnum.ReadOnly;
                        }
                    }
                }
            }

            return accessFlag;
        }
    }
}
