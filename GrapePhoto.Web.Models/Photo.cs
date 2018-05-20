using System;
using System.Collections.Generic;
using System.Text;
using GrapePhoto.Web.Models.Account;

namespace GrapePhoto.Web.Models
{
    public class Photo
    {
        public string Caption { get; set; }
        public string ThumbnailSrc { get; set; }
        //Full HD
        public string Src { get; set; }
        public Dimensions Dimensions { get; set; }
        public int LikeCount { get; set; }
        public User Owner { get; set; }

        public DateTime PostDate { get; set; }
    }
}
