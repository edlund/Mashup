using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.Wikipedia
{
    public class WpPage
    {
        [JsonPropertyName("pageid")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("extract")]
        public string Extract { get; set; }
    }
}
