using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.Wikipedia
{
    public class WpQueryHolder
    {
        [JsonPropertyName("query")]
        public WpQuery Query { get; set; }
    }
}
