using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace MinhLam.Framework.Application.UI
{
    /// <summary>
    /// Summary description for Globals
    /// </summary>
    public static class Globals
    {
        #region Constants

        private const string CACHE_KEY_MENU_ITEMS = "MenuItems";
        private const string CACHE_KEY_USERS = "Users";
        private const string CACHE_KEY_ROLES = "Roles";
        private const string CACHE_KEY_CAPABILITIES = "Capabilities";

        #endregion Constants

        #region Methods

        public static MenuItemModelList GetMenuItems(Cache cache)
        {
            //Check if the menus have been cached.
            if (cache[CACHE_KEY_MENU_ITEMS] == null)
            {
                LoadMenuItems(cache);
            }

            return (MenuItemModelList)cache[CACHE_KEY_MENU_ITEMS];
        }

        public static void ClearCache(Cache cache)
        {
            cache.Remove(CACHE_KEY_MENU_ITEMS);
            cache.Remove(CACHE_KEY_USERS);
            cache.Remove(CACHE_KEY_ROLES);
            cache.Remove(CACHE_KEY_CAPABILITIES);
        }

        public static UserAccountModelList GetUsers(Cache cache)
        {
            //Check for the users
            if (cache[CACHE_KEY_USERS] == null)
            {
                LoadUsers(cache);
            }

            return (UserAccountModelList)cache[CACHE_KEY_USERS];
        }

        public static RoleModelList GetRoles(Cache cache)
        {
            //Check for the roles
            if (cache[CACHE_KEY_ROLES] == null)
            {
                LoadRoles(cache);
            }

            return (RoleModelList)cache[CACHE_KEY_ROLES];
        }

        public static CapabilityModelList GetCapabilities(Cache cache)
        {
            //Check for the roles
            if (cache[CACHE_KEY_CAPABILITIES] == null)
            {
                LoadCapabilities(cache);
            }

            return (CapabilityModelList)cache[CACHE_KEY_CAPABILITIES];
        }

        public static void LoadMenuItems(Cache cache)
        {
            MenuItemModelList menuItems = new MenuItemModelList();
            menuItems.Load();

            cache.Remove(CACHE_KEY_MENU_ITEMS);
            cache[CACHE_KEY_MENU_ITEMS] = menuItems;
        }

        public static void LoadUsers(Cache cache)
        {
            UserAccountModelList users = new UserAccountModelList();
            users.LoadWithRoles();

            cache.Remove(CACHE_KEY_USERS);
            cache[CACHE_KEY_USERS] = users;
        }

        public static void LoadRoles(Cache cache)
        {
            RoleCapabilityModelList roles = new RoleCapabilityModelList();
            roles.Load();

            cache.Remove(CACHE_KEY_ROLES);
            cache[CACHE_KEY_ROLES] = roles;
        }

        public static void LoadCapabilities(Cache cache)
        {
            CapabilityModelList capabilities = new CapabilityModelList();
            capabilities.Load();

            cache.Remove(CACHE_KEY_CAPABILITIES);
            cache[CACHE_KEY_CAPABILITIES] = capabilities;
        }

        #endregion Methods
    }

}
