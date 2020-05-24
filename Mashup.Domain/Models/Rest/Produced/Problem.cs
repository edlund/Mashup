using Mashup.Core.Models;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Produced
{
    public class Problem : ProducedModelBase
    {
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }        
    }
}
