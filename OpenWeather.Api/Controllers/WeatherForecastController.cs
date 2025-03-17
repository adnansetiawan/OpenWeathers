using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenWeather.Core.Dto;
using OpenWeather.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeather.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public WeatherForecastController(IWeatherService weatherService)
        {
           _weatherService = weatherService;
        }

        /// <summary>
        /// get weather forecast by city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(statusCode:200, Type = typeof(Dto.ResponseApiDto<WeatherApiResponse>))]
        public async Task<IActionResult> GetByCity(string city)
        {
            var response = await _weatherService.GetWeatherAsync(new Core.Dto.WeatherApiRequest
            {
                  CityName = city
            });
            return Ok(new Dto.ResponseApiDto<WeatherApiResponse>(response));
        }
    }
}
