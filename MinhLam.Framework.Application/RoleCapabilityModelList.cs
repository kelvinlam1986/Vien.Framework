using MinhLam.Framework.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinhLam.Framework.Application
{
    [Serializable()]
    public class RoleCapabilityModelList : BaseModelList<RoleCapabilityModel>
    {
        public override void Load()
        {
            var repo = new RoleCapabilityData();
            var roleCapabilities = repo.GetAll();
            foreach (var roleCapability in roleCapabilities)
            {
                var roleCapabilityModel = new RoleCapabilityModel();
                roleCapabilityModel.MapEntityToProperties(roleCapability);
                this.Add(roleCapabilityModel);
            }
        }

        public List<RoleCapabilityModel> GetListByMenuItemId(int menuItemId)
        {
            return this.Where(x => x.Capability.MenuItemId == menuItemId).ToList();
        }

        public RoleCapabilityModel GetByCapabilityId(int capabilityId)
        {
            return this.SingleOrDefault(x => x.CapabilityId == capabilityId);
        }

        public void LoadByRoleId(int roleId)
        {
            var repo = new RoleCapabilityData();
            var roleCapabilities = repo.GetByRoleId(roleId);
            foreach (var roleCapability in roleCapabilities)
            {
                var roleCapabilityModel = new RoleCapabilityModel();
                roleCapabilityModel.MapEntityToProperties(roleCapability);
                this.Add(roleCapabilityModel);
            }
        }

        public bool Save(ref ValidationErrors validationErrors, string username)
        {
            foreach (var capability in this)
            {
                if (capability.Save(ref validationErrors, username) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
