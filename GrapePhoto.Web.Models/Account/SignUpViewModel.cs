using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrapePhoto.Web.Models.Account
{
    public class SignUpViewModel
    {
       
        [Required]
        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [JsonProperty("userid")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [JsonProperty("username")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [JsonProperty("pwd")]
        public string Password { get; set; }
 
    }
}
