using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vien.Framework.Application;
using Vien.Framework.Application.UI;

namespace Vien.Framework.Web
{
    public partial class AccountList : BasePage
    {
        private const int COL_INDEX_ACTION = 4;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.AddButton_Click += new EditGrid.ButtonClickedHandler(Master_AddButton_Click);
            Master.SearchButton_Click += new EditGrid.ButtonClickedHandler(Master_SearchButton_Click);

            if (!IsPostBack)
            {
                CustomGridView1.ListClassName = typeof(UserAccountModelList).AssemblyQualifiedName;
                CustomGridView1.LoadMethodName = "Load";

                //Name
                CustomGridView1.AddBoundField("WindowAccountName", "Tài khoản", "WindowAccountName");
                CustomGridView1.AddBoundField("FullName", "Họ tên", "FullName");
                CustomGridView1.AddBoundField("Email", "Email", "Email");
                CustomGridView1.AddCheckBoxField("IsActive", "Kích hoạt", "IsActive");
            
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
                        CustomGridView1.LoadMethodName = "Load";
                        //Rebind the grid so the delete event is captured.
                        CustomGridView1.DataBind();
                    }
                }
            }
        }

        private void Master_SearchButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Master_AddButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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

                editLink.NavigateUrl = "EditAccount.aspx" + EncryptQueryString("id=" +
                    ((UserAccountModel)e.Row.DataItem).Id.ToString());
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);
                if (ReadOnly == false)
                {
                    LiteralControl lc = new LiteralControl(" &nbsp; ");
                    e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                    // Add the Delete link
                    LinkButton lbtnDelete = new LinkButton();
                    lbtnDelete.ID = "lbtnDelete" + ((UserAccountModel)e.Row.DataItem).Id.ToString();
                    lbtnDelete.Text = "Xóa";
                    lbtnDelete.CssClass = "btn btn-danger";
                    lbtnDelete.CommandArgument = ((UserAccountModel)e.Row.DataItem).Id.ToString();
                    lbtnDelete.OnClientClick = "return ConfirmDelete();";
                    lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                    e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
                }
            }
        }

        private void lbtnDelete_Command(object sender, CommandEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override string MenuItemName()
        {
            return "Accounts";
        }
    }
}