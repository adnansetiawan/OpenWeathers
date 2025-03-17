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
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
           _locationService = locationService;
        }

        /// <summary>
        /// get countries
        /// </summary>
        /// <returns></returns>
        [HttpGet("countries")]
        [ProducesResponseType(statusCode:200, Type = typeof(Dto.ResponseApiDto<List<CountryApiResponse>>))]
        public IActionResult GetCountries()
        {
            var response = _locationService.GetCountries();
            return Ok(new Dto.ResponseApiDto<List<CountryApiResponse>>(response));
        }
        /// <summary>
        /// get cities by country id
        /// </summary>
        /// <returns></returns>
        [HttpGet("cities")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Dto.ResponseApiDto<List<CityApiResponse>>))]
        public IActionResult GetCities(string countryID)
        {
            var response = _locationService.GetCities(countryID);
            return Ok(new Dto.ResponseApiDto<List<CityApiResponse>>(response));
        }

    }
}
