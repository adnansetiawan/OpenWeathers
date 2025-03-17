using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeather.Core.Tests
{
    public static class WeatherApiMockData
    {
        public static string GetOkData()
        {
            return "{\"coord\":{\"lon\":-0.1257,\"lat\":51.5085},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcastclouds\",\"icon\":\"04n\"}],\"base\":\"stations\",\"main\":{\"temp\":278.48,\"feels_like\":275.99,\"temp_min\":277.62,\"temp_max\":279.25,\"pressure\":1028,\"humidity\":80,\"sea_level\":1028,\"grnd_level\":1023},\"visibility\":10000,\"wind\":{\"speed\":3.09,\"deg\":30},\"clouds\":{\"all\":100},\"dt\":1742189133,\"sys\":{\"type\":2,\"id\":2091269,\"country\":\"GB\",\"sunrise\":1742191796,\"sunset\":1742234867},\"timezone\":0,\"id\":2643743,\"name\":\"London\",\"cod\":200}";
    

        }
        public static string GetNotFoundData()
        {
            return "{\"cod\":\"404\",\"message\":\"city not found\"}";
        }
    }
}
