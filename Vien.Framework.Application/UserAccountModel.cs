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
            throw new NotImplementedException();
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        protected override void MapEntityToCustomProperties(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override void Validate(ref ValidationErrors validationErrors)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateDelete(ref ValidationErrors validationErrors)
        {
            throw new NotImplementedException();
        }
        
        public bool IsNewRecord()
        {
            return Id == 0;
        }
    }
}
