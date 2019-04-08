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
     public class PictureRepository : IRepository<Picture>
    {
        private readonly TourAgencyContext db;

        public PictureRepository(TourAgencyContext context)
        {
            db = context;
        }

        public IEnumerable<Picture> GetAll()
        {
            return db.Pictures;
        }

        public Picture Get(int? id)
        {
            return db.Pictures.FirstOrDefault(p => p.PictureId == id);
        }

        public IEnumerable<Picture> Find(Func<Picture, bool> predicate)
        {
            return db.Pictures.Where(predicate).ToList();
        }

        public void Create(Picture picture)
        {
            db.Pictures.Add(picture);
        }

        public void Update(Picture picture)
        {
            db.Entry(picture).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture != null)
                db.Pictures.Remove(picture);
        }
    }
}
