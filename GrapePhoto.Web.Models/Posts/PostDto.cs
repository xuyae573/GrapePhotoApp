using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Web.Models
{
    public class PostDto
    {
        [JsonProperty("PostId")] 
        public string Id { get; set; }

        [JsonProperty("Note")]
        public string Title { get; set; }

        public string ImgUrl { get; set; }

        public string ThumbUrl { get; set; }

        public string UserId { get; set; }

        [JsonProperty("TimeStamp")]
        public DateTime PostDate { get; set; }

        public int LikeCount { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int Liked { get; set; }
    }
}
