using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GrapePhoto.Extensions;
using GrapePhoto.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Amazon.S3;
using GrapePhoto.Web.Models;
using GrapePhoto.Models;
using GrapePhoto.Helper;
using GrapePhoto.Web.Models.Posts;
using Newtonsoft.Json;

namespace GrapePhoto.Controllers
{
    public class CommonController : Controller
    {
        private IPictureService _pictureService;
        private IPostService _postService;
        private IAccountClient _accountClient;

        public CommonController(IPictureService pictureService, IPostService postService, IAccountClient accountClient)
        {
            _pictureService = pictureService;
            _postService = postService;
            _accountClient = accountClient;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<string> AsyncUpload(string comments)
        {
            var pictureComments = comments != null ? comments.Trim() : "";
            if (string.IsNullOrEmpty(pictureComments))
            {
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "please enter your comments",
                });
            }
            var httpPostedFile = Request.Form.Files.FirstOrDefault();
                if (httpPostedFile == null)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        success = false,
                        message = "No file uploaded",
                    });
                }

                var qqFileNameParameter = "qqfile";
                var fileName = httpPostedFile.FileName;
                if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                    fileName = Request.Form[qqFileNameParameter].ToString();
                //remove path (passed in IE)
                fileName = Path.GetFileName(fileName);
                var contentType = httpPostedFile.ContentType;
                var fileExtension = Path.GetExtension(fileName);
                if (!string.IsNullOrEmpty(fileExtension))
                    fileExtension = fileExtension.ToLowerInvariant();

                var provider = new FileExtensionContentTypeProvider();
                if (string.IsNullOrEmpty(contentType))
                {
                    contentType = provider.Mappings[fileExtension];
                }


                var id = Guid.NewGuid().ToString();
                string s3FileName = $"{id}{fileExtension}";

                var bytes = httpPostedFile.GetDownloadBits();

                var picture = new Picture()
                {
                    Id = id,
                    S3FileName = s3FileName,
                    Bytes = bytes,
                    MimeType = contentType
                };

                _pictureService.InsertPicture(picture);
                if (string.IsNullOrEmpty(comments))
                {
                    comments = " ";
                }
                var post = new PostDto()
                {
                    Title = comments,
                    ImgUrl = picture.Src,
                    ThumbUrl = picture.ThumbnailSrc,
                    UserId = HttpContext.User.Identity.Name,
                    Width = picture.Width,
                    Height = picture.Height
                };

                _postService.AddPost(post);


            var result = _accountClient.GetAllFollowersUsersByUserId(HttpContext.User.Identity.Name.ToString());
            var followerList = result.Select(o => o.UserId).ToList();


            var m = new Message()
            {
                UserName = HttpContext.User.Identity.Name.ToString(),
                Content = comments,
                ImageUrl = picture.Src,
                Followers = followerList
            };

            if(followerList.Count>=1)
            await Channel.Trigger(m, "feed", "new_feed");



            return JsonConvert.SerializeObject(new
            {
                success = true,
                message = "uploaded",
            });
        }

        



        [HttpPost]
        public JsonResult Follow(string id)
        {

            _accountClient.FollowOtherUser(HttpContext.User.Identity.Name, id);
            return Json(new {
                success = true
            });
        }

        [HttpPost]
        public JsonResult Unfollow(string id)
        {
            _accountClient.UnfollowOtherUser(HttpContext.User.Identity.Name, id);
            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public JsonResult LikePost(string postId)
        {
            var post = new LikePostDto() { PostId = postId, UserId = HttpContext.User.Identity.Name, IsLike = true };
            var result = _postService.LikePost(post);
            return Json(new
            {
                success = true,
                count = result.LikeCount
            });
        }
        [HttpPost]
        public JsonResult DislikePost(string postId)
        {
            var post = new LikePostDto() { PostId = postId, UserId = HttpContext.User.Identity.Name, IsLike = false };
            var result = _postService.LikePost(post);
            return Json(new
            {
                success = true,
                count = result.LikeCount
            });
        }
    }
}