using Mashup.Core.RestClients;
using Mashup.Domain.Models.Rest.Consumed.Wikipedia;
using Mashup.Domain.Models.Rest.Consumed.Wikipedia.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Domain.RestClients.Wikipedia
{
    public interface IWpQueryRestClient
    {
        public Task<RestResponse<WpQueryHolder>> GetOneQueryHolderAsync(WpTitles wpTitles,
            CancellationToken cancellationToken = default);
    }
}
