using Mashup.Core.RestClients;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Domain.RestClients.MusicBrainz
{
    public interface IMbArtistRestClient
    {
        public Task<RestResponse<MbArtist>> GetOneAsync(MbId mbId, CancellationToken cancellationToken = default);
    }
}
