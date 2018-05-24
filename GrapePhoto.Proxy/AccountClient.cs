using System;
using System.Collections.Generic;
using System.Text;
using GrapePhoto.Web.Models.Account;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GrapePhoto.Web.Models;

namespace GrapePhoto.Proxy
{
    public static class AccountAPI
    {
        public static string SignIn => $"api/account/signin";
        public static string SignUp => $"api/account/signup";
    }

    public class AccountClient : IAccountClient
    {
        private string _baseUri;

        public AccountClient(string baseUri)
        {
            _baseUri = baseUri;
        }

        public List<User> GetAllFollowingUsersByUserName(string username)
        {
            var userId = GetUserIdByUsername(username).Id;
            //callAPI get all following users;

            var followingUsers = new List<User>();

            followingUsers.Add(new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Txxni",
                AvatarPicUrl = "https://scontent-sin6-2.cdninstagram.com/vp/3086c8b2f4efef61ad662a821c60e09d/5B8153DF/t51.2885-19/s150x150/29403266_193822707890209_7859898672618668032_n.jpg",
                Gender = Gender.Female,
            });

            return followingUsers;

        }

        public User GetUserByUserId(string userId)
        {
            return new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Txxni",
                AvatarPicUrl = "https://scontent-sin6-2.cdninstagram.com/vp/3086c8b2f4efef61ad662a821c60e09d/5B8153DF/t51.2885-19/s150x150/29403266_193822707890209_7859898672618668032_n.jpg",
                Gender = Gender.Female,
            };
        }

        public User GetUserIdByUsername(string username)
        {
            return new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "My Name",
                AvatarPicUrl = "https://scontent-sin6-2.cdninstagram.com/vp/3086c8b2f4efef61ad662a821c60e09d/5B8153DF/t51.2885-19/s150x150/29403266_193822707890209_7859898672618668032_n.jpg",
                Gender = Gender.Male,
            };
        }
 
        public SignInResult SignIn(User user)
        {
          //  var client = new RestClient(_baseUri);
           // var request = new RestRequest(AccountAPI.SignIn);
      
           // request.AddJsonBody(user);
           // IRestResponse response = client.Post(request);
            //var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);

            return new SignInResult()
            {
                Succeed = true,
                ErrorMessage = ""
            };
        }

        public User SignUp(SignUpViewModel signUpViewModel)
        {
            var client = new RestClient(_baseUri);
            var request = new RestRequest(AccountAPI.SignUp);
            request.AddJsonBody(signUpViewModel);
            IRestResponse response = client.Execute(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            var user = JsonConvert.DeserializeObject<User>(json.result.ToString());
            return user;
        }
    }
}
