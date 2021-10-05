using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vien.Framework.Application;

namespace View.Framework.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CustomMenu runat=server></{0}:CustomMenu>")]
    public class CustomMenu : WebControl
    {
        [Browsable(false)]
        public MenuItemModelList MenuItems { get; set; }

        [Browsable(false)]
        public string CurrentMenuItemName { get; set; }

        [Browsable(true)]
        [DefaultValue("Enter Application Root Path")]
        [Description("Enter the root path for your application.  This is used to determine the path for all items in the menu.")]
        public string RootPath { get; set; }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);

            string html;

            // Get the parent menu item for the current menu item.  The parent will be 
            // the one with a null ParentMenuItemId
            if (MenuItems != null)
            {
                // MenuItemModel topMenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);
                html = "<ul class='nav in' id='side-menu'>";

                int i = 0;
                foreach (var mi in MenuItems)
                {
                    var parentUrl = string.IsNullOrEmpty(mi.Url) ? "#" : "";
                    html += "<li>";
                    if (mi.ChildMenuItems.Count == 0)
                    {
                        html += $"<a href='{parentUrl}'><i class='fa {mi.Icon}'></i>&nbsp;{mi.DisplayName}</a>";
                    }
                    else
                    {
                        html += $"<a href='#'><i class='fa {mi.Icon}'></i>&nbsp;{mi.DisplayName}<span class='fa arrow'></span></a>";

                    }


                    if (mi.ChildMenuItems.Count > 0)
                    {

                        html += "<ul class='nav nav-second-level collapse'>";
                        foreach (var cmi in mi.ChildMenuItems)
                        {
                            var childUrl = string.IsNullOrEmpty(mi.Url) ? "#" : "";
                            html += "<li>";
                            html += $"<a href='{childUrl}'><i class='fa {cmi.Icon}'></i>&nbsp;{cmi.DisplayName}</a>";
                            html += "</li>";
                        }
                        html += "</ul>";
                    }

                    html += "</li>";
                    i++;
                }

                html += "</ul>";

            }
            else
            {
                html = "<div>Top Menu Goes Here</div>";
            }

            writer.Write(html);
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }
    }
}
