using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GrapePhoto.Web.Models.Account;

namespace GrapePhoto.Web.Models
{
    public class Picture
    {
        public string Id { get; set; }

        public string ThumbnailSrc { get; set; }
        //Full HD
        public string Src { get; set; }

        public int LikeCount { get; set; }

        public string UserId { get; set; }

        public DateTime PostDate { get; set; }

        public Stream PictureStream{ get; set; }
        public byte[] Bytes { get; set; }

        public string MimeType { get; set; }

        public string AltAttribute { get; set; }
 
        public string TitleAttribute { get; set; }

        public string S3FileName { get; set; }
    }
}
