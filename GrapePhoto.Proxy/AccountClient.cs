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

        public SignInResult SignIn(User user)
        {
            var client = new RestClient(_baseUri);
            var request = new RestRequest(AccountAPI.SignIn);
      
            request.AddJsonBody(user);
            IRestResponse response = client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);

            return new SignInResult()
            {
                Succeed = json.success,
                ErrorMessage = json.error
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
