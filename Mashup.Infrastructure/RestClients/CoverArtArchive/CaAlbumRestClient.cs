using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Domain.Models.Rest.Consumed.CoverArtArchive;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using Mashup.Domain.RestClients.CoverArtArchive;
using Mashup.Infrastructure.RestClients.CoverArtArchive.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Infrastructure.RestClients.CoverArtArchive
{
    public class CaAlbumRestClient : CoverArtArchiveRestClient, ICaAlbumRestClient
    {
        public CaAlbumRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
        {
        }

        public async Task<RestResponse<CaAlbum>> GetOneAsync(MbId mbId, CancellationToken cancellationToken = default)
        {
            string baseUri = "http://coverartarchive.org/release-group";
            string requestUri = $"{baseUri}/{mbId.Value}";
            return await GetAsync<CaAlbum>(new Uri(requestUri), cancellationToken);
        }
    }
}
