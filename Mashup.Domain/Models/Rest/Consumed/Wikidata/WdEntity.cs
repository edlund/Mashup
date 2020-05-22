using Mashup.Domain.Models.Rest.Consumed.Wikidata.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.Wikidata
{
    public class WdEntity : WdModel
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("sitelinks")]
        public IDictionary<string, WdSiteLink> SiteLinks { get; set; }

        public WdSiteLink EnSiteLink() => SiteLinks.FirstOrDefault(pair => pair.Key == "enwiki").Value;
    }
}
