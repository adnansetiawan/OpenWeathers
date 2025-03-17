using OpenWeather.Core.Constans;
using OpenWeather.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Core.Utilities
{
    public static class TemperaturConverter
    {
        public static double Convert(TemperatureType source, TemperatureType to, double temperatur)
        {
            if(source == to) return temperatur;
            if (source == TemperatureType.Fahrenheit && to == TemperatureType.Celcius)
            {
                return Math.Round((temperatur - 32) * 5 / 9, 2);
            }
            else
            { 
                return Math.Round((temperatur * 9 / 5) + 32, 2);
            }
           
        }
    }
}
