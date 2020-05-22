using System.Collections.Generic;

namespace Mashup.Domain.Models.Rest.Produced
{
    public class ArtistSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
#nullable enable
        public string? Extract { get; set; }
#nullable restore
        public IEnumerable<AlbumSummary> Albums { get; set; }
    }
}
