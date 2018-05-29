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
        public async Task<JsonResult>  AsyncUpload(string comments)
        {

                var pictureComments = comments != null ? comments.Trim() : "";
                var httpPostedFile = Request.Form.Files.FirstOrDefault();
                if (httpPostedFile == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "No file uploaded",
                        downloadGuid = Guid.Empty,
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

                var post = new PostDto()
                {
                    Title = comments,
                    ImgUrl = picture.Src,
                    UserId = HttpContext.User.Identity.Name
                };

                _postService.AddPost(post);


            var result = _accountClient.GetAllFollowersUsersByUserId(HttpContext.User.Identity.Name.ToString());
            var followerList = result.Select(o => o.UserId).ToList();

            followerList.Add("liucao123");
            followerList.Add("allen");

            var m = new Message()
            {
                UserName = HttpContext.User.Identity.Name.ToString(),
                Content = comments,
                ImageUrl = picture.Src,
                Followers = followerList//new string[] { "liucao", "allen" }
            };
            await Channel.Trigger(m, "feed", "new_feed");


            return Json(new
                {
                    success = true,
                    result = picture,

                });
        }
    }
}