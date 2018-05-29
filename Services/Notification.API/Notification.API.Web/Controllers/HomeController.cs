using Microsoft.AspNetCore.Mvc;

namespace Notification.API.Web.Controllers
{
    public class HomeController : APIControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }

       
    }
}