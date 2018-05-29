using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.API.SignIn
{
    public interface ISignInService : IApplicationService
    {
        UserDto SignIn(UserDto user);
    }
}
