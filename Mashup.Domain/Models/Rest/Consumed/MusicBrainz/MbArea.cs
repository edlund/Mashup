using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.MusicBrainz
{
    public class MbArea : MbModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sort-name")]
        public string SortName { get; set; }

        [JsonPropertyName("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonPropertyName("iso-3166-1-codes")]
        public IEnumerable<string> Iso31661Codes { get; set; }
    }
}
