using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Proxy
{
    public static class AccountAPI
    {
        public static string SignIn(string baseUri) => $"{baseUri}/api/signin";
        public static string Register(string baseUri) => $"{baseUri}/api/register";
    }

    public class AccountClient : IAccountClient
    {
        private string _baseUri;

        public AccountClient(string baseUri)
        {
            _baseUri = baseUri;
        }



    }
}
