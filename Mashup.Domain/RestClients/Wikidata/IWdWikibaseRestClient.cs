using Mashup.Core.RestClients;
using Mashup.Domain.Models.Rest.Consumed.Wikidata;
using Mashup.Domain.Models.Rest.Consumed.Wikidata.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Domain.RestClients.Wikidata
{
    public interface IWdWikibaseRestClient
    {
        public Task<RestResponse<WdEntitiesHolder>> GetOneEntitiesHolderAsync(WdId wdId,
            CancellationToken cancellationToken = default);
    }
}
