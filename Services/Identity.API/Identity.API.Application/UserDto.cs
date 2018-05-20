using Abp.AutoMapper;
using Identity.API.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.API
{
    [AutoMap(typeof(ApplicationUser))]
    public class UserDto
    {
        public virtual string FullName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserName { get; set; }
        public string ProfilePicUrl { get; set; }
        public int Gender { get; set; }
    }
}
