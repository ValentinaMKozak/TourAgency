using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace TourAgency.DAL.Entities
{
    public class Country
    {
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public ICollection<TourCountry> TourCountries { get; set; }

        public Country()
        {
            TourCountries = new Collection<TourCountry>();
        }
    }
}
