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
            var user = new User();
            user = _accountClient.GetUserByUserId(HttpContext.User.Identity.Name);
            return View(user);
        }


        [HttpPost]
        public IActionResult Edit(GrapePhoto.Web.Models.Account.User model)
        {
            if (ModelState.IsValid)
            {
                var UserId = HttpContext.User.Identity.Name;
                var user = _accountClient.GetUserByUserId(UserId);
                user.Email = model.Email;
                user.UserName = model.UserName;
                _accountClient.UpdateProfile(user);
                ViewBag.Message = "Save profile successful";
            }
            return View(model);
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
                var result =  _accountClient.SignIn(new SignInViewModel()
                {
                    UserId = HttpContext.User.Identity.Name,
                    Password = model.OldPassword
                });

                if (result.Succeed)
                {
                    User user = new User
                    {
                        UserId = HttpContext.User.Identity.Name,
                        Password = model.NewPassword
                    };
                    _accountClient.UpdateProfile(user);

                    ViewBag.Message = "Change password successful";
                }
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
            }
            return View(model);
        }
    }
}