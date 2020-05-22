using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.Wikidata
{
    public class WdEntitiesHolder
    {
        [JsonPropertyName("entities")]
        public IDictionary<string, WdEntity> Entities { get; set; }
    }
}
