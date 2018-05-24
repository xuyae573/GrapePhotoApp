
using GrapePhoto.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Proxy
{
    public interface IAccountClient
    {
        User SignUp(SignUpViewModel signUpViewModel);

        SignInResult SignIn(User user);

        User GetUserIdByUsername(string username);

        User GetUserByUserId(string userId);

        List<User> GetAllFollowingUsersByUserName(string username);
    }

}
