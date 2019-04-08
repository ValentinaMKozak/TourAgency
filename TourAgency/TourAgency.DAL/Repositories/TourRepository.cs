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
    public class TourRepository : IRepository<Tour>
    {
        private readonly TourAgencyContext db;

        public TourRepository(TourAgencyContext context)
        {
            db = context;
        }

        public IEnumerable<Tour> GetAll()
        {
            var tours = db.Tours.Include(p => p.Pictures).Include(t => t.TypeTransport).Include(tc => tc.TourCountries).ThenInclude(c => c.Country).ToList();
            return tours; 
        }

        public Tour Get(int? id)
        {
            return db.Tours.Include(p => p.Pictures).Include(t => t.TypeTransport).Include(d=> d.DailyPrograms).Include(tc => tc.TourCountries).ThenInclude(c => c.Country).FirstOrDefault(t => t.TourId == id);
        }

        public IEnumerable<Tour> Find(Func<Tour, bool> predicate)
        {
          //  var tours = db.Tours.Include(p => p.Pictures).Include(t => t.TypeTransport).Include(tc => tc.TourCountries).ThenInclude(c => c.Country).ToList();
            return db.Tours.Include(p => p.Pictures).Include(t => t.TypeTransport).Include(tc => tc.TourCountries).ThenInclude(c => c.Country).Where(predicate).ToList();
        }

    
    public void Create(Tour tour)
        {
            db.Tours.Add(tour);
        }

        public void Update(Tour tour)
        {
            db.Entry(tour).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Tour tour = db.Tours.Find(id);
            if (tour != null)
                db.Tours.Remove(tour);
        }




//        // delete
//        public Tour FindItem(Func<Tour, bool> predicate)
//        {
//            return db.Tours.Where(predicate).FirstOrDefault();
//        }

//        // delete      
//        public IEnumerable<Tour> GetAllByItemId(int? id)
//        {
//            throw new NotImplementedException();
//        }
//// delete
//        public Tour GetByStringId(string id)
//        {
//            throw new NotImplementedException();
//        }
//// delete
//        public Tour GetMainItem(int? id)
//        {
//            throw new NotImplementedException();
//        }
//// delete
//        public IEnumerable<Tour> GetAllByItem(string item)
//        {
//            throw new NotImplementedException();
//        }
    }
}
