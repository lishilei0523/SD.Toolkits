using System;
using System.Threading.Tasks;
using System.Web;

namespace SD.Toolkits.HttpContextReader.Tests
{
    public partial class Default : System.Web.UI.Page
    {
        private const string Key = "Key";

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session[Key] = "Hello World";
        }

        protected void NormallyRead(object sender, EventArgs e)
        {
            string text = Task.Run<string>(() =>
            {
                string value = HttpContext.Current?.Session[Key].ToString();
                return value;
            }).Result;

            this.Txt_Result.Text = text;
        }

        protected void CorrectlyRead(object sender, EventArgs e)
        {
            string text = Task.Run<string>(() =>
            {
                string value = HttpContextReader.Current.Session[Default.Key].ToString();
                return value;
            }).Result;

            this.Txt_Result.Text = text;
        }
    }
}