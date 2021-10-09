using MinhLam.Framework.Data.Repo;
using System;
using System.Linq;

namespace MinhLam.Framework.Application
{
    [Serializable()]
    public class RoleUserAccountModelList : BaseModelList<RoleUserAccountModel>
    {
        public override void Load()
        {
            var repo = new RoleUserAccountData();
            var roleAccounts = repo.GetAll();
            foreach (var roleAccount in roleAccounts)
            {
                var roleAccountModel = new RoleUserAccountModel();
                roleAccountModel.MapEntityToProperties(roleAccount);
                this.Add(roleAccountModel);
            }
        }

        public RoleUserAccountModel GetByAccountId(int accountId)
        {
            return this.SingleOrDefault(x => x.AccountId == accountId);
        }

        public bool IsUserInRole(int accountId)
        {
            var userRole = GetByAccountId(accountId);
            return userRole != null;
        }

        public void LoadByRoleId(int roleId)
        {
            var repo = new RoleUserAccountData();
            var roleAccounts = repo.GetByRoleId(roleId);
            foreach (var roleAccount in roleAccounts)
            {
                var roleAccountModel = new RoleUserAccountModel();
                roleAccountModel.MapEntityToProperties(roleAccount);
                this.Add(roleAccountModel);
            }
        }

        public bool Save(ref ValidationErrors validationErrors, string username)
        {
            foreach (var userRole in this)
            {
                if (userRole.Save(ref validationErrors, username) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
