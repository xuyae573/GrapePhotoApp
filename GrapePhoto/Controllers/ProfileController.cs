using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrapePhoto.Proxy;
using GrapePhoto.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace GrapePhoto.Controllers
{
    public class ProfileController : Controller
    {
        private IAccountClient _accountClient;
        public ProfileController(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }
        public IActionResult Index()
        {
            //Get Username and followers, following 

            //load the posts by username

            return View();
        }


        public IActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = HttpContext.User.Identity.Name;
               _accountClient.UpdateProfile(user);
            }
            return View(user);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModels model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}