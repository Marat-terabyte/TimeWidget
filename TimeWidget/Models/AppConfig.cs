using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimeWidget.Models
{
    internal class AppConfig : INotifyPropertyChanged
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
