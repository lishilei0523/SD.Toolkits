using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SD.Toolkits.SessionSharing.SiteSlave.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            const string sessionKey = "SessionSharing";
            string sessionResult = HttpContext.Session.GetString(sessionKey);
            base.ViewBag.Session = sessionResult;

            return base.View();
        }
    }
}
