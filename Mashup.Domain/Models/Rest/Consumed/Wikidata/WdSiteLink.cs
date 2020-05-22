using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.Wikidata
{
    public class WdSiteLink
    {
        [JsonPropertyName("site")]
        public string Site { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
