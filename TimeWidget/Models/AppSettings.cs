using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

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
                if (!CheckAppSettings())
                {
                    WriteAppSettings();
                }

                Update();
            }
        }

        public AppConfig ReadAppSettings() => JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(_settingsFile))!;

        public void Update()
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

        public bool CheckAppSettingsWithResult(ref string? message)
        {
            try
            {
                ReadAppSettings();
                message = null;

                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return false;
        }

        public bool CheckAppSettings()
        {
            try
            {
                ReadAppSettings();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void WriteAppSettings()
        {
            string json = JsonSerializer.Serialize(Config, new JsonSerializerOptions { WriteIndented = true });
            using (FileStream fs = new FileStream(_settingsFile, FileMode.Create))
            {
                fs.Write(Encoding.UTF8.GetBytes(json));
            }
        }

        public async Task OpenAppSettings()
        {

            string notepad = @"C:\Windows\System32\notepad.exe";
            var process = Process.Start(notepad, "appsettings.json");
            await process.WaitForExitAsync();
        }
    }
}
