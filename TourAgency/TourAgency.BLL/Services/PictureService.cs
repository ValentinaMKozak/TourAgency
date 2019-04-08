using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;
using TourAgency.BLL.Infrastructure;
using TourAgency.BLL.Interfaces;
using TourAgency.DAL.Entities;
using TourAgency.DAL.Interfaces;
using TourAgency.DAL.Repositories;

namespace TourAgency.BLL.Services
{
    public class PictureService : IPictureService
    {
        IUnitOfWork Database { get; set; }

        public PictureService()
        {
            Database = new EFUnitOfWork();
        }

        public IEnumerable<PictureDTO> GetAllPictures(int? tourId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDTO>()).CreateMapper();
            var picturesDb = Database.Pictures.Find(i => i.TourId == tourId);
            var picturesDTO = mapper.Map<IEnumerable<Picture>, List<PictureDTO>>(picturesDb);

            return picturesDTO;
        }

        public PictureDTO GetPicture(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var pictureDb = Database.Pictures.Get(id.Value);
            if (pictureDb == null)
                throw new ValidationException("Picture wasn't found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDTO>()).CreateMapper();
            var pictureDTO = mapper.Map<Picture, PictureDTO>(pictureDb);

            return pictureDTO;
        }

        public PictureDTO GetMainPicture(int? tourId)
        {
            if (tourId == null)
                throw new ValidationException("id was not passed", "");
            var pictureDb = Database.Pictures.Find(t => t.TourId == tourId).FirstOrDefault(p => p.IsMain);
            if (pictureDb == null)
                throw new ValidationException("Picture wasn't found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDTO>()).CreateMapper();
            var pictureDTO = mapper.Map<Picture, PictureDTO>(pictureDb);

            return pictureDTO;
        }

        public PictureDTO GetPictureByCloudId(string id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var pictureDb = Database.Pictures.Find(i => i.PublicId == id).FirstOrDefault();
            if (pictureDb == null)
                throw new ValidationException("Picture wasn't found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDTO>()).CreateMapper();
            var pictureDTO = mapper.Map<Picture, PictureDTO>(pictureDb);
          
            return pictureDTO;
        }
       
        public bool CreatePicture(PictureForCreationDTO pictureDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PictureForCreationDTO, Picture>()).CreateMapper();
            var pictureDb = mapper.Map<PictureForCreationDTO, Picture>(pictureDTO);
            //var pictureDb = new Picture
            //{
            //    URL = pictureDTO.URL,
            //    Description = pictureDTO.Description,
            //    PublicId = pictureDTO.PublicId,
            //    IsMain = pictureDTO.IsMain,
            //    DateAdded = pictureDTO.DateAdded,
            //    TourId = pictureDTO.TourId
            //};
            Database.Pictures.Create(pictureDb);

            return Database.Save();
        }

        public bool UpdatePicture(int? id, PictureDTO pictureDTO)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var pictureDb = Database.Pictures.Get(id.Value);
            if (pictureDb == null)
                throw new ValidationException("Picture wasn't found", "");
            pictureDb.URL = pictureDTO.URL;
            pictureDb.IsMain = pictureDTO.IsMain;
            pictureDb.Description = pictureDTO.Description;
            Database.Pictures.Update(pictureDb);

            return Database.Save();
        }

        public bool DeletePicture(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var pictureDb = Database.Pictures.Get(id.Value);
            if (pictureDb == null)
                throw new ValidationException("Picture wasn't found", "");
            Database.Pictures.Delete(pictureDb.PictureId);

            return Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
