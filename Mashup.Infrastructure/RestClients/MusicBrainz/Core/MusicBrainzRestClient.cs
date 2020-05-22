using Mashup.Core.Extensions;
using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Core.RestClients.Formats;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Infrastructure.RestClients.MusicBrainz.Core
{
    public class MusicBrainzRestClient : RestClient<Json>
    {
        public MusicBrainzRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
        {
        }

        protected string ModelToEndpoint<TModel>()
        {
            return string.Join("-", typeof(TModel).Name
                .SplitCamelCase()
                .Skip(1)
                .Select(part => part.ToLower()));
        }

        protected async Task<RestResponse<TModel>> GetOneAsync<TModel>(MbId mbId, CancellationToken cancellationToken = default)
                where TModel : MbModel, new()
        {
            string baseUri = $"https://musicbrainz.org/ws/2/{ModelToEndpoint<TModel>()}/{mbId.Value}";
            string requestUri = $"{baseUri}?fmt=json&inc={string.Join("+", new TModel().Includes())}";
            return await GetAsync<TModel>(new Uri(requestUri), cancellationToken);
        }
    }
}
