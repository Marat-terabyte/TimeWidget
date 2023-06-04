using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using TimeWidget.Models;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Threading;
using HtmlAgilityPack;
using System.Windows;

namespace TimeWidget.ViewModels
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        private string _weatherUrl = "https://yandex.ru/pogoda/";

        private Brush _foregroundColor;
        private Brush _backgroundColor;
        private Brush _borderColor;

        private int _weatherVisibility;

        private double _windowWidth;
        private double _windowHeight;

        private double _borderThickness;
        private double _timeFontSize;

        private double _weatherFontSize;

        private string _weatherPlace;
        private string _temperature;

        private string _currentTime;
        private Config _config;

        public Brush ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value;
                OnPropertyChanged();
            }
        }

        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }
        }

        public Brush BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                OnPropertyChanged();
            }
        }

        public int WeatherVisibility
        {
            get => _weatherVisibility;
            set
            {
                _weatherVisibility = value;
                OnPropertyChanged();
            }
        }

        public double WindowWidth
        {
            get => _windowWidth;
            set
            {
                _windowWidth = value;
                OnPropertyChanged();
            }
        }

        public double WindowHeight
        {
            get => _windowHeight;
            set
            {
                _windowHeight = value;
                OnPropertyChanged();
            }
        }

        public double BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = value;
                OnPropertyChanged();
            }
        }

        public double TimeFontSize
        {
            get => _timeFontSize;
            set
            {
                _timeFontSize = value;
                OnPropertyChanged();
            }
        }

        public double WeatherFontSize
        {
            get => _weatherFontSize;
            set
            {
                _weatherFontSize = value;
                OnPropertyChanged();
            }
        }

        public string WeatherPlace
        {
            get => _weatherPlace;
            set
            {
                _weatherPlace = value;
                OnPropertyChanged();
            }
        }

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        } 

        public RelayCommand SetTimeZoneCommand { get; set; }
        public RelayCommand SetConfigCommand { get; set; }

        public MainWindowVM()
        {
            SetTimeZoneCommand = new RelayCommand(o => SetTimeZone((string) o));
            SetConfigCommand   = new RelayCommand(o => SetConfig());
            
            InitFields();
            
            GetDateTime();
            ParseWeather();
        }

        private void GetDateTime()
        {
            int hours = GetTimeZone();

            Task.Run(() =>
            {
                while (true)
                {
                    DateTime time = DateTime.UtcNow.AddHours(hours);
                    CurrentTime = time.ToString("T");
                    Thread.Sleep(1000);
                }
            });
        }

        private void ParseWeather()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        Temperature = GetTemperature(_weatherUrl);
                    }
                    catch { }

                    Thread.Sleep(150000);
                }
            });
        }

        private string GetTemperature(string url)
        {
            string temperature = string.Empty;

            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(url + WeatherPlace);
                var nodes = doc.DocumentNode.SelectNodes("//span[@class='temp__value temp__value_with-unit']");

                temperature = nodes[1].InnerText;
            }
            catch { }

            return temperature; 
        }

        private int GetTimeZone()
        {
            var TimeZones = new Dictionary<string, int>
            {

                { "UTC-1", -1 }, { "UTC", 0 },
                { "UTC+1", 1 },  { "UTC+2", 2 },
                { "UTC+3", 3 },  { "МСК-1", 2 },
                { "МСК", 3 },    { "МСК+1", 4 },
                { "МСК+2", 5 },  { "МСК+3", 6 },
                { "МСК+4", 7 },  { "МСК+5", 8 },
                { "МСК+6", 9 },  { "МСК+7", 10 },
                { "МСК+8", 11 }, { "МСК+9", 12 },
            };

            return TimeZones[_config.TimeZone];
        
        }

        private void SetTimeZone(string timeZone)
        {
            _config.TimeZone = timeZone;
            _config.RewriteConfigFile();
        }

        private async void SetConfig()
        {
            await Task.Run(() =>
            {
                string notepad = @"C:\Windows\System32\notepad.exe";
                var process = Process.Start(notepad, "appsettings.json");
                process.WaitForExit();
            });

            InitFields();
        }

        private Config LoadAppSettings()
        {
            string json = File.ReadAllText("appsettings.json");
            
            var jsonNode = JsonNode.Parse(json);
            Config config = jsonNode.Deserialize<Config>()!;

            return config;
        }

        private void InitFields()
        {
            _config = LoadAppSettings();

            WindowWidth  = _config.WindowWidth;
            WindowHeight = _config.WindowHeight;

            ForegroundColor = (new BrushConverter().ConvertFromString(_config.ForegroundColor) as Brush)!;
            BackgroundColor = (new BrushConverter().ConvertFromString(_config.BackgroundColor) as Brush)!;
            BorderColor     = (new BrushConverter().ConvertFromString(_config.BorderColor) as Brush)!;

            WeatherFontSize   = _config.WeatherFontSize;
            WeatherPlace      = _config.WeatherPlace;
            WeatherVisibility = _config.WeatherVisibility;

            BorderThickness = _config.BorderThickness;
            TimeFontSize    = _config.TimeFontSize;

            Temperature = GetTemperature(_weatherUrl);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
