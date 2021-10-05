using System;
using Vien.Framework.Application;
using Vien.Framework.Application.UI;

namespace Vien.Framework.Web
{
    public partial class EditPage : System.Web.UI.MasterPage
    {
        public delegate void ButtonClickedHandler(object sender, EventArgs e);
        public event ButtonClickedHandler SaveButton_Click;
        public event ButtonClickedHandler CancelButton_Click;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (((BasePage)Page).ReadOnly)
            {
                // Hide the save button
                btnSave.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveButton_Click != null)
            {
                SaveButton_Click(sender, e);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelButton_Click != null)
            {
                CancelButton_Click(sender, e);
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