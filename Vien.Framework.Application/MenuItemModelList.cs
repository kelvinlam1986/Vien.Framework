using System;
using System.Collections.Generic;
using System.Linq;
using Vien.Framework.Data;
using Vien.Framework.Data.Entities;

namespace Vien.Framework.Application
{
    [Serializable()]
    public class MenuItemModelList : BaseModelList<MenuItemModel>
    {
        public override void Load()
        {
            List<MenuItem> menuItems = new MenuItemData().GetAll();
            foreach (var menuItem in menuItems)
            {
                var menuItemModel = new MenuItemModel();
                menuItemModel.MapEntityToProperties(menuItem);

                if (MenuExist(menuItemModel.Id) == false)
                {
                    if (menuItemModel.ParentMenuItemId == null)
                    {
                        this.Add(menuItemModel);
                    }
                    else
                    {
                        MenuItemModel parent = GetByMenuItemId(Convert.ToInt32(menuItemModel.ParentMenuItemId));
                        if (parent == null)
                        {
                            MenuItemModel newParentMenuItem = FindOrLoadParent(menuItems, Convert.ToInt32(menuItemModel.ParentMenuItemId));
                            newParentMenuItem.ChildMenuItems.Add(menuItemModel);
                        }
                        else
                        {
                            parent.ChildMenuItems.Add(menuItemModel);
                        }
                    }
                }
            }
        }

        public void LoadAll()
        {
            List<MenuItem> menuItems = new MenuItemData().GetAll();
            foreach (var menuItem in menuItems)
            {
                var menuItemModel = new MenuItemModel();
                menuItemModel.MapEntityToProperties(menuItem);
                this.Add(menuItemModel);
            }
      
        }

        public void Search(string keyword)
        {
            List<MenuItem> menuItems = new MenuItemData().Search(keyword);
            foreach (var menuItem in menuItems)
            {
                var menuItemModel = new MenuItemModel();
                menuItemModel.MapEntityToProperties(menuItem);
                this.Add(menuItemModel);
            }
        }

        private MenuItemModel FindOrLoadParent(List<MenuItem> menuItems, int parentMenuItemId)
        {
            // Find the menu item in the entity list.
            MenuItem parentMenuItem = menuItems.Single(m => m.Id == parentMenuItemId);

            // Load this into the business object.
            MenuItemModel menuItemBO = new MenuItemModel();
            menuItemBO.MapEntityToProperties(parentMenuItem);

            // Check if it has a parent
            if (parentMenuItem.ParentMenuItemId == null)
            {
                this.Add(menuItemBO);
            }
            else
            {
                // Since this has a parent it should be added to its parent's children.
                // Try to find the parent in the list already.
                MenuItemModel parent = GetByMenuItemId(Convert.ToInt32(parentMenuItem.ParentMenuItemId));

                if (parent == null)
                {
                    // This one's parent wasn't found.  So add it.
                    MenuItemModel newParent = FindOrLoadParent(menuItems, Convert.ToInt32(parentMenuItem.ParentMenuItemId));
                    newParent.ChildMenuItems.Add(menuItemBO);
                }
                else
                {
                    // Add this menu to the child of the parent
                    parent.ChildMenuItems.Add(menuItemBO);
                }
            }

            return menuItemBO;
        }

        private MenuItemModel GetByMenuItemId(int menuItemId)
        {
            foreach (var menuItem in this)
            {
                if (menuItem.Id == menuItemId)
                {
                    return menuItem;
                }
                else
                {
                    if (menuItem.ChildMenuItems.Count > 0)
                    {
                        MenuItemModel childMenuItem = menuItem.ChildMenuItems.GetByMenuItemId(menuItemId);
                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }

            return null;
        }

        public MenuItemModel GetByMenuItemName(string menuItemName)
        {
            foreach (MenuItemModel menuItem in this)
            {
                // Check if this is the item we are looking for
                if (menuItem.MenuItemName == menuItemName)
                {
                    return menuItem;
                }
                else
                {
                    // Check if this menu has children
                    if (menuItem.ChildMenuItems.Count > 0)
                    {
                        // Search the children for this item.
                        MenuItemModel childMenuItem = menuItem.ChildMenuItems.GetByMenuItemName(menuItemName);

                        // If the menu is found in the children then it won't be null
                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }

            //It wasn't found so return null.
            return null;
        }

        private bool MenuExist(int id)
        {
            return (GetByMenuItemId(id) != null);
        }

        //Return the menu item that is at the top most level that does not have a parent.
        public MenuItemModel GetTopMenuItem(string menuItemName)
        {
            //Find the menu item by it name.
            MenuItemModel menuItem = GetByMenuItemName(menuItemName);

            while (menuItem.ParentMenuItemId != null)
            {
                menuItem = GetByMenuItemId(Convert.ToInt32(menuItem.ParentMenuItemId));
            }

            return menuItem;
        }

        public void LoadAllMenuItemsExceptId(int menuItemId)
        {
            var menuItemDatas = new MenuItemData();
            var menuItems = menuItemDatas.GetAll().Where(x => x.Id != menuItemId).ToList();
            var menuItemModel = new MenuItemModel();
            foreach (var menuItem in menuItems)
            {
                menuItemModel.MapEntityToProperties(menuItem);
                this.Add(menuItemModel);
            }
        }
    }
}
