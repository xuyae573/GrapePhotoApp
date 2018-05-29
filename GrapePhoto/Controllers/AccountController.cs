using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GrapePhoto.Extensions;
using GrapePhoto.Proxy;
using GrapePhoto.Web.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrapePhoto.Controllers
{
    public class AccountController : Controller
    {
        private IAccountClient _accountClient;

        public AccountController(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }
 
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignInViewModel model,string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
 
            if (ModelState.IsValid)
            {
                var result = _accountClient.SignIn(model);
                if (result.Succeed)
                {
                    var claimsIdentity = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, model.UserId) }, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                }
            }
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(SignUpViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = _accountClient.SignUp(model);

                //SignIn and set session User
                if (result.Succeed)
                {
                    var claimsIdentity = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, model.UserId) }, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


    }
}
