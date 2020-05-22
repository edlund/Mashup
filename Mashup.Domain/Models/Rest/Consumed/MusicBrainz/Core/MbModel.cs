using Mashup.Core.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core
{
    public abstract class MbModel : ModelBase
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public virtual IEnumerable<string> Includes() => new List<string>();
    }
}
