using System;
using System.Web.UI.WebControls;
using Vien.Framework.Application;
using Vien.Framework.Application.UI;

namespace Vien.Framework.Web
{
    public partial class EditMenu : BaseEditPage<MenuItemModel>
    {
        private const string VIEW_STATE_KEY_MENU = "Menu";

        public override string MenuItemName()
        {
            return "Trang chủ";
        }

        protected override void GoToGridPage()
        {
            Response.Redirect("MenuList");
        }

        /// <summary>
        /// Preload the controls such as drop down lists and listboxes.
        /// </summary>
        protected override void LoadControls()
        {
            LoadMenuItemsToDropDown();
            LoadDropdownSequence();
        }

        protected override void LoadObjectFromScreen(MenuItemModel menuItemModel)
        {
            menuItemModel.MenuItemName = txtMenuItemName.Text;
            menuItemModel.DisplayName = txtDisplayName.Text;
            menuItemModel.Description = txtDescription.Text;
            menuItemModel.Url = txtUrl.Text;
            menuItemModel.DisplaySequence = Convert.ToInt16(ddlSequence.SelectedValue);
            if (ddlParentMenuItemId.SelectedValue != "")
            {
                menuItemModel.ParentMenuItemId = Convert.ToInt32(ddlParentMenuItemId.SelectedValue);
            }
            menuItemModel.Icon = txtIcon.Text;
            menuItemModel.IsAlwaysEnabled = ckIsAlwaysEnabled.Checked;
            if (string.IsNullOrEmpty(GetId()))
            {
                menuItemModel.CreatedBy = "Admin";
                menuItemModel.CreatedDate = DateTime.Now;
            }

            menuItemModel.UpdatedBy = "Admin";
            menuItemModel.UpdatedDate = DateTime.Now;

        }

        protected override void LoadScreenFromObject(MenuItemModel menuItemModel)
        {
            txtMenuItemName.Text = menuItemModel.MenuItemName;
            txtDisplayName.Text = menuItemModel.DisplayName;
            txtDescription.Text = menuItemModel.Description;
            txtUrl.Text = menuItemModel.Url;
            txtIcon.Text = menuItemModel.Icon;
            ckIsAlwaysEnabled.Checked = menuItemModel.IsAlwaysEnabled;
            ddlSequence.SelectedValue = menuItemModel.DisplaySequence.ToString();
            if (menuItemModel.ParentMenuItemId == null)
            {
                ddlParentMenuItemId.SelectedValue = "";
            }
            else
            {
                ddlParentMenuItemId.SelectedValue = menuItemModel.ParentMenuItemId.ToString();
            }
            
            ViewState[VIEW_STATE_KEY_MENU] = menuItemModel;
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

            var menuItem = (MenuItemModel)ViewState[VIEW_STATE_KEY_MENU];
            LoadObjectFromScreen(menuItem);

            if (!menuItem.Save(ref validationErrors, "Admin"))
            {
                Master.ValidationErrors = validationErrors;
            }
            else
            {
                GoToGridPage();
            }
        }

        private void LoadDropdownSequence()
        {
            for (int i = 1; i < 100; i++)
            {
                ddlSequence.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void LoadMenuItemsToDropDown()
        {
            var menuItemModelList = new MenuItemModelList();
            string idString = base.GetId();

            if (string.IsNullOrEmpty(idString))
            {
                menuItemModelList.Load();
            }
            else
            {
                int id = Convert.ToInt32(idString);
                menuItemModelList.LoadAllMenuItemsExceptId(id);
            }

            foreach (var menuItemModel in menuItemModelList)
            {
                ddlParentMenuItemId.Items.Add(new ListItem(menuItemModel.MenuItemName, menuItemModel.Id.ToString()));
            }

            ddlParentMenuItemId.Items.Insert(0, new ListItem("Không có menu cha", ""));
        }
    }
}