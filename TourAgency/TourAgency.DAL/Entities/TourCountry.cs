using System;
using System.Collections.Generic;
using System.Text;

namespace TourAgency.DAL.Entities
{
    public class TourCountry
    {
        public int? TourId { get; set; }
        public Tour Tour { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
