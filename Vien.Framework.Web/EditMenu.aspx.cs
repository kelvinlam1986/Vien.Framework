using System;
using System.Web.UI.WebControls;
using Vien.Framework.Application;
using Vien.Framework.Application.UI;

namespace Vien.Framework.Web
{
    public partial class EditMenu : BaseEditPage<MenuItemModel>
    {
        public override string MenuItemName()
        {
            return "Trang chủ";
        }

        protected override void GoToGridPage()
        {
            Response.Redirect("Default.apsx");
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
            menuItemModel.Description = txtDescription.Text;
            menuItemModel.Url = txtUrl.Text;
        }

        protected override void LoadScreenFromObject(MenuItemModel baseEO)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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