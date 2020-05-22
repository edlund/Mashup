using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.MusicBrainz
{
    /**
     * This is technically an Artist-Url relation, which is
     * requested by the RestClient from the MusicBrainz API.
     * This is obviously a sub-optimal solution; our model should
     * not be dependent on the parameters sent to the API for
     * its correctness...
     * 
     * The "right" way to do this would be to make this a base
     * class and then create different concrete classes for the
     * different relation types that the API can return. This
     * would be neat, but requires custom deserialization,
     * so that the correct models are instantiated.
     * 
     * The "dirty"/"ugly" way to do this would be to add all
     * possible fields for relations to this model.
     * 
     * For now, knowing that the RestClient will always perform
     * the correct queries that produce responses matching our
     * model, this will do.
     */
    public class MbRelation
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("type-id")]
        public string TypeId { get; set; }

        [JsonPropertyName("target-type")]
        public string TargetType { get; set; }

        [JsonPropertyName("url")]
        public MbUrl Url { get; set; }
    }
}
