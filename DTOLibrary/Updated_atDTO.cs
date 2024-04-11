using Converters;
using System.Text.Json.Serialization;

namespace DTOLibrary
{
    public class Updated_atDTO
    {
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Previous { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Current { get; set; }
    }
}
