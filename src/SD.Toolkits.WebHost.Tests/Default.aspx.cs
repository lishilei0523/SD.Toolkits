using SD.Toolkits.WebHost.Extensions;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace SD.Toolkits.WebHost.Tests
{
    public partial class Default : Page
    {
        private const string Key = "Key";

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session[Key] = "Hello World";
        }

        protected async void NormallyRead(object sender, EventArgs e)
        {
            string text = await Task.Run(() =>
             {
                 string value = HttpContext.Current?.Session[Key].ToString();
                 return value;
             });

            this.Txt_Result.Text = text;
        }

        protected async void CorrectlyRead(object sender, EventArgs e)
        {
            string text = await Task.Run(() =>
            {
                string value = HttpContextReader.Current.Session[Key].ToString();
                return value;
            });

            this.Txt_Result.Text = text;
        }
    }
}