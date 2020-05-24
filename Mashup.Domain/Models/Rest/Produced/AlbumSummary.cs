using Mashup.Core.Models;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Produced
{
    public class AlbumSummary : ProducedModelBase
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("released")]
        public string Released { get; set; }

#nullable enable
        [JsonPropertyName("coverArtUri")]
        public string? CoverArtUri { get; set; }
#nullable restore
    }
}
