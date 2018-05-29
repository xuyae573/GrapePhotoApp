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
        [Display(Name = "UserId")]
        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
