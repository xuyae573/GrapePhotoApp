using System;
using System.Collections.Generic;
using System.Text;
using GrapePhoto.Web.Models.Account;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GrapePhoto.Web.Models;
using RestSharp.Serializers.Newtonsoft.Json;

namespace GrapePhoto.Proxy
{
    public static class AccountAPI
    {
        public static string SignIn => $"/user/login";
        public static string SignUp => $"/user/register";

        public static string UpdateUser => $"/user/update";
        public static string SearchUsers => $"/user/search";

        public static string Follow => $"/user/follow";
    }

    public class AccountClient : IAccountClient
    {
     
        private string _baseUri;
        private IRestClient _client;
        public AccountClient(string baseUri)
        {
            _baseUri = baseUri;
            _client = new RestClient(_baseUri);
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
            var request = new RestSharp.RestRequest(AccountAPI.SearchUsers)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            var user = new User { UserName = userId };
            request.AddJsonBody(user);
            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                user = JsonConvert.DeserializeObject<User>(json.result.ToString());
                return user;
                //return new User()
                //{
                //    UserName = userId,
                //    AvatarPicUrl = 
                //   // AvatarPicUrl = "https://scontent-sin6-2.cdninstagram.com/vp/3086c8b2f4efef61ad662a821c60e09d/5B8153DF/t51.2885-19/s150x150/29403266_193822707890209_7859898672618668032_n.jpg",
                //};
            }
            else
            {
                return null;
            }
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
            var request = new RestSharp.RestRequest(AccountAPI.SignIn)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            request.AddJsonBody(user);

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            return new SignInResult()
            {
                Succeed = json.success,
                ErrorMessage = json.error
            };
        }

        public SignUpResult SignUp(SignUpViewModel signUpViewModel)
        {

            var request = new RestSharp.RestRequest(AccountAPI.SignUp)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };

            request.AddJsonBody(signUpViewModel);
            //request.AddHeader("userid", signUpViewModel.UserName);
            //request.AddHeader("pwd", signUpViewModel.Password);
            //request.AddHeader("username", signUpViewModel.FullName);
            //request.AddHeader("email", signUpViewModel.Email);
            var user = new User();
            IRestResponse response = _client.Post(request);
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
