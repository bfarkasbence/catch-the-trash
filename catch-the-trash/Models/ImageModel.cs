using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace catch_the_trash.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; }

        //public int ReportId { get; set; }

        //[ForeignKey("ReportId")]
        public ReportModel Report { get; set; }
    }
}
