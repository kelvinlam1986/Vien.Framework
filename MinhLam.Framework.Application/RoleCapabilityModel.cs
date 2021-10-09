using MinhLam.Framework.Data;
using MinhLam.Framework.Data.Entities;
using MinhLam.Framework.Data.Repo;
using System;

namespace MinhLam.Framework.Application
{
    [Serializable()]
    public class RoleCapabilityModel : BaseModel
    {
        public enum CapabilityAccessFlagEnum
        {
            None,
            ReadOnly,
            Edit
        }

        public RoleCapabilityModel()
        {
            Capability = new CapabilityModel();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public int CapabilityId { get; set; }
        public CapabilityAccessFlagEnum AccessFlag { get; set; }
        public CapabilityModel Capability { get; set; }

        public override void Init()
        {
        }

        public override bool Load(object id)
        {
            try
            {
                var repo = new RoleCapabilityData();
                int key = Convert.ToInt32(id);
                var roleCapability = repo.GetById(key);
                if (roleCapability != null)
                {
                    MapEntityToProperties(roleCapability);
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
                        var roleCapability = new RoleCapability();
                        roleCapability.RoleId = RoleId;
                        roleCapability.CapabilityId = CapabilityId;
                        roleCapability.AccessFlag = (byte)AccessFlag;
                        var id = new RoleCapabilityData().Insert(roleCapability);
                        Id = id;
                    }
                    else
                    {
                        var roleCapability = new RoleCapabilityData().GetById(Id);
                        roleCapability.RoleId = RoleId;
                        roleCapability.CapabilityId = CapabilityId;
                        roleCapability.AccessFlag = (byte)AccessFlag;
                        new RoleCapabilityData().Update(roleCapability);
                        Id = roleCapability.Id;
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
            return "";
        }

        protected override void MapEntityToCustomProperties(BaseEntity entity)
        {
            RoleCapability roleCapability = (RoleCapability)entity;
            Id = roleCapability.Id;
            CapabilityId = roleCapability.CapabilityId;
            RoleId = roleCapability.RoleId;
            AccessFlag = (CapabilityAccessFlagEnum)roleCapability.AccessFlag;
            Capability.Load(roleCapability.CapabilityId);
        }

        protected override void Validate(ref ValidationErrors validationErrors)
        {
            if (CapabilityId == 0)
            {
                validationErrors.Add("Bạn phải chọn một chức năng");
            }

            if (RoleId == 0)
            {
                validationErrors.Add("Bạn phải chọn một vai trò");
            }

            if (validationErrors.Count > 0)
            {
                return;
            }

            if (new CapabilityData().CheckExisting(CapabilityId) == false)
            {
                validationErrors.Add("Chức năng bạn chọn không tồn tại trong hệ thống");
            }

            if (new RoleData().CheckExisting(RoleId) == false)
            {
                validationErrors.Add("Vai trò bạn chọn không tồn tại trong hệ thống");
            }
        }

        protected override void ValidateDelete(ref ValidationErrors validationErrors)
        {
            if (new RoleCapabilityData().CheckExisting(Id))
            {
                validationErrors.Add("Dữ liệu không tồn tại trong hệ thống");
            }
        }

        protected override bool IsNewRecord()
        {
            return Id == 0;
        }
    }
}
