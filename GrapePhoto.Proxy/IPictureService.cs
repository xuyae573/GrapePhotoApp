using GrapePhoto.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Proxy
{
    public interface IPictureService
    {

        byte[] LoadPictureBinary(Picture picture);
  
        string GetPictureUrl(string pictureId,
            int targetSize = 0,
            PictureType defaultPictureType = PictureType.Post);

        string GetThumbLocalPath(Picture picture, int targetSize = 0);

        Picture GetPictureById(string pictureId);

        IPagedList<Picture> GetPictures(string userid, int pageIndex = 0, int pageSize = int.MaxValue);

        IList<Picture> GetPicturesByUserId(string userId, int recordsToReturn = 0);

        IList<Picture> GetFollowingPostsByUserId(string userId, int pageIndex = 0, int pageSize = int.MaxValue);

        IList<Picture> GetUserPostsByUserId(string userId, int pageIndex = 0, int pageSize = int.MaxValue);

        Picture InsertPicture(byte[] pictureBinary, string mimeType, string titleAttribute = null);

        void DeletePicture(Picture picture);


        bool LikePicture(Picture picture);
    }
}
