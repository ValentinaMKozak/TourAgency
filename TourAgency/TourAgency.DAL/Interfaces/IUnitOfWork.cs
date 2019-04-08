using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.DAL.Entities;

namespace TourAgency.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Tour> Tours { get; }
        IRepository<Country> Countries { get; }
        IRepository<Picture> Pictures { get; }
        IRepository<DailyProgram> DailyPrograms { get; }
        IRepository<Transport> Transports { get; }
        IRepository<Order> Orders { get; }

        bool Save();
    }
}
