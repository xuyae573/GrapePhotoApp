using System;
using System.Collections.Generic;
using System.Text;
using GrapePhoto.Web.Models.Account;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public User SignUp(SignUpViewModel signUpViewModel)
        {
            var client = new RestClient(_baseUri);
            var request = new RestRequest(AccountAPI.SignUp);
            request.AddBody(signUpViewModel);
            IRestResponse response = client.Execute(request);
            var json = JObject.Parse(response.Content);
            var userJson = json["result"];
            var user = JsonConvert.DeserializeObject<User>(userJson.ToString());
            return user;
        }
    }
}
