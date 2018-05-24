using GrapePhoto.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapePhoto.Models
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Posts = new List<PostViewModel>();
            FollowingUsers = new List<User>();
        }
        public List<PostViewModel> Posts { get; set; }

        public List<User> FollowingUsers { get; set; }
    }
}
