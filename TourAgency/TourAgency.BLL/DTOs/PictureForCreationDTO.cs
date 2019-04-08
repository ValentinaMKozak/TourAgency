using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.DAL.Entities;

namespace TourAgency.BLL.DTOs
{
    public class PictureForCreationDTO
    {
        public string URL { get; set; }
        public string Description { get; set; }
        public string PublicId { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }

        public int? TourId { get; set; }
        public TourForDetailedDTO TourDTO { get; set; }
    }
}
