using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenWeather.Core.Dto
{
    public class WeatherApiRequest
    {
        public string CityName { get; set; }

        public string AppId { get; set; }
    }
}
