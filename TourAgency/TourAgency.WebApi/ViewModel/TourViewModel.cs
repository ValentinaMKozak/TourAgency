using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAgency.WebApi.ViewModel
{
    public class TourViewModel
    {
        public int? TourId { get; set; }
        public string TourName { get; set; }
        public DateTime DepartureDate { get; set; }
        public int NumberOfDays { get; set; }
        public decimal Price { get; set; }
        public string Сurrency { get; set; }
        public DateTime Created { get; set; }
        public string PictureUrl { get; set; }
        public TransportViewModel TypeTransport { get; set; }
    }
}