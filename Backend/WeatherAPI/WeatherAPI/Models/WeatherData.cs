
using System;
using System.Collections.Generic;

namespace WeatherAPI.Models
{
    public class WeatherData
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string Condition { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public List<DailyForecast> DailyForecasts { get; set; }
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public string Condition { get; set; }
    }
}
