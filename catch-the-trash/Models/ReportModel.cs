using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catch_the_trash.Models
{
    public class ReportModel
    {
        public int Id { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Comment { get; set; }

        public ICollection<ImageModel> Images { get; set; }

    }
}
