using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Web.Models
{
    public class UsersPicture
    {
        public UsersPicture()
        {
            Photos = new List<Picture>();
        }
        public string UserName { get; set; }
        public List<Picture> Photos { get; set; } 
    }
}
