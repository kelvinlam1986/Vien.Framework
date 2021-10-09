using System.Collections.Generic;
using System.Linq;
using MinhLam.Framework.Data.Entities;
using MinhLam.Framework.Data.Repo;

namespace MinhLam.Framework.Application
{
    public class UserAccountModelList : BaseModelList<UserAccountModel>
    {
        public override void Load()
        {
            var repo = new UserAccountData();
            var userAccounts = repo.GetAll();
            foreach (var userAccount in userAccounts)
            {
                var userAccountModel = new UserAccountModel();
                userAccountModel.MapEntityToProperties(userAccount);
                this.Add(userAccountModel);
            }
        }

        public void Search(string keyword)
        {
            List<UserAccount> userAccounts = new UserAccountData().Search(keyword);
            foreach (var userAccount in userAccounts)
            {
                var userAccountModel = new UserAccountModel();
                userAccountModel.MapEntityToProperties(userAccount);
                this.Add(userAccountModel);
            }
        }

        public void LoadWithRoles()
        {
            Load();
            foreach (var user in this)
            {
                user.Roles.LoadByAccountId(user.Id);
            }
        }

        public UserAccountModel LoadByWindowAccountName(string windowAccountName)
        {
            return this.SingleOrDefault(x => x.WindowAccountName.ToUpper() == windowAccountName.ToUpper());
        }
    }
}
