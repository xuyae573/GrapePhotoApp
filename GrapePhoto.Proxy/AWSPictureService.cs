using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using GrapePhoto.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageMagick;

namespace GrapePhoto.Proxy
{
    public static class PictureAPI
    {
  
    }

    public class AWSPictureService : IPictureService
    {
        #region Const

        private const int MULTIPLE_THUMB_DIRECTORIES_LENGTH = 3;

        #endregion

        #region Fields

        IAmazonS3 S3Client { get; set; }
        IConfiguration Configuration { get; set; }

        private static readonly string bucketName = "image-us-east-1-824831449792";

        private static readonly string ThumbFolerPath = bucketName + @"/Thumbs";

        private static readonly string s3WebUrl = "https://s3.amazonaws.com/{0}/{1}";

        private static readonly int _size = 300; // thumb size
        #endregion



        #region Ctor

        public AWSPictureService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void DeletePicture(Picture picture)
        {
            throw new NotImplementedException();
        }

        public Picture GetPictureById(string pictureId)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Picture> GetPictures(string userid, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var results = new List<Picture>();
            //call aws api get pictures
            results.Add(new Picture()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "xuyae573",
                AltAttribute = "test",
                PostDate = DateTime.Now.AddDays(-10),
                Src = "https://scontent-sin6-2.cdninstagram.com/vp/0eb0099e34e5165c65b794f07e7f83d0/5BA2A7F3/t51.2885-15/e35/32070141_2020559538261493_1656320628367556608_n.jpg",
                ThumbnailSrc = "https://scontent-sin6-2.cdninstagram.com/vp/ec7397053ca611fcb4982101e86c7388/5B9107D9/t51.2885-15/s640x640/sh0.08/e35/32070141_2020559538261493_1656320628367556608_n.jpg",
                TitleAttribute = "this is the title",
                LikeCount = 100,
            });
            results.Add(new Picture()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "xuyae573",
                AltAttribute = "test 3",
                PostDate = DateTime.Now.AddDays(-10),
                Src = "https://scontent-sin6-2.cdninstagram.com/vp/e12b44e9b10392910fc55258b2c0cd64/5B833062/t51.2885-15/e35/32243789_205152516760720_3057022588736765952_n.jpg",
                ThumbnailSrc = "https://scontent-sin6-2.cdninstagram.com/vp/dee299f8649386dbd5192f8141cffda7/5B9FBB94/t51.2885-15/sh0.08/e35/p640x640/32243789_205152516760720_3057022588736765952_n.jpg",
                TitleAttribute = "this is the title",
                LikeCount = 100,
            });
            results.Add(new Picture()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "xuyae573",
                AltAttribute = "test 3",
                PostDate = DateTime.Now.AddDays(-10),
                Src = "https://scontent-sin6-2.cdninstagram.com/vp/e12b44e9b10392910fc55258b2c0cd64/5B833062/t51.2885-15/e35/32243789_205152516760720_3057022588736765952_n.jpg",
                ThumbnailSrc = "https://scontent-sin6-2.cdninstagram.com/vp/dee299f8649386dbd5192f8141cffda7/5B9FBB94/t51.2885-15/sh0.08/e35/p640x640/32243789_205152516760720_3057022588736765952_n.jpg",
                TitleAttribute = "this is the title",
                LikeCount = 100,
            });
            IPagedList<Picture> pictures = new PagedList<Picture>(results,pageIndex,pageSize);
            return pictures;
        }

        public IList<Picture> GetPicturesByUserId(string userId, int recordsToReturn = 0)
        {
            throw new NotImplementedException();
        }



        public string GetPictureUrl(string pictureId, int targetSize = 0, PictureType defaultPictureType = PictureType.Post)
        {
            throw new NotImplementedException();
        }

        public string GetThumbLocalPath(Picture picture, int targetSize = 0)
        {
            throw new NotImplementedException();
        }

 
        #endregion

        #region Utilities

        public IList<Picture> GetFollowingPostsByUserId(string userId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            List<Picture> pictures = new List<Picture>();

            //call aws api get pictures
            pictures.Add(new Picture()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "xuyae573",
                AltAttribute = "test",
                PostDate = DateTime.Now.AddDays(-10),
                Src = "https://scontent-sin6-2.cdninstagram.com/vp/0eb0099e34e5165c65b794f07e7f83d0/5BA2A7F3/t51.2885-15/e35/32070141_2020559538261493_1656320628367556608_n.jpg",
                ThumbnailSrc = "https://scontent-sin6-2.cdninstagram.com/vp/ec7397053ca611fcb4982101e86c7388/5B9107D9/t51.2885-15/s640x640/sh0.08/e35/32070141_2020559538261493_1656320628367556608_n.jpg",
                TitleAttribute= "this is the title",
                LikeCount = 100,
            });

            pictures.Add(new Picture()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "xuyae573",
                AltAttribute = "test 3",
                PostDate = DateTime.Now.AddDays(-10),
                Src = "https://scontent-sin6-2.cdninstagram.com/vp/e12b44e9b10392910fc55258b2c0cd64/5B833062/t51.2885-15/e35/32243789_205152516760720_3057022588736765952_n.jpg",
                ThumbnailSrc = "https://scontent-sin6-2.cdninstagram.com/vp/dee299f8649386dbd5192f8141cffda7/5B9FBB94/t51.2885-15/sh0.08/e35/p640x640/32243789_205152516760720_3057022588736765952_n.jpg",
                TitleAttribute = "this is the title",
                LikeCount = 100,
            });
            return pictures;

        }

        public IList<Picture> GetUserPostsByUserId(string userId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public bool LikePicture(Picture picture)
        {
            throw new NotImplementedException();
        }

        public Picture InsertPicture(Picture picture)
        {
            var awsConfig = Configuration.GetAWSOptions();
            S3Client = new AmazonS3Client(awsConfig.Credentials,RegionEndpoint.USEast1);
            try
            {
                var fileTransferUtility = new TransferUtility(S3Client);
          
                picture.PictureStream = new MemoryStream(picture.Bytes);
                var transferrequest = new TransferUtilityUploadRequest
                {
                    InputStream = picture.PictureStream,
                    BucketName = bucketName,
                    ContentType = picture.MimeType,
                    Key = picture.S3FileName,
                    CannedACL = S3CannedACL.PublicRead
                };
                fileTransferUtility.Upload(transferrequest);
                picture.Src = string.Format(s3WebUrl, transferrequest.BucketName, transferrequest.Key);


                var thumbStream = new MemoryStream();
               
                using (MagickImage image = new MagickImage(picture.Bytes))
                {
                    int width, height;
                    if (image.Width > image.Height)
                    {
                        width = _size;
                        height = image.Height * _size / image.Width;
                    }
                    else
                    {
                        width = image.Width * _size / image.Height;
                        height = _size;
                    }
                    MagickGeometry size = new MagickGeometry(width, height);
                    size.IgnoreAspectRatio = true;
                    image.Resize(size);
                    image.Write(thumbStream);
                }

                var transferThumbs = new TransferUtilityUploadRequest
                {
                    InputStream = thumbStream,
                    BucketName = ThumbFolerPath,
                    ContentType = picture.MimeType,
                    Key = picture.S3FileName,
                    CannedACL = S3CannedACL.PublicRead
                };
                fileTransferUtility.Upload(transferThumbs);

                picture.ThumbnailSrc = string.Format(s3WebUrl, transferThumbs.BucketName, transferrequest.Key);
                return picture;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Please check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception(string.Format("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message));
                }
            }
        }

        #endregion

    }
}
