using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.Wikipedia
{
    public class WpNormalization
    {
        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }
    }
}
