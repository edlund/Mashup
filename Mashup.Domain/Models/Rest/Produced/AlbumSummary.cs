using System;

namespace Mashup.Domain.Models.Rest.Produced
{
    public class AlbumSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Released { get; set; }
        public string CoverArtUri { get; set; }
    }
}
