using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimeWidget
{
    internal static class Weather
    {
        private static string _weatherUrl = "https://yandex.ru/pogoda/";

        public static string GetTemperature(string place)
        {
            string temperature = string.Empty;

            try
            {
                var doc = Parse(place);
                var nodes = doc.DocumentNode.SelectNodes("//span[@class='temp__value temp__value_with-unit']");

                temperature = nodes[1].InnerText;
            }
            catch { }

            return temperature;
        }

        private static HtmlDocument Parse(string place)
        {
            var web = new HtmlWeb();
            var doc = web.Load(_weatherUrl + place);

            return doc;
        }
    }
}
