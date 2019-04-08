using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;

namespace TourAgency.BLL.Interfaces
{
    public interface ITransportService
    {
        IEnumerable<TransportDTO> GetAllTransports();
        TransportDTO GetTransport(int? id);
        bool CreateTransport(TransportDTO transportDTO);
        bool UpdateTransport(int? id, TransportDTO transportDTO);
        bool DeleteTransport(int? id);
        void Dispose();
    }
}
