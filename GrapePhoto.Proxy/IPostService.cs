
using GrapePhoto.Web.Models;
using GrapePhoto.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Proxy
{
    public interface IPostService
    { 
        PostDto AddPost(PostDto postDto);
    }

}
