using System;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.MusicBrainz
{
    public class MbLifeSpan
    {
        [JsonPropertyName("begin")]
        public string Begin { get; set; }

        [JsonPropertyName("end")]
        public string End { get; set; }

        [JsonPropertyName("ended")]
        public bool Ended { get; set; }
    }
}
