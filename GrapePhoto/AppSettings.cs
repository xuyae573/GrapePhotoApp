using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapePhoto
{
    public class AppSettings
    {
        public string PhotoUrl { get; set; }

        public string IdentityUrl { get; set; }

        public string NotificationUrl { get; set; }

    }

    public class Consts
    {
        public const string AuthSchemes =
          CookieAuthenticationDefaults.AuthenticationScheme;
    }
}
