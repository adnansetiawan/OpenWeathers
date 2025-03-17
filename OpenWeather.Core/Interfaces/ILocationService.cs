using OpenWeather.Core.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Core.Interfaces
{
    public interface ILocationService
    {
        /// <summary>
        /// get coutries
        /// </summary>
        /// <returns></returns>
       List<CountryApiResponse> GetCountries();

        /// <summary>
        /// get cities by country
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        List<CityApiResponse> GetCities(string CountryID);

    }
}
