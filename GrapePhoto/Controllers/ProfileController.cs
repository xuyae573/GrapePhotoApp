using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrapePhoto.Models;
using GrapePhoto.Proxy;
using GrapePhoto.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace GrapePhoto.Controllers
{
    public class ProfileController : Controller
    {
        private IAccountClient _accountClient;
        private IPostService _postService;

        public ProfileController(IAccountClient accountClient, IPostService postService)
        {
            _accountClient = accountClient;
            _postService = postService;
        }
        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = HttpContext.User.Identity.Name;
            }
            var indexModel = new ProfileIndexModel(_accountClient);
            indexModel.User = _accountClient.GetUserByUserId(id);
            indexModel.Posts = _postService.GetUserPostsByUserId(id);
            indexModel.SetRelationship(HttpContext.User.Identity.Name);
            return View(indexModel);
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