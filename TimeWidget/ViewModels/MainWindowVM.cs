using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TimeWidget.Command;
using TimeWidget.Models;
using TimeWidget.Parsers;
using TimeWidget.Views;

namespace TimeWidget.ViewModels
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        private string _currentTime;
        private Weather _currentWeather;

        public ICommand EditAppSettingsCommand {  get; set; }
        public AppSettings Settings { get; set; }
        public IParser WeatherParser { get; set; }
        
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        public Weather CurrentWeather
        {
            get => _currentWeather;
            set
            {
                _currentWeather = value;
                OnPropertyChanged();
            }
        }

        public MainWindowVM()
        {
            Settings = new AppSettings();
            EditAppSettingsCommand = new RelayCommand(o => EditAppSettings());
            WeatherParser = new YandexWeatherParser();

            GetTime();
            GetWeather();
        }

        public async void EditAppSettings()
        {
            while (true)
            {
                await Settings.OpenAppSettings();

                string? errorMessage = null;
                
                bool isValidAppSettings = Settings.CheckAppSettingsWithResult(ref errorMessage);
                if (isValidAppSettings)
                {
                    Settings.Update();
                    await Task.Run(() => { CurrentWeather = WeatherParser.ParseWeather(); });
                    break;

                }

                var result = MessageBox.Show(errorMessage + "\n\nRewrite automatically app settings?", "Error", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Settings.WriteAppSettings();
                    Settings.Update();
                    break;
                }
            }
        }

        public void GetWeather()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var weather = WeatherParser.ParseWeather();
                    if (weather != null)
                        CurrentWeather = weather;

                    Thread.Sleep(1_800_000); // 30 min
                }
            });
        }

        public void GetTime()
        {
            int hours = GetTimeZone();

            Task.Run(() =>
            {
                while (true)
                {
                    CurrentTime = DateTime.UtcNow.AddHours(hours).ToString("T");
                    Thread.Sleep(950);
                }
            });
        }

        private int GetTimeZone()
        {
            var TimeZones = new Dictionary<string, int>
            {
                { "UTC-11", 1 },  { "UTC-12", 2 },
                { "UTC-9", 3 },  { "UTC-10", 4 },
                { "UTC-7", 1 },  { "UTC-8", 2 },
                { "UTC-5", 3 },  { "UTC-6", 4 },
                { "UTC-3", 1 },  { "UTC-4", 2 },
                { "UTC-1", -1 },  { "UTC-2", -2 },
                { "UTC", 0 },
                { "UTC+1", 1 },  { "UTC+2", 2 },
                { "UTC+3", 3 },  { "UTC+4", 4 },
                { "UTC+5", 1 },  { "UTC+6", 2 },
                { "UTC+7", 3 },  { "UTC+8", 4 },
                { "UTC+9", 1 },  { "UTC+10", 2 },
                { "UTC+11", 3 },  { "UTC+12", 4 },
            };

            return TimeZones[Settings.Config.TimeZone];

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
