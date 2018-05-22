using GrapePhoto.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Proxy
{
    public interface IPictureService
    {

        byte[] LoadPictureBinary(Picture picture);
  
        string GetPictureUrl(int pictureId,
            int targetSize = 0,
            PictureType defaultPictureType = PictureType.Post);

        string GetThumbLocalPath(Picture picture, int targetSize = 0);

        Picture GetPictureById(int pictureId);

        IPagedList<Picture> GetPictures(int pageIndex = 0, int pageSize = int.MaxValue);

        IList<Picture> GetPicturesByUserId(int userId, int recordsToReturn = 0);

        Picture InsertPicture(byte[] pictureBinary, string mimeType, string titleAttribute = null);

        void DeletePicture(Picture picture);

    }
}
