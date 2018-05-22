using Amazon.S3;
using GrapePhoto.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GrapePhoto.Proxy
{
    public class AWSPictureService : IPictureService
    {
        #region Const

        private const int MULTIPLE_THUMB_DIRECTORIES_LENGTH = 3;

        #endregion

        #region Fields

        IAmazonS3 S3Client { get; set; }
        IHostingEnvironment _hostingEnvironment;
        private static readonly string bucketName = "";
        #endregion



        #region Ctor

        public AWSPictureService(IHostingEnvironment hostingEnvironment, IAmazonS3 amazonS3)
        {
            _hostingEnvironment = hostingEnvironment;
            this.S3Client = amazonS3;
        }

        public void DeletePicture(Picture picture)
        {
            throw new NotImplementedException();
        }

        public Picture GetPictureById(int pictureId)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Picture> GetPictures(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public IList<Picture> GetPicturesByUserId(int userId, int recordsToReturn = 0)
        {
            throw new NotImplementedException();
        }

        public string GetPictureUrl(int pictureId, int targetSize = 0, PictureType defaultPictureType = PictureType.Post)
        {
            throw new NotImplementedException();
        }

        public string GetThumbLocalPath(Picture picture, int targetSize = 0)
        {
            throw new NotImplementedException();
        }

        public Picture InsertPicture(byte[] pictureBinary, string mimeType, string titleAttribute = null)
        {
            throw new NotImplementedException();
        }

        public byte[] LoadPictureBinary(Picture picture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Returns the file extension from mime type.
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns>File extension</returns>
        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //TODO use FileExtensionContentTypeProvider to get file extension

            var parts = mimeType.Split('/');
            var lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }
            return lastPart;
        }

        #endregion
 

         
    }
}
