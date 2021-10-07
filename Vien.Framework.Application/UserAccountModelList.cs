using System.Collections.Generic;
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
    }
}
