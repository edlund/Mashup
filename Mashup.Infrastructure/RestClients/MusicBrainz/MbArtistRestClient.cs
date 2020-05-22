using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using Mashup.Domain.RestClients.MusicBrainz;
using Mashup.Infrastructure.RestClients.MusicBrainz.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Infrastructure.RestClients.MusicBrainz
{
    public class MbArtistRestClient : MusicBrainzRestClient, IMbArtistRestClient
    {
        public MbArtistRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
        {
        }

        public async Task<RestResponse<MbArtist>> GetOneAsync(MbId mbId, CancellationToken cancellationToken = default)
        {
            return await GetOneAsync<MbArtist>(mbId, cancellationToken);
        }
    }
}
