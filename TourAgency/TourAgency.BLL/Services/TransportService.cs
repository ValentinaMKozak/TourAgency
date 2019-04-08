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
    public class TransportService : ITransportService
    {
        IUnitOfWork Database { get; set; }

        public TransportService()
        {
            Database = new EFUnitOfWork();
        }

        public IEnumerable<TransportDTO> GetAllTransports()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Transport, TransportDTO>()).CreateMapper();
            var transports = Database.Transports.GetAll();

            return mapper.Map<IEnumerable<Transport>, List<TransportDTO>>(transports);
        }

        public TransportDTO GetTransport(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var transportDb = Database.Transports.Get(id.Value);
            if (transportDb == null)
                throw new ValidationException("Transport wasn't found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Transport, TransportDTO>()).CreateMapper();

            return mapper.Map<Transport, TransportDTO>(transportDb);
        }

        public bool CreateTransport(TransportDTO transportDTO)
        {
            var transportDb = new Transport
            {
                TransportName = transportDTO.TransportName,
            };
            Database.Transports.Create(transportDb);

            return Database.Save();
        }

        public bool UpdateTransport(int? id, TransportDTO transportDTO)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var transportDb = Database.Transports.Get(id.Value);
            if (transportDb == null)
                throw new ValidationException("Transport wasn't found", "");
            transportDb.TransportName = transportDTO.TransportName;
            Database.Transports.Update(transportDb);

            return Database.Save();
        }

        public bool DeleteTransport(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var transportDb = Database.Transports.Get(id.Value);
            if (transportDb == null)
                throw new ValidationException("Transport wasn't found", "");
            Database.Transports.Delete(transportDb.TransportId);

            return Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
