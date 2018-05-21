using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GrapePhoto.Controllers
{
    public class ProfileController : Controller
    {
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


        public IActionResult ChangePassword()
        {
            return View();
        }


    }
}