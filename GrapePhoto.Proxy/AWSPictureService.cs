using Amazon.S3;
using Amazon.S3.Model;
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
        private static readonly string bucketName = "image01-824831449792";
        #endregion



        #region Ctor

        public AWSPictureService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
           // this.S3Client = amazonS3;
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
            try
            {
                Stream stream = new MemoryStream(pictureBinary);
                var pictureId = Guid.NewGuid();
                // simple object put
                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = stream,
                    BucketName = bucketName,
                    ContentType = mimeType,
                    Key = pictureId.ToString()
                };

                var response = S3Client.PutObjectAsync(request);

                // put a more complex object with some metadata and http headers.
                PutObjectRequest titledRequest = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = pictureId.ToString()
                };
                titledRequest.Metadata.Add("title", titleAttribute);

                S3Client.PutObjectAsync(titledRequest);

              
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                }
            }
            return new Picture();
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
