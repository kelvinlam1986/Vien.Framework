using MinhLam.Framework.Application;
using MinhLam.Framework.Application.UI;
using MinhLam.Framework.Web;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vien.Framework.Web
{
    public partial class RoleList : BasePage
    {
        private const int COL_INDEX_NAME = 0;
        private const int COL_INDEX_ACTION = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.AddButton_Click += new EditGrid.ButtonClickedHandler(Master_AddButton_Click);
            Master.SearchButton_Click += new EditGrid.ButtonClickedHandler(Master_SearchButton_Click);
            if (!IsPostBack)
            {
                cgvRoles.ListClassName = typeof(RoleModelList).AssemblyQualifiedName;
                cgvRoles.LoadMethodName = "Load";

                cgvRoles.AddBoundField("DisplayText", "Tên vai trò", "DisplayText");

                // Action column - Contains the Edit link
                cgvRoles.AddBoundField("", "Thao tác", "");
                cgvRoles.DataBind();
            }
            else
            {
                if (Page.Request.Form["__EVENTTARGET"] != null)
                {
                    string eventTarget = Page.Request.Form["__EVENTTARGET"].ToString();
                    if (eventTarget.IndexOf("lbtnDelete") > -1)
                    {
                        cgvRoles.LoadMethodName = "Load";
                        //Rebind the grid so the delete event is captured.
                        cgvRoles.DataBind();
                    }
                }
            }
        }

        protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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

                editLink.NavigateUrl = "EditRole.aspx" + EncryptQueryString("id=" +
                    ((RoleModel)e.Row.DataItem).Id.ToString());
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);
                if (ReadOnly == false)
                {
                    LiteralControl lc = new LiteralControl(" &nbsp; ");
                    e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                    // Add the Delete link
                    LinkButton lbtnDelete = new LinkButton();
                    lbtnDelete.ID = "lbtnDelete" + ((RoleModel)e.Row.DataItem).Id.ToString();
                    lbtnDelete.Text = "Xóa";
                    lbtnDelete.CssClass = "btn btn-danger";
                    lbtnDelete.CommandArgument = ((RoleModel)e.Row.DataItem).Id.ToString();
                    lbtnDelete.OnClientClick = "return ConfirmDelete();";
                    lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                    e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
                }
            }
        }

        private void lbtnDelete_Command(object sender, CommandEventArgs e)
        {
            ValidationErrors validationErrors = new ValidationErrors();
            RoleModel roleModel = new RoleModel();
            roleModel.Id = Convert.ToInt32(e.CommandArgument);
            roleModel.Delete(ref validationErrors);
            Master.ValidationErrors = validationErrors;
            cgvRoles.DataBind();
        }

        private void Master_AddButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditRole.aspx" + EncryptQueryString("id=0"));
        }

        private void Master_SearchButton_Click(object sender, EventArgs e)
        {
            cgvRoles.ListClassName = typeof(RoleModelList).AssemblyQualifiedName;
            cgvRoles.LoadMethodName = "Search";
            cgvRoles.LoadMethodParameters.Add(this.Master.SearchString);
            cgvRoles.DataBind();
        }

        public override string MenuItemName()
        {
            return "Roles";
        }

        public override string[] CapabilityNames()
        {
            return new string[] { "Role" };
        }
    }
}