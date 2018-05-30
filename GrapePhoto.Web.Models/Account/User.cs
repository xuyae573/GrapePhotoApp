using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Web.Models.Account
{
    public class User : IdentityUser
    {
        public User()
        {
        }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string AvatarPicUrl { get; set; }

      
    }

  
}
