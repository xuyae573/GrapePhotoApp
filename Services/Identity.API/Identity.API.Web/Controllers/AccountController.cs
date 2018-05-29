using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Web.Models;
using Identity.API.SignIn;
using Identity.API.SignUp;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : AbpController
    {
        private ISignInService _signInService;
        private ISignUpService _signUpService;
        public AccountController(ISignInService signInService, ISignUpService signUpService)
        {
            _signInService = signInService;
            _signUpService = signUpService;
        }
     
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

 
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public JsonResult SignIn([FromBody]UserDto user)
        {
            var result = _signInService.SignIn(user);
            if (result == null)
                return Json(new AjaxResponse()
                {
                    Success = false,
                    Result = null,
                    Error = new ErrorInfo() { Message="LoginFailed" }
                });
            return Json(result);
        }


        [HttpPost]
        public UserDto SignUp([FromBody]SignUpDto user)
        {
            var result = _signUpService.SignUp(user);
            return result;
        }



        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



    }
}
