using System;
using System.Collections.Generic;
using System.Text;

namespace TourAgency.DAL.Entities
{
    public class Picture
    {
        public int? PictureId { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        public int? TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
