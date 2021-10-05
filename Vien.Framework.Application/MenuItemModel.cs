using System;
using Vien.Framework.Data;
using Vien.Framework.Data.Entities;

namespace Vien.Framework.Application
{
    public class MenuItemModel : BaseModel
    {
        public MenuItemModel()
        {
            ChildMenuItems = new MenuItemModelList();
        }

        public int Id { get; set; }
        public string MenuItemName { get; set; }
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
            return true;
        }

        public override bool Save(ref ValidationErrors validationErrors, int userAccountId)
        {
            return true;
        }

        protected override void DeleteForReal()
        {
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
            Description = menuItem.Description;
            Url = menuItem.Url;
            ParentMenuItemId = menuItem.ParentMenuItemId;
            DisplaySequence = menuItem.DisplaySequence;
            IsAlwaysEnabled = menuItem.IsAlwaysEnabled;
            Icon = menuItem.Icon;
        }

        protected override void Validate(ref ValidationErrors validationErrors)
        {
        }

        protected override void ValidateDelete(ref ValidationErrors validationErrors)
        {
        }
    }
}
