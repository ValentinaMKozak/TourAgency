using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAgency.WebApi.ViewModel
{
    public class DailyProgramViewModel
    {
        public int? DailyProgramId { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }

        public int? TourId { get; set; }
    }
}
