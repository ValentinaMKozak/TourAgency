using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;

namespace TourAgency.BLL.Interfaces
{
    public interface IPictureService
    {
        IEnumerable<PictureDTO> GetAllPictures(int? tourId);
        PictureDTO GetPicture(int? id);
        PictureDTO GetPictureByCloudId(string id);
        PictureDTO GetMainPicture(int? tourId);
        bool CreatePicture(PictureForCreationDTO pictureDTO);
        bool UpdatePicture(int? id, PictureDTO pictureDTO);
        bool DeletePicture(int? id);
        void Dispose();
    }
}

