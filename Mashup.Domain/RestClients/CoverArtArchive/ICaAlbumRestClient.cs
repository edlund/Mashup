using Mashup.Core.RestClients;
using Mashup.Domain.Models.Rest.Consumed.CoverArtArchive;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Domain.RestClients.CoverArtArchive
{
    public interface ICaAlbumRestClient
    {
        public Task<RestResponse<CaAlbum>> GetOneAsync(MbId mbId, CancellationToken cancellationToken = default);
    }
}
