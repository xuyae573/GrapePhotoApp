using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Web.Models.Account
{
    public class User
    {
        public int Id { get; set; }
        /// <summary>
        /// Username , unique key field
        /// </summary>
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicUrl { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female
    }
}
