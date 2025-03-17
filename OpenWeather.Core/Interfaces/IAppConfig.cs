using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeather.Core.Interfaces
{
    public interface IAppConfig
    {
        string? AppId { get; }
    }
}
