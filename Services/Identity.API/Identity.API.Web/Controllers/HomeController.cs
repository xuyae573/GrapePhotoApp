using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Web.Controllers
{
    public class HomeController : APIControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}