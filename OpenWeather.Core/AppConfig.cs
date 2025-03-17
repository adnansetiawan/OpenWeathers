using Microsoft.Extensions.Configuration;
using OpenWeather.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeather.Core
{
    public class AppConfig : IAppConfig
    {
        private readonly IConfiguration _configuration;
        public AppConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string AppId => _configuration["AppId"].ToString();
    }
}
