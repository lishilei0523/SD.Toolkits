using System;
using System.Web.UI;

namespace SD.Toolkits.SessionSharing.TestSiteMain
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["SessionSharing"] = "Hello world";
            this.Response.Write(this.Session["SessionSharing"]);
        }
    }
}