using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrapePhoto.Web.Models.Account
{
    public class SignInViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [JsonProperty("userid")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty("pwd")]
        public string Password { get; set; }
    }
}
