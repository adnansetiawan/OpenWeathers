using Newtonsoft.Json;
using OpenWeather.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenWeather.Core.Dto
{
    public class WeatherApiResponse
    {
      
        public string Name { get; set; }

        public int Timezone { get; set; }


        [JsonProperty("coord")]
        public Coordinate Coordinate{ get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        public MainData Main { get; set; }

        public int Visibility { get; set; }

        public Wind Wind { get; set; }  
        public Cloud Clouds { get; set; }    
      
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
    public class MainData
    {
        public double Temp
        {
            get;set;
        }
        public double TempInCelcius
        {
            get
            {
                return TemperaturConverter.Convert(Constans.TemperatureType.Fahrenheit, Constans.TemperatureType.Celcius, Temp);
            }
         
        }
        public double Pressure { get; set; }

        public double Humidity { get; set; }
    }

    public class Wind
    { 
       public double Speed { get; set; }
       public double Deg { get; set; }
    }

    public class Cloud
    {
        public double All { get; set; }
    }
}
