using System;
using Vien.Framework.Application;
using Vien.Framework.Application.UI;

namespace Vien.Framework.Web
{
    public partial class EditGrid : System.Web.UI.MasterPage
    {
        public delegate void ButtonClickedHandler(object sender, EventArgs e);
        public event ButtonClickedHandler AddButton_Click;
        public event ButtonClickedHandler PrintButton_Click;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (((BasePage)this.Page).ReadOnly)
            {
                // Hide the Add Button
                btnAddNew.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            if (AddButton_Click != null)
            {
                AddButton_Click(sender, e);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (PrintButton_Click != null)
            {
                PrintButton_Click(sender, e);
            }
        }

        public ValidationErrors ValidationErrors
        {
            get
            {
                return ErrorMessages.ValidationErrors;
            }
            set
            {
                ErrorMessages.ValidationErrors = value;
            }
        }
    }
}