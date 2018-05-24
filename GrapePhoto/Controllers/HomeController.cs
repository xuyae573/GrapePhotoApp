namespace GrapePhoto.Controllers
{
    using GrapePhoto.Models;
    using GrapePhoto.Models.Consts;
    using GrapePhoto.Proxy;
    using GrapePhoto.Web.Models;
    using GrapePhoto.Web.Models.Account;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [Authorize]
    public class HomeController : Controller
    {
        private IPictureService _pictureService;
        private IAccountClient _accountService;

        public HomeController(IPictureService pictureService, IAccountClient client)
        {
            this._pictureService = pictureService;
            this._accountService = client;
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

        private List<PostViewModel> GetAllPictures(int pageIndex)
        {
            List<PostViewModel> list = new List<PostViewModel>();
            var user = _accountService.GetUserIdByUsername(HttpContext.User.Identity.Name);

            var pictures = this._pictureService.GetPictures(user.Id, pageIndex, PaginationConsts.PageSize);
            foreach (Picture picture in pictures)
            {
                PostViewModel model1 = new PostViewModel
                {
                    Picture = picture,
                    User = this._accountService.GetUserByUserId(picture.UserId)
                };
                list.Add(model1);
            }
            return list;
        }

        [HttpPost]
        public JsonResult GetExploreJson(int pageIndex) =>
            this.Json(this.GetAllPictures(pageIndex));

        [HttpPost]
        public JsonResult GetPosts(int pageIndex)
        {
            List<PostViewModel> recentFollowingPosts = this.GetRecentFollowingPosts(pageIndex);
            return this.Json(recentFollowingPosts);
        }

        private List<PostViewModel> GetRecentFollowingPosts(int page)
        {
            string name = this.HttpContext.User.Identity.Name;
            User userIdByUsername = this._accountService.GetUserIdByUsername(name);
            List<PostViewModel> list = new List<PostViewModel>();
            IList<Picture> list2 = this._pictureService.GetFollowingPostsByUserId(userIdByUsername.Id, page, PaginationConsts.PageSize);
            foreach (Picture picture in list2)
            {
                PostViewModel model1 = new PostViewModel
                {
                    Picture = picture,
                    User = this._accountService.GetUserIdByUsername(picture.UserId)
                };
                list.Add(model1);
            }
            return list;
        }

        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel
            {
                Posts = this.GetRecentFollowingPosts(1),
                FollowingUsers = this._accountService.GetAllFollowingUsersByUserName(base.HttpContext.User.Identity.Name)
            };
            return this.View(model);
        }
    }
}
