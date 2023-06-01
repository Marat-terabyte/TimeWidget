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
using System.Windows;

namespace TimeWidget.ViewModels
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        private string? _currentTime;
        private Config _config;

        public Brush ForegroundColor { get; set; }
        public Brush BackgroundColor { get; set; }

        public RelayCommand SetTimeZoneCommand { get; set; }

        public string? CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        public MainWindowVM()
        {
            SetTimeZoneCommand = new RelayCommand(o => SetTimeZone((string) o));

            _config = LoadAppSettings();

            ForegroundColor = (new BrushConverter().ConvertFromString(_config.ForegroundColor) as Brush)!;
            BackgroundColor = (new BrushConverter().ConvertFromString(_config.BackgroundColor) as Brush)!;

            GetDateTime();
        }

        private void GetDateTime()
        {
            int hours = GetTimeZone();

            Task.Run(() =>
            {
                DateTime time = DateTime.UtcNow.AddHours(hours);
                CurrentTime = time.ToString("T");
                Thread.Sleep(1000);
                
                GetDateTime();
            });
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

        private Config LoadAppSettings()
        {
            var jsonNode = JsonNode.Parse(File.ReadAllText("appsettings.json"));
            Config config = jsonNode.Deserialize<Config>()!;

            return config;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
