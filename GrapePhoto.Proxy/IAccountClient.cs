
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

        List<User> GetAllFollowingUsersByUserId(string userId);

        List<User> GetAllFollowersUsersByUserId(string userId);

        User UpdateProfile(User user);

        List<User> SerachUsers(string keyword);

        void FollowOtherUser(string userId, string otherUserId);

        void UnfollowOtherUser(string userId, string otherUserId);

        List<User> GetPopularUsers(int returnRecordsCount);

    }

}
