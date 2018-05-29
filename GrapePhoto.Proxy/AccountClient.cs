using System;
using System.Collections.Generic;
using System.Text;
using GrapePhoto.Web.Models.Account;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GrapePhoto.Web.Models;
using RestSharp.Serializers.Newtonsoft.Json;
using System.Linq;

namespace GrapePhoto.Proxy
{
    public static class AccountAPI
    {
        public static string SignIn => $"/user/login";
        public static string SignUp => $"/user/register";

        public static string UpdateUser => $"/user/update";
        public static string SearchUsers => $"/user/search";

        public static string FollowingUser => $"/user/follow";

        public static string Followee => $"/user/followee";
        public static string Followers => $"/user/follower";
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

        public void FollowOtherUser(string userId, string otherUserId)
        {
            var request = new RestSharp.RestRequest(AccountAPI.SearchUsers)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            var user = new User { UserName = userId };

            request.AddJsonBody(new
            {
                Key = "UserId",
                Value = userId
            });

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
          
        }

        public List<User> GetAllFollowersUsersByUserId(string userId)
        {
            var request = new RestSharp.RestRequest(AccountAPI.Followers)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            var user = new User { UserName = userId };
            request.AddJsonBody(new
            {
               UserId = userId
            });

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(json.result.ToString());
                return users;
            }
            else
            {
                return new List<User>();
            }
        }

        public List<User> GetAllFollowingUsersByUserId(string userId)
        {
            //callAPI get all following users;

            var followingUsers = new List<User>();
 
            var request = new RestSharp.RestRequest(AccountAPI.Followee)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };

            request.AddJsonBody(new
            {
                UserId = userId
            });

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                followingUsers = JsonConvert.DeserializeObject<List<User>>(json.result.ToString());
            }

            return followingUsers;
        }

        public User GetUserByUserId(string userId)
        {
            var request = new RestSharp.RestRequest(AccountAPI.SearchUsers)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            var user = new User { UserName = userId };

            request.AddJsonBody(new
            {
                Key = "UserId",
                Value = userId
            });
             
            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(json.result.ToString());
                return users.First();
            }
            else
            {
                return null;
            }
        }

        public List<User> SerachUsers(string keyword)
        {
            var request = new RestSharp.RestRequest(AccountAPI.SearchUsers)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };

            request.AddJsonBody(new
            {
                Key = "UserName",
                Value = keyword     
            });

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(json.result.ToString());
                return users;
            }
            else
            {
                return new List<User>();
            }
        }

        public SignInResult SignIn(SignInViewModel user)
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
            var user = new User();
            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                user = new User()
                {
                    UserName = signUpViewModel.UserName,
                    PasswordHash = signUpViewModel.Password,
                    Email = signUpViewModel.Email,
                    UserId = signUpViewModel.UserId
                }; 
              
            }
          
            return new SignUpResult()
            {
                Succeed = json.success,
                ErrorMessage = json.error,
                user = user
            };
        }

        public void UnfollowOtherUser(string userId, string otherUserId)
        {
            throw new NotImplementedException();
        }

        public User UpdateProfile(User user)
        {
            var request = new RestSharp.RestRequest(AccountAPI.UpdateUser)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            request.AddJsonBody(user);

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            
            return user;
        }
    }
}
