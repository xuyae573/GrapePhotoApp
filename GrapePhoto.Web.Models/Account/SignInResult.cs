using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Web.Models.Account
{
    public class SignInResult
    {
        public bool Succeed { get; set; }

        public string ErrorMessage { get; set; }
    }
}
