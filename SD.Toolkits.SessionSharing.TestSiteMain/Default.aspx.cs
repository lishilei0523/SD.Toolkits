using System;
using System.Web.UI;

namespace SD.Toolkits.SessionSharing.TestSiteMain
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["SessionSharing5"] = "Hello world2";
            this.Response.Write(this.Session["SessionSharing5"]);
        }
    }
}