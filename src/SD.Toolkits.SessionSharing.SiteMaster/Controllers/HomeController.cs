using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SD.Toolkits.SessionSharing.SiteMaster.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            const string sessionKey = "SessionSharing";
            const string sessionValue = "Hello world";

            HttpContext.Session.SetString(sessionKey, sessionValue);

            string sessionResult = HttpContext.Session.GetString(sessionKey);
            base.ViewBag.Session = sessionResult;

            return base.View();
        }
    }
}
