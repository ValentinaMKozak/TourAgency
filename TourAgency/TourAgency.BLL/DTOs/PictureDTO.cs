using System;
using System.Collections.Generic;
using System.Text;

namespace TourAgency.BLL.DTOs
{
    public class PictureDTO
    {
        public int? PictureId { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}
