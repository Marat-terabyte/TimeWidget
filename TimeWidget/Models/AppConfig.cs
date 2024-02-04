using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimeWidget.Models
{
    internal class AppConfig
    {
        private string _backgroundColor = "Transparent";
        private string _foregroundColor = "#fff";
        private string _borderColor = "Transparent";
        private string _timeZone = "UTC+3";
        private string _weatherPlace = "Moscow";

        private int _weatherVisibility = 0;

        private double _windowWidth = 450;
        private double _windowHeight = 170;
        private double _borderThickness = 1;
        private double _timeFontSize = 72;
        private double _weatherFontSize = 30;

        [JsonPropertyName("window-width")]
        public double WindowWidth
        {
            get => _windowWidth;
            set
            {
                if (value == 0)
                    value = 450;

                _windowWidth = value;
            }
        }

        [JsonPropertyName("window-height")]
        public double WindowHeight
        {
            get => _windowHeight;
            set
            {
                if (value == 0)
                    value = 170;

                _windowHeight = value;
            }
        }

        [JsonPropertyName("border-thickness")]
        public double BorderThickness
        {
            get => _borderThickness;
            set
            {
                if (value == 0)
                    value = 1;

                _borderThickness = value;
            }
        }

        [JsonPropertyName("time-font-size")]
        public double TimeFontSize
        {
            get => _timeFontSize;
            set
            {
                if (value == 0)
                    value = 72;

                _timeFontSize = value;
            }
        }

        [JsonPropertyName("weather-font-size")]
        public double WeatherFontSize
        {
            get => _weatherFontSize;
            set
            {
                if (value == 0)
                    value = 30;

                _weatherFontSize = value;
            }
        }

        [JsonPropertyName("weather-visibility")]
        public int WeatherVisibility
        {
            get => _weatherVisibility;
            set
            {
                if (value > 2)
                    value = 0;

                _weatherVisibility = value;
            }
        }

        [JsonPropertyName("background-color")]
        public string BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (value is null)
                    value = "Transparent";

                _backgroundColor = value;
            }
        }

        [JsonPropertyName("foreground-color")]
        public string ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                if (value is null)
                    value = "#fff";

                _foregroundColor = value;
            }
        }

        [JsonPropertyName("border-color")]
        public string BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (value is null)
                    value = "#fff";

                _borderColor = value;
            }
        }

        [JsonPropertyName("time-zone")]
        public string TimeZone
        {
            get => _timeZone;
            set
            {
                if (value is null)
                    value = "МСК";

                _timeZone = value;
            }
        }

        [JsonPropertyName("weather-place")]
        public string WeatherPlace
        {
            get => _weatherPlace;
            set
            {
                if (value is null)
                    value = "Moscow";

                _weatherPlace = value;
            }
        }

    }
}
