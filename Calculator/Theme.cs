using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        public bool IsDarkMode { get; set; }
    }
}
