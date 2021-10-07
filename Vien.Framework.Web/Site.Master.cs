using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vien.Framework.Application.UI;

namespace Vien.Framework.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cmMenu.MenuItems = Globals.GetMenuItems(this.Cache);
            cmMenu.RootPath = BasePage.RootPath(Context);
            cmMenu.CurrentMenuItemName = ((BasePage)Page).MenuItemName();
            lblCurrentUser.InnerText = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
        }
    }
}