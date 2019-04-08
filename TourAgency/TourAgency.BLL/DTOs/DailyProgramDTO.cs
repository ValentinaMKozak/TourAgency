using System;
using System.Collections.Generic;
using System.Text;

namespace TourAgency.BLL.DTOs
{
    public class DailyProgramDTO
    { 
        public int? DailyProgramId { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }

        public int? TourId { get; set; }
    }
}
