using MinhLam.Framework.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinhLam.Framework.Application
{
    [Serializable()]
    public class CapabilityModelList : BaseModelList<CapabilityModel>
    {
        public override void Load()
        {
            var repo = new CapabilityData();
            var capabilities = repo.GetAll();
            foreach (var capability in capabilities)
            {
                var capabilityModel = new CapabilityModel();
                capabilityModel.MapEntityToProperties(capability);
                this.Add(capabilityModel);
            }
        }

        public CapabilityModel GetByName(string name)
        {
            return this.SingleOrDefault(x => x.Name == name);
        }

        public List<CapabilityModel> GetByMenuItemId(int menuItemId)
        {
            return this.Where(x => x.MenuItemId == menuItemId).OrderBy(x => x.Name).ToList();
        }
    }
}
