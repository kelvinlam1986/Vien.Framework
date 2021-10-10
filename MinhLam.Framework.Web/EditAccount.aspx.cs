using System;
using MinhLam.Framework.Application;
using MinhLam.Framework.Application.UI;

namespace MinhLam.Framework.Web
{
    public partial class EditAccount : BaseEditPage<UserAccountModel>
    {
        private const string VIEW_STATE_KEY_MENU = "UserAccount";

        public override string[] CapabilityNames()
        {
            return new string[] { "User" };
        }

        public override string MenuItemName()
        {
            return "Accounts";
        }

        protected override void GoToGridPage()
        {
            Response.Redirect("AccountList");
        }

        protected override void LoadControls()
        {
        }

        protected override void LoadObjectFromScreen(UserAccountModel userAccountModel)
        {
            userAccountModel.WindowAccountName = txtWindowAccountName.Text;
            userAccountModel.FullName = txtFullName.Text;
            userAccountModel.Email = txtEmail.Text;
            userAccountModel.IsActive = ckIsActive.Checked;
        }

        protected override void LoadScreenFromObject(UserAccountModel userAccountModel)
        {
            txtWindowAccountName.Text = userAccountModel.WindowAccountName;
            txtFullName.Text = userAccountModel.FullName;
            txtEmail.Text = userAccountModel.Email;
            ckIsActive.Checked = userAccountModel.IsActive;
            ViewState[VIEW_STATE_KEY_MENU] = userAccountModel;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SaveButton_Click += new EditPage.ButtonClickedHandler(Master_SaveButton_Click);
            Master.CancelButton_Click += new EditPage.ButtonClickedHandler(Master_CancelButton_Click);
        }

        private void Master_CancelButton_Click(object sender, EventArgs e)
        {
            GoToGridPage();
        }

        private void Master_SaveButton_Click(object sender, EventArgs e)
        {
            ValidationErrors validationErrors = new ValidationErrors();

            var userAccount = (UserAccountModel)ViewState[VIEW_STATE_KEY_MENU];
            LoadObjectFromScreen(userAccount);

            if (!userAccount.Save(ref validationErrors, "Admin"))
            {
                Master.ValidationErrors = validationErrors;
            }
            else
            {
                GoToGridPage();
            }
        }
    }
}