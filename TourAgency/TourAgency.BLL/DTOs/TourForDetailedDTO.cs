﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TourAgency.BLL.DTOs
{
    public class TourForDetailedDTO
    {
        public int? TourId { get; set; }
        public string TourName { get; set; }
        public DateTime DepartureDate { get; set; }
        public int NumberOfDays { get; set; }
        public decimal Price { get; set; }
        public string Сurrency { get; set; }
        public DateTime Created { get; set; }
        public string PictureUrl { get; set; }
        public TransportDTO TypeTransport { get; set; }

        public ICollection<PictureDTO> Pictures { get; set; }
        public ICollection<DailyProgramDTO> DailyPrograms { get; set; }
        public ICollection<CountryDTO> Countries { get; set; }
    }
}