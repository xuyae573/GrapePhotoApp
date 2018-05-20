using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.API.SignUp
{
    public interface ISignUpService : IApplicationService
    {
        UserDto SignUp(SignUpDto signUpDto);
    }
}
