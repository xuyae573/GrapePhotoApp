using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Web.Models
{
    public class UsersPhotos
    {
        public UsersPhotos()
        {
            Photos = new List<Photo>();
        }
        public string UserName { get; set; }
        public List<Photo> Photos { get; set; } 
    }
}
