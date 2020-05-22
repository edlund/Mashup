using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Domain.Models.Rest.Consumed.Wikipedia;
using Mashup.Domain.Models.Rest.Consumed.Wikipedia.Core;
using Mashup.Domain.RestClients.Wikipedia;
using Mashup.Infrastructure.RestClients.Wikipedia.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Infrastructure.RestClients.Wikipedia
{
    public class WpQueryRestClient : WikipediaRestClient, IWpQueryRestClient
    {
        public WpQueryRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
        {
        }

        public async Task<RestResponse<WpQueryHolder>> GetOneQueryHolderAsync(WpTitles wpTitles,
            CancellationToken cancellationToken = default)
        {
            string baseUri = "https://en.wikipedia.org/w/api.php?format=json&action=query";
            string requestUri = $"{baseUri}&prop=extracts&exintro=true&redirects=true&titles={wpTitles.Value}";
            return await GetAsync<WpQueryHolder>(new Uri(requestUri), cancellationToken);
        }
    }
}
