using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.CoverArtArchive
{
    public class CaImage
    {
        /*
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        */
        [JsonPropertyName("front")]
        public bool Front { get; set; }

        [JsonPropertyName("back")]
        public bool Back { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("thumbnails")]
        public IDictionary<string, string> Thumbnails { get; set; }
    }
}
