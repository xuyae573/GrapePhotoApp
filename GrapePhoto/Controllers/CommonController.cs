using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GrapePhoto.Extensions;
using GrapePhoto.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

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
        public IActionResult AsyncUpload(string comments)
        {
            var pictureComments = comments != null ? comments.Trim(): "";
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

            var fileBinary = httpPostedFile.GetDownloadBits();

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

           // var picture = _pictureService.InsertPicture(fileBinary, contentType, null);
            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return Json(new
            {
                success = true,
                //pictureId = picture.Id,
                //imageUrl =  picture.Src
            });
        }

    }
}