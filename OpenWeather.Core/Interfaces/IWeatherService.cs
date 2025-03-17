using OpenWeather.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Core.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherApiResponse> GetWeatherAsync(WeatherApiRequest request);
    }
}
