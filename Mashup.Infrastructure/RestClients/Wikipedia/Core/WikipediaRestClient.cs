using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Core.RestClients.Formats;

namespace Mashup.Infrastructure.RestClients.Wikipedia.Core
{
    public class WikipediaRestClient : RestClient<Json>
    {
        public WikipediaRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
        {
        }
    }
}
