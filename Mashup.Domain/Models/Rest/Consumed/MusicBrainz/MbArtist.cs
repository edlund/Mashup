using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.MusicBrainz
{
    public class MbArtist : MbModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sort-name")]
        public string SortName { get; set; }

        [JsonPropertyName("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("type-id")]
        public string TypeId { get; set; }

        [JsonPropertyName("relations")]
        public IEnumerable<MbRelation> Relations { get; set; }

        [JsonPropertyName("release-groups")]
        public IEnumerable<MbReleaseGroup> ReleaseGroups { get; set; }

        [JsonPropertyName("area")]
        public MbArea Area { get; set; }

        [JsonPropertyName("begin-area")]
        public MbArea BeginArea { get; set; }

        [JsonPropertyName("end-area")]
        public MbArea EndArea { get; set; }

        [JsonPropertyName("life-span")]
        public MbLifeSpan LifeSpan { get; set; }
#nullable enable
        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("gender-id")]
        public string? GenderId { get; set; }
#nullable restore

        public MbRelation WikidataRelation => Relations
            .FirstOrDefault(relation => relation.Type == "wikidata");

        public override IEnumerable<string> Includes() => new List<string>
        {
            "release-groups",
            "url-rels"
        };
    }
}
