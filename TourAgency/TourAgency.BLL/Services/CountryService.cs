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

namespace TourAgency.BLL.Services
{
    public class CountryService : ICountryService
    {
        IUnitOfWork Database { get; set; }

        public CountryService()
        {
            Database = new EFUnitOfWork();
        }

        public IEnumerable<CountryDTO> GetAllCountries()
        {
            var countries = Database.Countries.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Country>, List<CountryDTO>>(countries);
        }

        public CountryDTO GetCountry(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var countryDb = Database.Countries.Get(id.Value);
            if (countryDb == null)
                throw new ValidationException("Country wasn't found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>()).CreateMapper();
            var countryDTO = mapper.Map<Country, CountryDTO>(countryDb);  
            return countryDTO;
        }
     
        public bool CreateCountry(CountryDTO countryDTO)
        {
            var countryDb = new Country
            {
                CountryName = countryDTO.CountryName
            };
            Database.Countries.Create(countryDb);
            return Database.Save();
        }

        public bool UpdateCountry(int? id, CountryDTO countryDTO)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var countryDb = Database.Countries.Get(id.Value);
            if (countryDb == null)
                throw new ValidationException("Country wasn't found", "");
            countryDb.CountryName = countryDTO.CountryName;
            Database.Countries.Update(countryDb);
            return Database.Save();
        }

        public bool DeleteCountry(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var countryDb = Database.Countries.Get(id.Value);
            if (countryDb == null)
                throw new ValidationException("Country wasn't found", "");
            Database.Countries.Delete(countryDb.CountryId);
            return Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
