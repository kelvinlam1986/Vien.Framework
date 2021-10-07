using Vien.Framework.Data.Repo;

namespace Vien.Framework.Application
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
    }
}
