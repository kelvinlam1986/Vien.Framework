using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using MinhLam.Framework.Application;

namespace MinhLam.Framework.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ValidationErrorMessages runat=server></{0}:ValidationErrorMessages>")]
    public class ValidationErrorMessages : WebControl
    {
        [Bindable(false),
        Browsable(false)]
        public ValidationErrors ValidationErrors { get; set; }

        public ValidationErrorMessages()
        {
            ValidationErrors = new ValidationErrors();
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            // Show all the messages in ENTValidationErrors

            if (ValidationErrors.Count != 0)
            {
                //gets the controls from the page
                Panel pannelMessage = new Panel();
                foreach (var ve in ValidationErrors)
                {
                    LiteralControl labelMessage = new LiteralControl();

                    //sets the message and the type of alert, than displays the message
                    labelMessage.Text = $"<p>{ve.ErrorMessage}</p>";
                    pannelMessage.CssClass = string.Format("alert alert-danger alert-dismissable");
                    pannelMessage.Attributes.Add("role", "alert");
                    pannelMessage.Visible = true;
                    pannelMessage.Controls.Add(labelMessage);
                    labelMessage = null;
                }

                pannelMessage.RenderControl(output);
                pannelMessage = null;
            }
            else
            {
                output.Write("");
            }
        }
    }
}
