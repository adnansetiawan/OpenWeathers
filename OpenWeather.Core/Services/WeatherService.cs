using OpenWeather.Core.Dto;
using OpenWeather.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Core.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppConfig _appConfig;
        private const string BaseUrl = "http://api.openweathermap.org/data/2.5/weather";
        public WeatherService(IHttpClientFactory httpClientFactory, IAppConfig appConfig)
        { 
            _httpClientFactory = httpClientFactory;
            _appConfig = appConfig;
        }
        /// <summary>
        /// get weather data from open api
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<WeatherApiResponse> GetWeatherAsync(WeatherApiRequest request)
        {
            var appId = _appConfig.AppId;
            if (string.IsNullOrEmpty(appId))
            {
                throw new Core.Exceptions.InternalServerErrorRequestException("AppId is not found");
            }
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}?q={request.CityName}&APPID={_appConfig.AppId}");
            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherApiResponse>(stringContent);
                return result;
            }
            else
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherApiErrorResponse>(stringContent);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Core.Exceptions.DataNotFoundException(result.Code, result.Message);
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Core.Exceptions.UnauthorizedException(result.Code, result.Message);
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new Core.Exceptions.BadRequestException(result.Code, result.Message);
                }
                throw new Core.Exceptions.InternalServerErrorRequestException(result.Message);
            }
           
        }
           
            
           
             
    }
}
