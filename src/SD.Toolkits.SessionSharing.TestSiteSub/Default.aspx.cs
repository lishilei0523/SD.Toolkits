using System;
using System.Web.UI;

namespace SD.Toolkits.SessionSharing.TestSiteSub
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /***
             * 测试说明：
             * 主站以SessionSharing为键将"Hello world"存入Session，
             * 子站以SessionSharing为键读取Session，
             * 子站如果可以读到主站存放的Session，
             * 则说明两站点Session共享成功。
             * **/

            this.Response.Write(this.Session["SessionSharing"]);
        }
    }
}