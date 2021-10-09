using MinhLam.Framework.Data;
using MinhLam.Framework.Data.Entities;
using MinhLam.Framework.Data.Repo;
using System;

namespace MinhLam.Framework.Application
{
    [Serializable]
    public class CapabilityModel : BaseModel
    {
        public enum AccessTypeEnum
        {
            ReadOnlyEdit = 0,
            ReadOnly = 1,
            Edit = 2
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MenuItemId { get; set; }
        public AccessTypeEnum AccessType { get; set; }

        public override void Init()
        {
        }

        public override bool Load(object id)
        {
            try
            {
                var repo = new CapabilityData();
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
            return true;
        }

        protected override void DeleteForReal()
        {
        }

        protected override string GetDisplayText()
        {
            return "Name";
        }

        protected override void MapEntityToCustomProperties(BaseEntity entity)
        {
            Capability capability = (Capability)entity;
            Id = capability.Id;
            Name = capability.Name;
            MenuItemId = capability.MenuItemId;
            AccessType = (AccessTypeEnum)capability.AccessType;
        }

        protected override void Validate(ref ValidationErrors validationErrors)
        {
        }

        protected override void ValidateDelete(ref ValidationErrors validationErrors)
        {
        }

        protected override bool IsNewRecord()
        {
            return Id == 0;
        }
    }
}
