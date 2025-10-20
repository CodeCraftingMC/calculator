using System.Text.Json.Serialization;

namespace Calculator
{
    public class Theme
    {
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color PrimaryBackground { get; set; }
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color SecondaryBackground { get; set; }
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color AccentColor { get; set; }
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color FontColor { get; set; }
        public float FontSize { get; set; } = 12;
        public string? FontFamily { get; set; }
        public bool IsDarkMode { get; set; }
    }
}
