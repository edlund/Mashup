using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Mashup.Domain.Models.Rest.Consumed.CoverArtArchive
{
    public class CaAlbum
    {
        [JsonPropertyName("images")]
        public IEnumerable<CaImage> Images { get; set; }
        
        [JsonPropertyName("release")]
        public string Release { get; set; }

        public CaImage FrontCover => Images.FirstOrDefault(image => image.Front);

        public CaImage BackCover => Images.FirstOrDefault(image => image.Back);
    }
}
