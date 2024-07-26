using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaApodIntegration.Models
{
    public class ApodResponse
    {
        public required string Date { get; set; }
        public required string Explanation { get; set; }
        public required string Url { get; set; }
        public required string Title { get; set; }
    }
}
