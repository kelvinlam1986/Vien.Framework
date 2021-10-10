using System;
using MinhLam.Framework.Application.UI;

namespace Vien.Framework.Web
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            IgnoreCapabilityCheck = true;
            base.OnInit(e);
        }

        public override string MenuItemName()
        {
            return "Home";
        }

        public override string[] CapabilityNames()
        {
            return new string[] { "" };
        }
    }
}