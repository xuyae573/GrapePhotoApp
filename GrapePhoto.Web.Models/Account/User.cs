using Microsoft.AspNetCore.Identity;
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
        /// <summary>
        /// Username , unique key field
        /// </summary>
        public string FullName { get; set; }
        public string AvatarPicUrl { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female
    }
}
