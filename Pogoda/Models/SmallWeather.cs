using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pogoda.Models
{
    public class SmallWeather
    {
        public int ID { get; set; }
        public int WeatherReportId { get; set; }

        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public DateTime DateOf { get; set; }

    }
}