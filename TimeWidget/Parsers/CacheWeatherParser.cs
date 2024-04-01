using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeWidget.Models;

namespace TimeWidget.Parsers
{
    internal class CacheWeatherParser : IParser
    {
        private IParser _parser;
        private Weather _cacheWeather;

        public CacheWeatherParser(IParser parser)
        {
            _parser = parser;
        }

        public Weather? ParseWeather()
        {
            Weather? weather = _parser.ParseWeather();

            if (weather == null || weather.Temperature == string.Empty)
            {
                return _cacheWeather;
            }

            _cacheWeather = weather;

            return weather;
        }
    }
}
