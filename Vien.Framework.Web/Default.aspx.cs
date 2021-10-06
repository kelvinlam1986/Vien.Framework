using System;
using System.Web.UI;
using Vien.Framework.Application.UI;

namespace Vien.Framework.Web
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        public override string MenuItemName()
        {
            return "Home";
        }

    }
}