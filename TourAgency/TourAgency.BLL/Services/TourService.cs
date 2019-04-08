using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;
using TourAgency.BLL.Infrastructure;
using TourAgency.BLL.Interfaces;
using TourAgency.DAL.Entities;
using TourAgency.DAL.Interfaces;
using TourAgency.DAL.Repositories;
using System.Linq;

namespace TourAgency.BLL.Services
{
    public class TourService : ITourService
    {
        IUnitOfWork Database { get; set; }

        public TourService()
        {
            Database = new EFUnitOfWork();
        }

        public IEnumerable<TourForListDTO> GetAllTours()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tour, TourForListDTO>()
                    .ForMember(dest => dest.PictureUrl, opt =>
                    {
                        opt.MapFrom(src => src.Pictures.FirstOrDefault(p => p.IsMain).URL);
                    })
                    .ForMember(dto => dto.Countries, opt => opt.MapFrom(x => x.TourCountries.Select(y => y.Country).ToList())))
                    .CreateMapper();
            var toursDb = Database.Tours.GetAll();
            var toursDTO = mapper.Map<IEnumerable<Tour>, List<TourForListDTO>>(toursDb);

            return toursDTO;
        }

        public TourForDetailedDTO GetTour(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var tourDb = Database.Tours.Get(id.Value);
            if (tourDb == null)
                throw new ValidationException("Tour wasn't found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tour, TourForDetailedDTO>()
                    .ForMember(dest => dest.PictureUrl, opt =>
                    {
                        opt.MapFrom(src => src.Pictures.FirstOrDefault(p => p.IsMain).URL);
                    })
                    .ForMember(dto => dto.Countries, opt => opt.MapFrom(x => x.TourCountries.Select(y => y.Country).ToList())))
                    .CreateMapper();
            var tourDTO = mapper.Map<Tour, TourForDetailedDTO>(tourDb);
           
            return tourDTO;
        }

        public bool CreateTour(TourDTO tourDTO)
        {
            var tourDb = new Tour
            {
                TourName = tourDTO.TourName,
                DepartureDate = tourDTO.DepartureDate,
                NumberOfDays = tourDTO.NumberOfDays,
                Price = tourDTO.Price,
                Сurrency = tourDTO.Сurrency,
                Created = tourDTO.Created
            };
            Database.Tours.Create(tourDb);

            return Database.Save();
        }

        public bool UpdateTour(int? id, TourDTO tourDTO)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var tourDb = Database.Tours.Get(id.Value);
            if (tourDb == null)
                throw new ValidationException("Tour wasn't found", "");
            tourDb.TourName = tourDTO.TourName;
            tourDb.DepartureDate = tourDTO.DepartureDate;
            tourDb.NumberOfDays = tourDTO.NumberOfDays;
            tourDb.Price = tourDTO.Price;
            tourDb.Сurrency = tourDTO.Сurrency;
            Database.Tours.Update(tourDb);

            return Database.Save();
        }

        public bool DeleteTour(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var tourDb = Database.Tours.Get(id.Value);
            if (tourDb == null)
                throw new ValidationException("Tour wasn't found", "");
            Database.Tours.Delete(tourDb.TourId);

            return Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
