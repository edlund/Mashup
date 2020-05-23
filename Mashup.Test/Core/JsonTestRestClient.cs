using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Core.RestClients.Formats;

namespace Mashup.Test.Core
{
    public class JsonTestRestClient : RestClient<Json>
    {
        public JsonTestRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
        {
        }
    }
}
