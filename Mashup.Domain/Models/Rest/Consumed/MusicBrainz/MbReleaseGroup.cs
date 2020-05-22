using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.MusicBrainz
{
    public class MbReleaseGroup : MbModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonPropertyName("first-release-date")]
        public string FirstReleaseDate { get; set; }

        [JsonPropertyName("primary-type")]
        public string PrimaryType { get; set; }

        [JsonPropertyName("primary-type-id")]
        public string PrimaryTypeId { get; set; }

        [JsonPropertyName("secondary-types")]
        public IEnumerable<string> SecondaryTypes { get; set; }

        [JsonPropertyName("secondary-type-ids")]
        public IEnumerable<string> SecondaryTypeIds { get; set; }
    }
}
