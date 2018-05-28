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

namespace GrapePhoto.Controllers
{
    public class CommonController : Controller
    {
        private IPictureService _pictureService;
        public CommonController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        //do not validate request token (XSRF)
        public JsonResult AsyncUpload(string comments)
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

            return Json(new
            {
                success = true,
                result = picture
            });
        }
    }
}