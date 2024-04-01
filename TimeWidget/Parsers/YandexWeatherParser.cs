using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeWidget.Models;
using HtmlAgilityPack;
using System.Windows;

namespace TimeWidget.Parsers
{
    internal class YandexWeatherParser : IParser
    {
        private static string _weatherUrl = @"https://yandex.ru/pogoda/";

        public Weather? ParseWeather()
        {
            Weather weather = new Weather();

            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(_weatherUrl);
                var nodes = doc.DocumentNode.SelectNodes("//span[@class='temp__value temp__value_with-unit']");

                weather.Temperature = nodes?[1].InnerText ?? string.Empty;
            }
            catch { }

            return null;
        }
    }
}
