using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catch_the_trash.Models
{
    public class ReportModel
    {
        public int Id { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string Comment { get; set; }

        public ICollection<ImageModel> Images { get; set; }

    }
}
