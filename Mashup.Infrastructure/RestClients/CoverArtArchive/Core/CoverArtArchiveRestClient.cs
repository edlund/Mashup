using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Core.RestClients.Formats;

namespace Mashup.Infrastructure.RestClients.CoverArtArchive.Core
{
    public class CoverArtArchiveRestClient : RestClient<Json>
    {
        public CoverArtArchiveRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
        {
        }
    }
}
