using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TimeWidget.Models
{
    internal class AppSettings
    {
        private string _settingsFile = "appsettings.json";

        public AppConfig Config { get; }

        public AppSettings()
        {
            Config = new AppConfig();

            if (!File.Exists(_settingsFile) || new FileInfo(_settingsFile).Length == 0)
            {
                WriteAppSettings();
            }
            else
            {
                AppConfig config = ReadAppSettings();

                Config.BackgroundColor = config.BackgroundColor;
                Config.ForegroundColor = config.ForegroundColor;
                Config.BorderColor = config.BorderColor;
                Config.TimeZone = config.TimeZone;
                Config.WeatherPlace = config.WeatherPlace;
                Config.WeatherVisibility = config.WeatherVisibility;
                Config.WindowWidth = config.WindowWidth;
                Config.WindowHeight = config.WindowHeight;
                Config.BorderThickness = config.BorderThickness;
                Config.TimeFontSize = config.TimeFontSize;
                Config.WeatherFontSize = config.WeatherFontSize;
            }
        }

        public AppConfig ReadAppSettings() => JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(_settingsFile))!;

        public void WriteAppSettings()
        {
            string json = JsonSerializer.Serialize(Config, new JsonSerializerOptions { WriteIndented = true });
            using (FileStream fs = new FileStream(_settingsFile, FileMode.Create))
            {
                fs.Write(Encoding.UTF8.GetBytes(json));
            }
        }
    }
}
