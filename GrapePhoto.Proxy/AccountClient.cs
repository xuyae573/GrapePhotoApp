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
        public static string SignIn => $"/user/login";
        public static string SignUp => $"/user/register";
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
            };
        }

        public User GetUserIdByUsername(string username)
        {
            return new User()
            {
                Id = username
            };
        }
 
        public SignInResult SignIn(User user)
        {
            var client = new RestClient(_baseUri);
            var request = new RestRequest(AccountAPI.SignIn);
            request.AddJsonBody(JsonConvert.SerializeObject(user));
            //request.AddQueryParameter("userid", user.UserName);
            //request.AddQueryParameter("pwd", user.PasswordHash);
       
            IRestResponse response = client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);

            return new SignInResult()
            {
                Succeed = json.success,
                ErrorMessage = json.error
            };
        }

        public SignUpResult SignUp(SignUpViewModel signUpViewModel)
        {
             
            var request = new RestRequest(AccountAPI.SignUp);
            var client = new RestClient(_baseUri);
            request.AddJsonBody(JsonConvert.SerializeObject(signUpViewModel));
            //request.AddHeader("userid", signUpViewModel.UserName);
            //request.AddHeader("pwd", signUpViewModel.Password);
            //request.AddHeader("username", signUpViewModel.FullName);
            //request.AddHeader("email", signUpViewModel.Email);
            var user = new User();
            IRestResponse response = client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                user = new User()
                {
                    FullName = signUpViewModel.FullName,
                    PasswordHash = signUpViewModel.Password,
                    Email = signUpViewModel.Email,
                    UserName = signUpViewModel.UserName
                }; 
              
            }
          
            return new SignUpResult()
            {
                Succeed = json.success,
                ErrorMessage = json.error,
                user = user
            };
        }
    }
}
