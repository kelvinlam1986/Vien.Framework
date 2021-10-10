using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MinhLam.Framework.Application;
using MinhLam.Framework.Application.UI;

namespace MinhLam.Framework.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserAccountModel currentUser = ((BasePage)Page).CurrentUser;
            cmMenu.MenuItems = Globals.GetMenuItems(this.Cache);
            cmMenu.RootPath = BasePage.RootPath(Context);
            cmMenu.CurrentMenuItemName = ((BasePage)Page).MenuItemName();
            cmMenu.Roles = Globals.GetRoles(this.Cache);
            cmMenu.UserAccount = currentUser;
            lblCurrentUser.InnerText = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
        }
    }
}