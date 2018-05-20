using Microsoft.AspNetCore.Mvc;

namespace Photo.API.Web.Controllers
{
    public class HomeController : APIControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }

      
    }
}