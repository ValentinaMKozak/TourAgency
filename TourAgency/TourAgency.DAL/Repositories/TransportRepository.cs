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
    public class TransportRepository : IRepository<Transport>
    {
        private readonly TourAgencyContext db;

        public TransportRepository(TourAgencyContext context)
        {
            db = context;
        }
       
        public IEnumerable<Transport> GetAll()
        {
            var transports = db.Transports.ToList();
            return transports;
        }

        public Transport Get(int? id)
        {
            return db.Transports.FirstOrDefault(tr => tr.TransportId == id);
        }

        public IEnumerable<Transport> Find(Func<Transport, bool> predicate)
        {
            return db.Transports.Where(predicate).ToList();
        }

        public void Create(Transport transport)
        {
            db.Transports.Add(transport);
        }

        public void Update(Transport transport)
        {
            db.Entry(transport).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Transport transport = db.Transports.Find(id);
            if (transport != null)
                db.Transports.Remove(transport);
        }
    }
}
