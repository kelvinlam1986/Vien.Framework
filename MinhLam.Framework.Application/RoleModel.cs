using MinhLam.Framework.Data;
using MinhLam.Framework.Data.Entities;
using MinhLam.Framework.Data.Repo;
using System;
using System.Transactions;

namespace MinhLam.Framework.Application
{
    [Serializable()]
    public class RoleModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleCapabilityModelList RoleCapabilities { get; set; }
        public RoleUserAccountModelList RoleUserAccounts { get; set; }

        public RoleModel()
        {
            RoleCapabilities = new RoleCapabilityModelList();
            RoleUserAccounts = new RoleUserAccountModelList();
        }

        public override void Init()
        {
        }

        public override bool Load(object id)
        {
            try
            {
                var repo = new RoleData();
                int key = Convert.ToInt32(id);
                var role = repo.GetById(key);
                if (role != null)
                {
                    MapEntityToProperties(role);
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
                    using (TransactionScope ts = new TransactionScope())
                    {
                        if (IsNewRecord())
                        {
                            var role = new Role();
                            role.Name = Name;
                            role.CreatedBy = userName;
                            role.CreatedDate = DateTime.Now;
                            role.UpdatedBy = userName;
                            role.UpdatedDate = DateTime.Now;
                            var id = new RoleData().Insert(role);
                            Id = id;

                            foreach (var capability in RoleCapabilities)
                            {
                                capability.RoleId = Id;
                            }

                            foreach (var userRole in RoleUserAccounts)
                            {
                                userRole.RoleId = Id;
                            }
                        }
                        else
                        {
                            var role = new RoleData().GetById(Id);
                            role.Name = Name;
                            role.UpdatedBy = userName;
                            role.UpdatedDate = DateTime.Now;
                            new RoleData().Update(role);
                            Id = role.Id;
                        }

                        if (RoleCapabilities.Save(ref validationErrors, userName))
                        {
                            if (RoleUserAccounts.Save(ref validationErrors, userName))
                            {
                                ts.Complete();
                            }
                            else
                            {
                                Id = 0;
                                return false;
                            }
                        }
                        else
                        {
                            Id = 0;
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    Id = 0;
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
            var repo = new RoleData();
            var role = repo.GetById(Id);
            new RoleData().Remove(role);
        }

        protected override string GetDisplayText()
        {
            return Name;
        }

        protected override bool IsNewRecord()
        {
            return Id == 0;
        }

        protected override void MapEntityToCustomProperties(BaseEntity entity)
        {
            Role role = (Role)entity;
            Id = role.Id;
            Name = role.Name;
            RoleCapabilities.LoadByRoleId(role.Id);
            RoleUserAccounts.LoadByRoleId(role.Id);
        }

        protected override void Validate(ref ValidationErrors validationErrors)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                validationErrors.Add("Bạn phải nhập tên vai trò");
            }

            if (validationErrors.Count > 0)
            {
                return;
            }

            if (new RoleData().CheckExistingName(Id, Name))
            {
                validationErrors.Add("Tên vai trò này đã tồn tại");
            }
        }

        protected override void ValidateDelete(ref ValidationErrors validationErrors)
        {
            if (new RoleCapabilityData().CheckRoleInUse(Id))
            {
                validationErrors.Add("Vai trò này đã được sử dụng. Bạn không thể xóa");
            }

            if (new RoleUserAccountData().CheckRoleInUse(Id))
            {
                validationErrors.Add("Vai trò này đã được sử dụng. Bạn không thể xóa");
            }

            if (new RoleData().CheckExisting(Id) == false)
            {
                validationErrors.Add("Dữ liệu không tìm thấy trong hệ thống.");
            }
        }
    }
}
