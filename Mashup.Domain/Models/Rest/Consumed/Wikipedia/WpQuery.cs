using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.Wikipedia
{
    public class WpQuery
    {
        [JsonPropertyName("normalized")]
        public IEnumerable<WpNormalization> Normalized { get; set; }

        [JsonPropertyName("pages")]
        public IDictionary<string, WpPage> Pages { get; set; }
    }
}
