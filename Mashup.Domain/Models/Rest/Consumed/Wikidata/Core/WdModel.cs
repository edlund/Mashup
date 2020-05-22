using Mashup.Core.Models;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.Wikidata.Core
{
    public abstract class WdModel : ModelBase
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
