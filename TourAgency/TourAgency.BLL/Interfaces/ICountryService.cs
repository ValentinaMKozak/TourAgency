using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;

namespace TourAgency.BLL.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<CountryDTO> GetAllCountries();
        CountryDTO GetCountry(int? id);
        bool CreateCountry(CountryDTO countryDTO);
        bool UpdateCountry(int? id, CountryDTO countryDTO);
        bool DeleteCountry(int? id);
        void Dispose();
    }
}
