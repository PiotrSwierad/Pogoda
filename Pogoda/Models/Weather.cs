using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pogoda.Models
{
    public class Weather
    {

        public int ID { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityCountry { get; set; }
        public string UserId { get; set; }

        public double CurrentTemperature { get; set; }
        public int CurrentHumidity { get; set; }
        public DateTime DateEntrySet { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}