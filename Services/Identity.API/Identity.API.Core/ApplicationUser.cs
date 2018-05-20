using Abp.Domain.Entities;
using System;

namespace Identity.API.Core
{
    public class ApplicationUser : Entity<string>
    {
        //
        // Summary:
        //     Initializes a new instance of Microsoft.AspNetCore.Identity.IdentityUser`1.
        public ApplicationUser() { }
        //
        // Summary:
        //     Initializes a new instance of Microsoft.AspNetCore.Identity.IdentityUser`1.
        //
        // Parameters:
        //   userName:
        //     The user name.
        public ApplicationUser(string userName) {
            this.UserName = userName;
        }
       
        //
        // Summary:
        //     Gets or sets a telephone number for the user.
        public virtual string PhoneNumber { get; set; }

        //
        // Summary:
        //     Gets or sets a salted and hashed representation of the password for this user.
        public virtual string PasswordHash { get; set; }

        //
        // Summary:
        //     Gets or sets a salted and hashed representation of the password for this user.
        public virtual string Email { get; set; }
        //
        // Summary:
        //     Gets or sets the user name for this user.
        public virtual string UserName { get; set; }

        public string ProfilePicUrl { get; set; }
        public int Gender { get; set; }

    }
}
