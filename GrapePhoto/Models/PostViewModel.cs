using GrapePhoto.Web.Models;
using GrapePhoto.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapePhoto.Models
{
    public class PostViewModel
    {
        public Picture Picture { get; set; }
        public User User { get; set; }
        public bool IsLike { get; set; }
    }
}
