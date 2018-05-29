using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrapePhoto.Proxy;
using Microsoft.AspNetCore.Mvc;

namespace GrapePhoto.Controllers
{
    public class FollowersController : Controller
    {
        private IAccountClient _accountClient;
        public FollowersController(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllFollowers()
        {
            var userid = HttpContext.User.Identity.Name;
            var result = _accountClient.GetAllFollowersUsersByUserId(userid);
            return Json(result);
        }
    }
}