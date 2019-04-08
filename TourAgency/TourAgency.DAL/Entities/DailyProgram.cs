using System;
using System.Collections.Generic;
using System.Text;

namespace TourAgency.DAL.Entities
{
    public class DailyProgram
    {
        public int? DailyProgramId { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }
         
        public int? TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
