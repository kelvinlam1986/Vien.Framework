using MinhLam.Framework.Application;
using MinhLam.Framework.Application.UI;
using MinhLam.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vien.Framework.Web
{
    public partial class EditRole : BaseEditPage<RoleModel>
    {
        private const string VIEW_STATE_KEY_MENU = "Role";

        public override string[] CapabilityNames()
        {
            return new string[] { "Role" };
        }

        public override string MenuItemName()
        {
            return "Roles";
        }

        protected override void GoToGridPage()
        {
            Response.Redirect("RoleList");
        }

        protected override void LoadControls()
        {
        }

        protected override void LoadObjectFromScreen(RoleModel roleModel)
        {
            roleModel.Name = txtRoleName.Text;
            for (int row = 0; row < tblCapabilities.Rows.Count; row++)
            {
                TableRow tr = tblCapabilities.Rows[row];
                if (tr.Cells.Count > 0)
                {
                    RadioButtonList radioButtons = (RadioButtonList)tr.Cells[1].Controls[0];
                    int capabilityId = Convert.ToInt32(radioButtons.ID);
                    string value = radioButtons.SelectedValue;
                    RoleCapabilityModel.CapabilityAccessFlagEnum accessFlag =
                        (RoleCapabilityModel.CapabilityAccessFlagEnum)Enum.Parse(
                            typeof(RoleCapabilityModel.CapabilityAccessFlagEnum), value);

                    RoleCapabilityModel capability = roleModel.RoleCapabilities.GetByCapabilityId(capabilityId);
                    if (capability == null)
                    {
                        // New record
                        var roleCapability = new RoleCapabilityModel();
                        roleCapability.RoleId = roleModel.Id;
                        roleCapability.CapabilityId = capabilityId;
                        roleCapability.Capability.Id = capabilityId;
                        roleCapability.AccessFlag = accessFlag;
                        roleModel.RoleCapabilities.Add(roleCapability);
                    }
                    else
                    {
                        capability.AccessFlag = accessFlag;
                    }

                }
            }

            foreach (ListItem item in lstSelectedUsers.Items)
            {
                if (roleModel.RoleUserAccounts.IsUserInRole(Convert.ToInt32(item.Value)) == false)
                {
                    roleModel.RoleUserAccounts.Add(new RoleUserAccountModel
                    {
                        AccountId = Convert.ToInt32(item.Value),
                        RoleId = roleModel.Id
                    });
                }
            }

            foreach (ListItem item in lstUnSelectedUsers.Items)
            {
                if (roleModel.RoleUserAccounts.IsUserInRole(Convert.ToInt32(item.Value)))
                {
                    var roleUserAccount = roleModel.RoleUserAccounts.GetByAccountId(Convert.ToInt32(item.Value));
                    roleUserAccount.IsDeleted = true;
                }
            }
        }

        protected override void LoadScreenFromObject(RoleModel roleModel)
        {
            txtRoleName.Text = roleModel.Name;
            for (int row = 0; row < tblCapabilities.Rows.Count; row++)
            {
                TableRow tr = tblCapabilities.Rows[row];
                if (tr.Cells.Count > 0)
                {
                    RadioButtonList radioButtons = (RadioButtonList)tr.Cells[1].Controls[0];
                    RoleCapabilityModel capability = 
                        roleModel.RoleCapabilities.GetByCapabilityId(Convert.ToInt32(radioButtons.ID));
                    if (capability != null)
                    {
                        radioButtons.SelectedValue = capability.AccessFlag.ToString();
                    }
                    else
                    {
                        radioButtons.SelectedIndex = 0;
                    }

                    capability = null;
                }
            }

            UserAccountModelList users = Globals.GetUsers(Page.Cache);
            foreach (var user in users)
            {
                if (roleModel.RoleUserAccounts.IsUserInRole(user.Id))
                {
                    lstSelectedUsers.Items.Add(new ListItem(user.DisplayText, user.Id.ToString()));
                }
                else
                {
                    lstUnSelectedUsers.Items.Add(new ListItem(user.DisplayText, user.Id.ToString()));
                }
            }

            ViewState[VIEW_STATE_KEY_MENU] = roleModel;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SaveButton_Click += new EditPage.ButtonClickedHandler(Master_SaveButton_Click);
            Master.CancelButton_Click += new EditPage.ButtonClickedHandler(Master_CancelButton_Click);
        }

        protected override void OnInit(EventArgs e)
        {
            //You need to build the table here so it retains state between postbacks.
            BuildCapabilityTable();
            base.OnInit(e);
        }

        private void BuildCapabilityTable()
        {
            CapabilityModelList capabilities = Globals.GetCapabilities(this.Cache);
            MenuItemModelList menuItems = Globals.GetMenuItems(this.Cache);
            AddCapabilitiesForMenuItems(menuItems, capabilities, "");
        }

        private void AddCapabilitiesForMenuItems(
            MenuItemModelList menuItems, CapabilityModelList capabilities, string indentation)
        {
            foreach (var menuItem in menuItems)
            {
                var capabilitiesForMenuItem = capabilities.GetByMenuItemId(menuItem.Id);
                if (capabilitiesForMenuItem.Count == 0)
                {
                    //Just add the menu item to the row
                    TableRow tr = new TableRow();
                    TableCell tc = new TableCell();
                    LiteralControl lc = new LiteralControl();
                    lc.Text = indentation + menuItem.MenuItemName;
                    tc.CssClass = "capabilityHeader";
                    tc.Controls.Add(lc);
                    tc.ColumnSpan = 3;
                    tr.Cells.Add(tc);

                    tblCapabilities.Rows.Add(tr);
                }
                else
                {
                    //If there is only one capability associated with this menu item then just display the
                    //menu item name and the radio buttons
                    if (capabilitiesForMenuItem.Count() == 1)
                    {
                        AddCapabilityToTable(capabilitiesForMenuItem.ElementAt(0), indentation + menuItem.MenuItemName);
                    }
                    else
                    {
                        //Add a row for each capability
                        foreach (CapabilityModel capability in capabilitiesForMenuItem)
                        {
                            AddCapabilityToTable(capability, indentation + menuItem.MenuItemName + 
                                " (" + capability.Name + ")");
                        }
                    }

                    if (menuItem.ChildMenuItems.Count > 0)
                    {
                        AddCapabilitiesForMenuItems(menuItem.ChildMenuItems, capabilities, indentation + "---");
                    }
                }
            }
        }

        private void AddCapabilityToTable(CapabilityModel capability, string text)
        {
            TableRow tr = new TableRow();

            //Name
            TableCell tc = new TableCell();
            tc.CssClass = "capabilityHeader";
            LiteralControl lc = new LiteralControl();
            lc.Text = text;
            tc.Controls.Add(lc);
            tr.Cells.Add(tc);

            //access flag
            TableCell tc1 = new TableCell();

            RadioButtonList radioButtons = new RadioButtonList();
            radioButtons.ID = capability.Id.ToString();
            radioButtons.CssClass = "radiostyle";

            switch (capability.AccessType)
            {
                case CapabilityModel.AccessTypeEnum.ReadOnlyEdit:
                    radioButtons.Items.Add(new ListItem("None", RoleCapabilityModel.CapabilityAccessFlagEnum.None.ToString()));
                    radioButtons.Items.Add(new ListItem("Read Only", RoleCapabilityModel.CapabilityAccessFlagEnum.ReadOnly.ToString()));
                    radioButtons.Items.Add(new ListItem("Edit", RoleCapabilityModel.CapabilityAccessFlagEnum.Edit.ToString()));
                    radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                    radioButtons.RepeatLayout = RepeatLayout.Table;
                    break;
                case CapabilityModel.AccessTypeEnum.ReadOnly:
                    radioButtons.Items.Add(new ListItem("None", RoleCapabilityModel.CapabilityAccessFlagEnum.None.ToString()));
                    radioButtons.Items.Add(new ListItem("Read Only", RoleCapabilityModel.CapabilityAccessFlagEnum.ReadOnly.ToString()));
                    radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                    radioButtons.RepeatLayout = RepeatLayout.Table;
                    break;
                case CapabilityModel.AccessTypeEnum.Edit:
                    radioButtons.Items.Add(new ListItem("None", RoleCapabilityModel.CapabilityAccessFlagEnum.None.ToString()));
                    radioButtons.Items.Add(new ListItem("Edit", RoleCapabilityModel.CapabilityAccessFlagEnum.Edit.ToString()));
                    radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                    radioButtons.RepeatLayout = RepeatLayout.Table;
                    break;
            }

            tc1.Controls.Add(radioButtons);
            tr.Cells.Add(tc1);
            tblCapabilities.Rows.Add(tr);
        }

        private void MoveItems(ListBox lstSource, ListBox lstDestination, bool moveAll)
        {
            for (int i = 0; i < lstSource.Items.Count; i++)
            {
                ListItem li = lstSource.Items[i];
                if ((moveAll == true) || (li.Selected))
                {
                    li.Selected = true;
                    lstDestination.Items.Add(li);
                    lstSource.Items.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Master_CancelButton_Click(object sender, EventArgs e)
        {
            GoToGridPage();
        }

        private void Master_SaveButton_Click(object sender, EventArgs e)
        {
            ValidationErrors validationErrors = new ValidationErrors();

            var role = (RoleModel)ViewState[VIEW_STATE_KEY_MENU];
            LoadObjectFromScreen(role);

            if (!role.Save(ref validationErrors, "Admin"))
            {
                Master.ValidationErrors = validationErrors;
            }
            else
            {
                Globals.ClearCache(Page.Cache);
                Globals.LoadMenuItems(Page.Cache);
                Globals.LoadCapabilities(Page.Cache);
                Globals.LoadUsers(Page.Cache);
                Globals.LoadRoles(Page.Cache);
                GoToGridPage();
            }
        }

        protected void btnMoveToSelected_Click(object sender, EventArgs e)
        {
            MoveItems(lstUnSelectedUsers, lstSelectedUsers, false);
        }

        protected void btnMoveAllToSelected_Click(object sender, EventArgs e)
        {
            MoveItems(lstUnSelectedUsers, lstSelectedUsers, true);
        }

        protected void btnMoveToUnSelected_Click(object sender, EventArgs e)
        {
            MoveItems(lstSelectedUsers, lstUnSelectedUsers, false);
        }

        protected void btnMoveAllToUnSelected_Click(object sender, EventArgs e)
        {
            MoveItems(lstSelectedUsers, lstUnSelectedUsers, true);
        }

        public override void CustomReadOnlyLogic(string capabilityName)
        {
            base.CustomReadOnlyLogic(capabilityName);

            //If this is read only then do not show the available choice for the users or
            //the buttons to swap between list boxes
            lstUnSelectedUsers.Visible = false;
            btnMoveAllToSelected.Visible = false;
            btnMoveAllToUnSelected.Visible = false;
            btnMoveToSelected.Visible = false;
            btnMoveToUnSelected.Visible = false;
            lblUsers.Visible = false;
            lblUserHeader.Visible = false;
        }
    }
}