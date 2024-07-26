using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaApodIntegration.Models
{
    public class AppSettings
    {
        public required string BaseUrl { get; set; }
        public required string ApiKey { get; set; }
        public required string Date { get; set; }
    }
}
