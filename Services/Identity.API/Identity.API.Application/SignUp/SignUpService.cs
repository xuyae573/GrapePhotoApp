using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.API.SignUp
{
    public class SignUpService : ApplicationService, ISignUpService
    {
        public UserDto SignUp(SignUpDto signUpDto)
        {
            //todo: call database insert records

            return new UserDto()
            {
                Email = signUpDto.Email,
                FullName = signUpDto.FullName,
                ProfilePicUrl = string.Empty,
                PhoneNumber = signUpDto.PhoneNumber,
                UserName = signUpDto.UserName
            };
        }
    }
}
