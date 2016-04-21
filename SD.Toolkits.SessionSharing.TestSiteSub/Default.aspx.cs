using System;
using System.Web.UI;

namespace SD.Toolkits.SessionSharing.TestSiteSub
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Write(this.Session["SessionSharing"]);
        }
    }
}