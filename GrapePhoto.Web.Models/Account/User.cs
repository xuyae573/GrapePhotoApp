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

        [JsonProperty("UserId")]
        public string UserId { get; set; }
        /// <summary>
        /// Username , unique key field
        /// </summary>
        /// 
        [JsonProperty("UserName")]
        public new string UserName { get; set; }

        public string AvatarPicUrl { get; set; }

      
    }

  
}
