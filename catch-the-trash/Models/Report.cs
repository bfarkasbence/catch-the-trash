using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catch_the_trash.Models
{
    public class Report
    {
        public int Id { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string Comment { get; set; }

        public string Images { get; set; }
    }
}
