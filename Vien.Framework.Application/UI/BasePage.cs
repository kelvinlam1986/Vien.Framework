using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vien.Framework.Application.UI
{
    public abstract class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // CheckCapabilities();
        }

        #region Properities

        public bool ReadOnly { get; set; }

        // public bool IgnoreCapabilityCheck { get; set; }

        //public ENTUserAccountEO CurrentUser
        //{
        //    get
        //    {
        //        return Globals.GetUsers(this.Cache).GetByWindowAccountName(this.User.Identity.Name);
        //    }
        //}

        #endregion Properties

        public abstract string MenuItemName();

        // public abstract string[] CapabilityNames();

        public static string RootPath(HttpContext context)
        {
            string urlSuffix = context.Request.Url.Authority +
                context.Request.ApplicationPath;
            return context.Request.Url.Scheme + @"://" + urlSuffix + "/";
        }

        public static NameValueCollection DecryptQueryString(string queryString)
        {
            return StringHelpers.DecryptQueryString(queryString);
        }
        public static string EncryptQueryString(NameValueCollection queryString)
        {
            return StringHelpers.EncryptQueryString(queryString);
        }
        public static string EncryptQueryString(string queryString)
        {
            return StringHelpers.EncryptQueryString(queryString);
        }

        //public virtual void CheckCapabilities()
        //{
        //    if (IgnoreCapabilityCheck == false)
        //    {
        //        foreach (var capabilityName in CapabilityNames())
        //        {
        //            // Check if user has the capability to view this screen
        //            ENTCapabilityBO capability = Globals.GetCapabilities(this.Cache).GetByName(capabilityName);
        //            if (capability == null)
        //            {
        //                throw new Exception("Security is not enabled for this page. " + this.ToString());
        //            }
        //            else
        //            {
        //                switch (CurrentUser.GetCapabilityAccess(capability.ID,
        //                    Globals.GetRoles(this.Cache)))
        //                {
        //                    case ENTRoleCapabilityEO.CapabilityAccessFlagEnum.None:
        //                        NoAccessToPage(capabilityName);
        //                        break;
        //                    case ENTRoleCapabilityEO.CapabilityAccessFlagEnum.ReadOnly:
        //                        MakeFormReadOnly(capabilityName, this.Controls);
        //                        break;
        //                    case ENTRoleCapabilityEO.CapabilityAccessFlagEnum.Edit:
        //                        break;
        //                    default:
        //                        throw new Exception("Unknown access for this screen. " + capability.CapabilityName);
        //                }
        //            }

        //            capability = null;
        //        }
        //    }
        //}

        protected void NoAccessToPage(string capabilityName)
        {
            throw new AccessViolationException("Bạn không có quyển truy cập trang này");
        }

        public virtual void MakeFormReadOnly(string capabilityName, ControlCollection controls)
        {
            ReadOnly = true;

        }

        public void MakeControlsReadOnly(ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Enabled = false;
                }
                else if (control is RadioButton)
                {
                    ((RadioButton)control).Enabled = false;
                }
                else if (control is DropDownList)
                {
                    ((DropDownList)control).Enabled = false;
                }
                else if (control is CheckBox)
                {
                    ((CheckBox)control).Enabled = false;
                }
                else if (control is RadioButtonList)
                {
                    ((RadioButtonList)control).Enabled = false;
                }

                if (control.HasControls())
                {
                    MakeControlsReadOnly(control.Controls);
                }
            }
        }

        public virtual void CustomReadOnlyLogic(string capabilityName)
        {
            // Override this method in a page that has custom logic
            // for non  standard controls on the screen
        }
    }
}
