namespace GrapePhoto.Controllers
{
    using GrapePhoto.Helper;
    using GrapePhoto.Models;
    using GrapePhoto.Models.Consts;
    using GrapePhoto.Proxy;
    using GrapePhoto.Web.Models;
    using GrapePhoto.Web.Models.Account;
    using GrapePhoto.Web.Models.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Linq;

    [Authorize]
    public class HomeController : Controller
    {
        private IPictureService _pictureService;
        private IPostService _postService;
        private IAccountClient _accountService;

        public HomeController(IPictureService pictureService, IAccountClient client, IPostService postService)
        {
            this._pictureService = pictureService;
            this._accountService = client;
            this._postService = postService;
        }

        public IActionResult Error()
        {
            ErrorViewModel model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? base.HttpContext.TraceIdentifier
            };
            return this.View(model);
        }

        public IActionResult Explore()
        {
            List<PostViewModel> allPictures = this.GetAllPictures(0);
            IndexViewModel model = new IndexViewModel
            {
                Posts = allPictures
            };
            return this.View(model);
        }
   
        [HttpPost]
        public JsonResult GetExploreJson(int pageIndex) =>
            this.Json(this.GetAllPictures(pageIndex));


        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel
            {
                Posts = this.GetRecentFollowingPosts(0),
                FollowingUsers = this._accountService.GetAllFollowingUsersByUserId(base.HttpContext.User.Identity.Name)
            };
            return this.View(model);
        }

        [HttpPost]
        public JsonResult GetPosts(int pageIndex)
        {
            List<PostViewModel> recentFollowingPosts = this.GetRecentFollowingPosts(pageIndex);
            return this.Json(recentFollowingPosts);
        }
      

        #region Prepare ViewModel
        private List<PostViewModel> GetAllPictures(int pageIndex)
        {
            List<PostViewModel> list = new List<PostViewModel>();
            var userid = HttpContext.User.Identity.Name;
            var posts = this._postService.GetPosts(userid, pageIndex, PaginationConsts.PageSize);
            PreparePostViewModel(list, posts);
            return list;
        }
        private List<PostViewModel> GetRecentFollowingPosts(int page)
        {
            string userid = this.HttpContext.User.Identity.Name;
            List<PostViewModel> list = new List<PostViewModel>();
            var followingPosts = this._postService.GetFollowingPostsByUserId(userid, page, PaginationConsts.PageSize);
            PreparePostViewModel(list, followingPosts);
            return list;
        }
        private void PreparePostViewModel(List<PostViewModel> list, IPagedList<PostDto> posts)
        {
            foreach (var post in posts)
            {
                PostViewModel model1 = new PostViewModel
                {
                    Picture = new Picture()
                    {
                        Src = post.ImgUrl,
                        ThumbnailSrc = post.ThumbUrl,
                        TitleAttribute = post.Title,
                        PostDate = post.PostDate,
                        LikeCount = post.LikeCount,
                        Width = post.Width,
                        Height = post.Height,
                        Id = post.Id,
                        UserId = post.UserId,
                        AltAttribute = post.Title
                    },
                    User = this._accountService.GetUserByUserId(post.UserId),
                    IsLike = post.Liked == 1
                };
                list.Add(model1);
            }
        }

    
        #endregion 
    }
}
