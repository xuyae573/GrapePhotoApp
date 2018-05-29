using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Web.Models.Posts
{
    public class LikePostDto
    {
        public string UserId { get; set; }
        public string PostId { get; set; }

        public int Like
        {
            get
            {
                return IsLike ? 1 : 0;
            }
        }

        public bool IsLike { get; set; }
    }
}
