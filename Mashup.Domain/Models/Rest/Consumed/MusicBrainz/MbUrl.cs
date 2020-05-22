using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.MusicBrainz
{
    public class MbUrl : MbModel
    {
        [JsonPropertyName("resource")]
        public string Resource { get; set; }
    }
}
