using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Domain.Models.Rest.Consumed.Wikidata;
using Mashup.Domain.Models.Rest.Consumed.Wikidata.Core;
using Mashup.Domain.RestClients.Wikidata;
using Mashup.Infrastructure.RestClients.Wikidata.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Infrastructure.RestClients.Wikidata
{
    /**
     * RestClient base class for Wikidata wb/wikibase endpoints.
     */
    public class WdWikibaseRestClient : WikidataRestClient, IWdWikibaseRestClient
    {
        public WdWikibaseRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
        {
        }

        public async Task<RestResponse<WdEntitiesHolder>> GetOneEntitiesHolderAsync(WdId wdId,
            CancellationToken cancellationToken = default)
        {
            string baseUri = "https://www.wikidata.org/w/api.php?action=wbgetentities";
            string requestUri = $"{baseUri}&format=json&props=sitelinks&ids={wdId.Value}";
            return await GetAsync<WdEntitiesHolder>(new Uri(requestUri), cancellationToken);
        }
    }
}
