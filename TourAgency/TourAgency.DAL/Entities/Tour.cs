using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TourAgency.DAL.Entities
{
    public class Tour
    {
        public int? TourId { get; set; }
        public string TourName { get; set; }
        public DateTime DepartureDate { get; set; }
        public int NumberOfDays { get; set; }
        public decimal Price { get; set; }
        public string Сurrency { get; set; }
        public DateTime Created { get; set; }
        public Transport TypeTransport { get; set; }
        public ICollection<DailyProgram> DailyPrograms { get; set; }
        public ICollection<TourCountry> TourCountries { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Tour()
        {
            DailyPrograms = new Collection<DailyProgram>();
            TourCountries = new Collection<TourCountry>();
            Pictures = new Collection<Picture>();
            Orders = new Collection<Order>();
        }
    }
}
