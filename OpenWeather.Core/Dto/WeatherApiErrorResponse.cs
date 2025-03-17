using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeather.Core.Dto
{
    public class WeatherApiErrorResponse
    {
        [JsonProperty("cod")]
        public string Code { get; set; }

        public string Message { get; set; }
    }
}
