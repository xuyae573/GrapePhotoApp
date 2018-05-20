using Abp.Application.Services;
using Abp.Domain.Entities;
using Identity.API.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.API.SignIn
{
    public class SignInService : ApplicationService, ISignInService
    {
        public UserDto SignIn(UserDto user)
        {
            if(user.UserName == "xuya" && user.PasswordHash == "123")
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
