using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourAgency.DAL.EF;
using TourAgency.DAL.Entities;
using TourAgency.DAL.Interfaces;

namespace TourAgency.DAL.Repositories
{
    public class CountryRepository : IRepository<Country>
    {
        private readonly TourAgencyContext db;

        public CountryRepository(TourAgencyContext context)
        {
            db = context;
        }
      
        public IEnumerable<Country> GetAll()
        {
            return db.Countries.ToList();
        }

        public Country Get(int? id)
        {
            return db.Countries.FirstOrDefault(c => c.CountryId == id);
        }

        public IEnumerable<Country> Find(Func<Country, bool> predicate)
        {
            return db.Countries.Where(predicate).ToList();
        }

        public void Create(Country country)
        {
            db.Countries.Add(country);
        }

        public void Update(Country country)
        {
            db.Entry(country).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Country country = db.Countries.Find(id);
            if (country != null)
                db.Countries.Remove(country);
        }
    }
}
