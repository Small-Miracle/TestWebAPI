using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Converters
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private const string Format = "yyyy-MM-dd HH:mm:ss UTC";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateStr = reader.GetString();
            DateTime date = DateTime.ParseExact(dateStr, Format, CultureInfo.InvariantCulture);
            return date;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }

}
