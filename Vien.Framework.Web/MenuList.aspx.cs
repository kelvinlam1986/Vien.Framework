using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MinhLam.Framework.Application;
using MinhLam.Framework.Application.UI;

namespace MinhLam.Framework.Web
{
    public partial class MenuList : BasePage
    {
        private const int COL_INDEX_ACTION = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.AddButton_Click += new EditGrid.ButtonClickedHandler(Master_AddButton_Click);
            Master.SearchButton_Click += new EditGrid.ButtonClickedHandler(Master_SearchButton_Click);

            if (!IsPostBack)
            {
                CustomGridView1.ListClassName = typeof(MenuItemModelList).AssemblyQualifiedName;
                CustomGridView1.LoadMethodName = "LoadAll";

                //Name
                CustomGridView1.AddBoundField("MenuItemName", "Tên Menu", "MenuItemName");
                CustomGridView1.AddBoundField("Description", "Mô tả", "Description");
                CustomGridView1.AddBoundField("Url", "Url", "Url");
                CustomGridView1.AddBoundField("DisplaySequence", "Thứ tự", "DisplaySequence");
                CustomGridView1.AddCheckBoxField("IsAlwaysEnabled", "Luôn hiển thị", "IsAlwaysEnabled");
                // Action column - Contains the Edit link
                CustomGridView1.AddBoundField("", "Thao tác", "");
                CustomGridView1.DataBind();
            }
            else
            {
                if (Page.Request.Form["__EVENTTARGET"] != null)
                {
                    string eventTarget = Page.Request.Form["__EVENTTARGET"].ToString();
                    if (eventTarget.IndexOf("lbtnDelete") > -1)
                    {
                        CustomGridView1.LoadMethodName = "Search";
                        CustomGridView1.LoadMethodParameters.Add(this.Master.SearchString);
                        //Rebind the grid so the delete event is captured.
                        CustomGridView1.DataBind();
                    }
                }
            }
        }

        private void Master_AddButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditMenu.aspx" + EncryptQueryString("id=0"));
        }

        private void Master_SearchButton_Click(object sender, EventArgs e)
        {
            CustomGridView1.ListClassName = typeof(MenuItemModelList).AssemblyQualifiedName;
            CustomGridView1.LoadMethodName = "Search";
            CustomGridView1.LoadMethodParameters.Add(this.Master.SearchString);
            CustomGridView1.DataBind();
        }

        public override string MenuItemName()
        {
            return "Menus";
        }

        protected void CustomGridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Add the edit link to action column
                HyperLink editLink = new HyperLink();
                if (ReadOnly)
                {
                    editLink.Text = "Xem";
                    editLink.CssClass = "btn btn-default";
                }
                else
                {
                    editLink.CssClass = "btn btn-default";
                    editLink.Text = "Sửa";
                }

                editLink.NavigateUrl = "EditMenu.aspx" + EncryptQueryString("id=" +
                    ((MenuItemModel)e.Row.DataItem).Id.ToString());
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);
                if (ReadOnly == false)
                {
                    LiteralControl lc = new LiteralControl(" &nbsp; ");
                    e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                    // Add the Delete link
                    LinkButton lbtnDelete = new LinkButton();
                    lbtnDelete.ID = "lbtnDelete" + ((MenuItemModel)e.Row.DataItem).Id.ToString();
                    lbtnDelete.Text = "Xóa";
                    lbtnDelete.CssClass = "btn btn-danger";
                    lbtnDelete.CommandArgument = ((MenuItemModel)e.Row.DataItem).Id.ToString();
                    lbtnDelete.OnClientClick = "return ConfirmDelete();";
                    lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                    e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
                }
            }
        }

        private void lbtnDelete_Command(object sender, CommandEventArgs e)
        {
            ValidationErrors validationErrors = new ValidationErrors();
            MenuItemModel menuItem = new MenuItemModel();
            menuItem.Id = Convert.ToInt32(e.CommandArgument);
            menuItem.Delete(ref validationErrors);
            Master.ValidationErrors = validationErrors;
            CustomGridView1.DataBind();
        }
    }
}