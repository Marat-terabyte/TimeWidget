using System.IO;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TimeWidget.Models
{
    class Config
    {
        private string _backgroundColor = "Transparent";
        private string _foregroundColor = "#fff";
        private string _borderColor = "Transparent";
        private string _timeZone = "МСК";

        private double _windowWidth = 250;
        private double _windowHeight = 170;
        private double _borderThickness = 1;
        private double _timeFontSize = 72;

        [JsonPropertyName("window-width")]
        public double WindowWidth
        {
            get { return _windowWidth; }
            set
            {
                if (value == 0)
                    _windowWidth = 450;
                else
                    _windowWidth = value;
            }
        }

        [JsonPropertyName("window-height")]
        public double WindowHeight
        {
            get { return _windowHeight; }
            set
            {
                if (value == 0)
                    _windowHeight = 170;
                else
                    _windowHeight = value;
            }
        }

        [JsonPropertyName("background-color")]
        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                if (value is null)
                    _backgroundColor = "Transparent";
                else
                    _backgroundColor = value;
            }
        }

        [JsonPropertyName("foreground-color")]
        public string ForegroundColor
        {
            get { return _foregroundColor; }
            set
            {
                if (value is null)
                    _foregroundColor = "#fff";
                else
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
                    _borderColor = "#fff";
                else
                    _borderColor = value;
            }
        }

        [JsonPropertyName("border-thickness")]
        public double BorderThickness
        {
            get { return _borderThickness; }
            set
            {
                if (value == 0)
                    _borderThickness = 1;
                else
                    _borderThickness = value;
            }
        }

        [JsonPropertyName("time-font-size")]
        public double TimeFontSize
        {
            get { return _timeFontSize; }
            set
            {
                if (value == 0)
                    _timeFontSize = 72;
                else
                    _timeFontSize = value;
            }
        }

        [JsonPropertyName("time-zone")]
        public string TimeZone
        {
            get => _timeZone;
            set
            {
                if (value is null)
                    _timeZone = "МСК";
                else
                    _timeZone = value;
            }
        }

        public void RewriteConfigFile()
        {
            Task.Run(() =>
            {
                string json = JsonSerializer.Serialize<Config>(this, new JsonSerializerOptions() { WriteIndented = true });

                using (FileStream fs = new FileStream("appsettings.json", FileMode.Truncate))
                    fs.Write(Encoding.UTF8.GetBytes(json), 0, json.Length);
            });
        }
    }
}
