using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.DAL.EF;
using TourAgency.DAL.Entities;
using TourAgency.DAL.Interfaces;

namespace TourAgency.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly TourAgencyContext db;

        private TourRepository tourRepository;
        private CountryRepository countryRepository;
        private PictureRepository pictureRepository;
        private DailyProgramRepository dailyProgramRepository;
        private TransportRepository transportRepository;
        private OrderRepository orderRepository;

        private bool disposed = false;

        public EFUnitOfWork()
        {
            db = new TourAgencyContext();
        }

        public IRepository<Tour> Tours
        {
            get
            {
                if (tourRepository == null)
                    tourRepository = new TourRepository(db);
                return tourRepository;
            }
        }

        public IRepository<Country> Countries
        {
            get
            {
                if (countryRepository == null)
                    countryRepository = new CountryRepository(db);
                return countryRepository;
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                if (pictureRepository == null)
                    pictureRepository = new PictureRepository(db);
                return pictureRepository;
            }
        }

        public IRepository<DailyProgram> DailyPrograms
        {
            get
            {
                if (dailyProgramRepository == null)
                    dailyProgramRepository = new DailyProgramRepository(db);
                return dailyProgramRepository;
            }
        }

        public IRepository<Transport> Transports
        {
            get
            {
                if (transportRepository == null)
                    transportRepository = new TransportRepository(db);
                return transportRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
