using System.Text.Json;
using System.Text.Json.Serialization;

namespace Calculator
{
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var hex = reader.GetString();
            if (string.IsNullOrWhiteSpace(hex))
                return Color.Empty;

            if (hex.StartsWith('#'))
                hex = hex[1..];

            if (hex.Length == 8) // RGBA
            {
                byte r = Convert.ToByte(hex[..2], 16);
                byte g = Convert.ToByte(hex.Substring(2, 2), 16);
                byte b = Convert.ToByte(hex.Substring(4, 2), 16);
                byte a = Convert.ToByte(hex.Substring(6, 2), 16);
                return Color.FromArgb(a, r, g, b);
            }

            throw new JsonException($"Invalid RGBA color format: #{hex}");
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            string hex = $"#{value.R:X2}{value.G:X2}{value.B:X2}{value.A:X2}";
            writer.WriteStringValue(hex);
        }
    }
}
