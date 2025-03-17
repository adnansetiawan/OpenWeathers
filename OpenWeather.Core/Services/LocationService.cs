using OpenWeather.Core.Dto;
using OpenWeather.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Core.Services
{
    public class LocationService : ILocationService
    {
      
        /// <summary>
        /// mock data for cities
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public List<CityApiResponse> GetCities(string CountryID)
        {
           var cities = new List<CityApiResponse>();
           cities.Add(new CityApiResponse("ID", "Jakarta"));
           cities.Add(new CityApiResponse("ID", "Bandung"));
           cities.Add(new CityApiResponse("ID", "Makassar"));
           cities.Add(new CityApiResponse("JP", "Tokyo"));
           cities.Add(new CityApiResponse("JP", "Osaka"));
           cities.Add(new CityApiResponse("UK", "London"));
           cities.Add(new CityApiResponse("UK", "Manchester"));
           return cities.Where(x=>x.CountryID == CountryID).ToList();

        }

        /// <summary>
        /// mock data for countries
        /// </summary>
        /// <returns></returns>
        public List<CountryApiResponse> GetCountries()
        {
            var countries = new List<CountryApiResponse>();
            countries.Add(new CountryApiResponse("ID", "Indonesia"));
            countries.Add(new CountryApiResponse("UK", "United Kingdom"));
            countries.Add(new CountryApiResponse("JP", "Japan"));
            return countries;
        }
    }
}
