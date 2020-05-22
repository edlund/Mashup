using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Core.RestClients.Formats;

namespace Mashup.Infrastructure.RestClients.Wikidata.Core
{
    public class WikidataRestClient : RestClient<Json>
    {
        public WikidataRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
        {
        }
    }
}
