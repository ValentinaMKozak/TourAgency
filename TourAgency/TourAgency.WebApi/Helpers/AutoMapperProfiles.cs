using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAgency.BLL.DTOs;
using TourAgency.WebApi.Data;
using TourAgency.WebApi.ViewModel;

namespace TourAgency.WebApi.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserViewModel, ApplicationUser>();
            
            CreateMap<TourDTO, TourViewModel>();
            CreateMap<TourViewModel, TourDTO>();
            CreateMap<TourForListDTO, TourForListViewModel>();
            CreateMap<TourForDetailedDTO, TourForDetailedViewModel>();

            CreateMap<PictureForCreationViewModel, PictureForCreationDTO>();
            CreateMap<PictureDTO, PictureViewModel>();

            CreateMap<CountryDTO, CountryViewModel>();
            CreateMap<CountryViewModel, CountryDTO>();
     
            CreateMap<TransportViewModel, TransportDTO>();
            CreateMap<TransportDTO, TransportViewModel>();

            CreateMap<DailyProgramDTO, DailyProgramViewModel>();
            CreateMap<DailyProgramViewModel, DailyProgramDTO > ();

            CreateMap<OrderDTO, OrderViewModel>();
            CreateMap<OrderViewModel, OrderDTO>();
        }
    }
}
