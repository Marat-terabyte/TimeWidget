using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeWidget.Models;

namespace TimeWidget.Parsers
{
    internal interface IParser
    {
        public Weather ParseWeather();
    }
}
