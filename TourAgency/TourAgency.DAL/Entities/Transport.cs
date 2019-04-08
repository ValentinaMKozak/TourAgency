using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TourAgency.DAL.Entities
{
    public class Transport
    {
        public int? TransportId { get; set; }
        public string TransportName { get; set; }
        public ICollection<Tour> Tours { get; set; }

        public Transport()
        {
            Tours = new Collection<Tour>();
        }
    }
}
