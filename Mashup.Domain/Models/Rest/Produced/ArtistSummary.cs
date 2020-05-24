using Mashup.Core.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Produced
{
    public class ArtistSummary : ProducedModelBase
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

#nullable enable
        [JsonPropertyName("extract")]
        public string? Extract { get; set; }
#nullable restore

        [JsonPropertyName("albums")]
        public IEnumerable<AlbumSummary> Albums { get; set; }
    }
}
