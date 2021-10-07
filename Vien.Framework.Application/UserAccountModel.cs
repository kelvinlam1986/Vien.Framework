using System;
using Vien.Framework.Data;
using Vien.Framework.Data.Entities;
using Vien.Framework.Data.Repo;

namespace Vien.Framework.Application
{
    public class UserAccountModel : BaseModel
    {
        public int Id { get; set; }
        public string WindowAccountName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

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
                var menuItem = repo.GetById(key);
                if (menuItem != null)
                {
                    MapEntityToProperties(menuItem);
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
                        new UserAccountData().Insert(userAccount);
                        Id = userAccount.Id;
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
            return "WindowAccountName";
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
        }
        
        public bool IsNewRecord()
        {
            return Id == 0;
        }
    }
}
