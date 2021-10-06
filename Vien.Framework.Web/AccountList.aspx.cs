using System;
using System.Web.UI.WebControls;
using Vien.Framework.Application.UI;

namespace Vien.Framework.Web
{
    public partial class AccountList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public override string MenuItemName()
        {
            return "Accounts";
        }
    }
}