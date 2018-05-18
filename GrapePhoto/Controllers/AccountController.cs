using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrapePhoto.Application;
using GrapePhoto.Domain;
using GrapePhoto.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrapePhoto.Controllers
{
    public class AccountController : Controller
    {

        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                 _accountService.Insert(user);
                //if (result.Succeeded)
                //{

                //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                //    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                //    await _signInManager.SignInAsync(user, isPersistent: false);
                //    _logger.LogInformation("User created a new account with password.");
                //    return RedirectToLocal(returnUrl);
                //}
                //AddErrors(result);
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



    }
}
