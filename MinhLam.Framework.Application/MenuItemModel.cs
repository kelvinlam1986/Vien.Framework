using System;
using MinhLam.Framework.Data;
using MinhLam.Framework.Data.Entities;
using MinhLam.Framework.Data.Repo;

namespace MinhLam.Framework.Application
{
    [Serializable()]
    public class MenuItemModel : BaseModel
    {
        public MenuItemModel()
        {
            ChildMenuItems = new MenuItemModelList();
        }

        public int Id { get; set; }
        public string MenuItemName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Nullable<int> ParentMenuItemId { get; set; }
        public short DisplaySequence { get; set; }
        public bool IsAlwaysEnabled { get; set; }
        public string Icon { get; set; }
        public MenuItemModelList ChildMenuItems { get; set; }

        public override void Init()
        {
        }

        public override bool Load(object id)
        {
            try
            {
                var repo = new MenuItemData();
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

        public override bool Save(ref ValidationErrors validationErrors, string username)
        {
            try
            {
                Validate(ref validationErrors);
                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        var menuItem = new MenuItem();
                        menuItem.MenuItemName = MenuItemName;
                        menuItem.DisplayName = DisplayName;
                        menuItem.Description = Description;
                        menuItem.Url = Url;
                        menuItem.Icon = Icon;
                        menuItem.DisplaySequence = Convert.ToInt16(DisplaySequence);
                        if (ParentMenuItemId == null)
                        {
                            menuItem.ParentMenuItemId = null;
                        }
                        else
                        {
                            menuItem.ParentMenuItemId = Convert.ToInt32(ParentMenuItemId);
                        }

                        menuItem.IsAlwaysEnabled = IsAlwaysEnabled;
                        menuItem.CreatedBy = CreatedBy;
                        menuItem.CreatedDate = CreatedDate;
                        menuItem.UpdatedBy = UpdatedBy;
                        menuItem.UpdatedDate = UpdatedDate;

                        var id = new MenuItemData().Insert(menuItem);
                        Id = id;
                    }
                    else
                    {
                        var menuItem = new MenuItemData().GetById(Id);
                        menuItem.MenuItemName = MenuItemName;
                        menuItem.DisplayName = DisplayName;
                        menuItem.Description = Description;
                        menuItem.Url = Url;
                        menuItem.Icon = Icon;
                        menuItem.DisplaySequence = Convert.ToInt16(DisplaySequence);
                        if (ParentMenuItemId == null)
                        {
                            menuItem.ParentMenuItemId = null;
                        }
                        else
                        {
                            menuItem.ParentMenuItemId = Convert.ToInt32(ParentMenuItemId);
                        }

                        menuItem.IsAlwaysEnabled = IsAlwaysEnabled;
                        menuItem.UpdatedBy = UpdatedBy;
                        menuItem.UpdatedDate = UpdatedDate;
                        new MenuItemData().Update(menuItem);
                        Id = menuItem.Id;
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
            var repo = new MenuItemData();
            var menuItem = repo.GetById(Id);
            new MenuItemData().Remove(menuItem);
        }

        protected override string GetDisplayText()
        {
            return "MenuItemName";
        }

        protected override void MapEntityToCustomProperties(BaseEntity entity)
        {
            MenuItem menuItem = (MenuItem)entity;
            Id = menuItem.Id;
            MenuItemName = menuItem.MenuItemName;
            DisplayName = menuItem.DisplayName;
            Description = menuItem.Description;
            Url = menuItem.Url;
            ParentMenuItemId = menuItem.ParentMenuItemId;
            DisplaySequence = menuItem.DisplaySequence;
            IsAlwaysEnabled = menuItem.IsAlwaysEnabled;
            Icon = menuItem.Icon;
        }

        protected override void Validate(ref ValidationErrors validationErrors)
        {
            if (string.IsNullOrEmpty(MenuItemName))
            {
                validationErrors.Add("Bạn phải nhập tên Menu");
            }

            if (string.IsNullOrEmpty(DisplayName))
            {
                validationErrors.Add("Bạn phải nhập tên hiển thị");
            }

            if (DisplaySequence <= 0)
            {
                validationErrors.Add("Số thứ tự phải lớn hơn bằng 0");
            }

            if (new MenuItemData().CheckExistingName(Id, MenuItemName))
            {
                validationErrors.Add("Tên menu bạn đặt đã tồn tại");
            }
        }

        protected override void ValidateDelete(ref ValidationErrors validationErrors)
        {
            if (new MenuItemData().CheckExisting(Id) == false)
            {
                validationErrors.Add("Menu này chưa tồn tại");
            }

            if (new CapabilityData().CheckMenuInUse(Id))
            {
                validationErrors.Add("Menu này đã được sử dụng. Không thể xóa.");
            }
        }

        protected override bool IsNewRecord()
        {
            return Id == 0;
        }
    }
}
