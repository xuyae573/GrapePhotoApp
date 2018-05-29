
using GrapePhoto.Web.Models;
using GrapePhoto.Web.Models.Account;
using GrapePhoto.Web.Models.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Proxy
{
    public interface IPostService
    { 
        PostDto AddPost(PostDto postDto);
        PostDto LikePost(LikePostDto likePostDto);
        PostDto UnlikePost(LikePostDto likePostDto);

        IPagedList<PostDto> GetFollowingPostsByUserId(string userId, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<PostDto> GetUserPostsByUserId(string userId, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<PostDto> GetPosts(string userId, int pageIndex = 0, int pageSize = int.MaxValue);



    }

}
