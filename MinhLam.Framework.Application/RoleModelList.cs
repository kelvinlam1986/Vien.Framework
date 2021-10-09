using MinhLam.Framework.Data.Repo;
using System;
using System.Linq;

namespace MinhLam.Framework.Application
{
    [Serializable()]
    public class RoleModelList : BaseModelList<RoleModel>
    {
        public override void Load()
        {
            var repo = new RoleData();
            var roles = repo.GetAll();
            foreach (var userAccount in roles)
            {
                var roleModel = new RoleModel();
                roleModel.MapEntityToProperties(userAccount);
                this.Add(roleModel);
            }
        }

        public RoleModel GetById(int id)
        {
            return this.SingleOrDefault(x => x.Id == id);
        }

        public void LoadByAccountId(int accountId)
        {
            var repo = new RoleData();
            var roles = repo.GetByUserAccountId(accountId);
            foreach (var userAccount in roles)
            {
                var roleModel = new RoleModel();
                roleModel.MapEntityToProperties(userAccount);
                this.Add(roleModel);
            }
        }
    }
}
