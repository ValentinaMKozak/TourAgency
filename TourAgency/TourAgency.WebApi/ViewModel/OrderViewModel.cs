using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAgency.WebApi.ViewModel
{
    public class OrderViewModel
    {
        public int? OrderId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool IsBiometricPassport { get; set; }
        public string SerieAndNumberOfPassport { get; set; }

        public bool IsBookingRailwayTicket { get; set; }
        public bool IsBookingAviaTicket { get; set; }
        public bool IsVisaSupport { get; set; }
        public bool IsInsurance { get; set; }

        public string DesiredHotelAccom { get; set; }

        public int? TourId { get; set; }
        public string TourName { get; set; }

    }
}
