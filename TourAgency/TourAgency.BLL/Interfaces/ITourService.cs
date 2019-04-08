using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;

namespace TourAgency.BLL.Interfaces
{
    public interface ITourService
    {
        IEnumerable<TourForListDTO> GetAllTours();
        TourForDetailedDTO GetTour(int? id);
        bool CreateTour(TourDTO tourDTO);
        bool UpdateTour(int? id, TourDTO tourDTO);
        bool DeleteTour(int? id);
        void Dispose();
    }
}
