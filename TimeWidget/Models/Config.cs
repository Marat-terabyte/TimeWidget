using System.Text.Json.Serialization;

namespace TimeWidget.Models
{
    class Config
    {
        private string _backgroundColor = "Transparent";
        private string _foregroundColor = "#fff";
        private string _timeZone = "МСК";

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
    }
}
