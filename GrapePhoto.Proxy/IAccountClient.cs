
using GrapePhoto.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Proxy
{
    public interface IAccountClient
    {
        SignUpResult SignUp(SignUpViewModel signUpViewModel);

        SignInResult SignIn(SignInViewModel user);
 

        User GetUserByUserId(string userId);

        List<User> GetAllFollowingUsersByUserName(string userid);
    }

}
