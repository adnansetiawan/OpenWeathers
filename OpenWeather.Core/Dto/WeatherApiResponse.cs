using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenWeather.Core.Dto
{
    public class WeatherApiResponse
    {
        [JsonProperty("coord")]
        public Coordinate Coordinate{ get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }
      
    }
    public class Coordinate
    {
       
        public double Lat { get; set; }

        public double Lon { get; set; }

    }
    public class Weather
    {
        public string Main { get; set; }

        public string Description { get; set; }

        
    }
}
