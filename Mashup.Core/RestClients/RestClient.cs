using Mashup.Core.HttpClients;
using Mashup.Core.RestClients.Formats;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Core.RestClients
{
    /**
     * Base class for REST clients. It wraps some boilerplate
     * code for dealing with HttpClient.
     * 
     * FIXME Add support for formats other than JSON (XML, YAML).
     * FIXME Add support for authentication.
     */
    public abstract class RestClient<TFormat> where TFormat : IFormat, new()
    {
        private readonly IFormat _format;

        public IHttpClientProvider HttpClientProvider { get; set; }

        public Encoding RequestEncoding { get; set; }

        public RestClient(IHttpClientProvider httpClientProvider)
        {
            _format = new TFormat();
            HttpClientProvider = httpClientProvider;
            RequestEncoding = Encoding.UTF8;
        }

        protected async Task<RestResponse<TResponseModel>> ExecuteWithRequestBodyAsync<TResponseModel, TRequestModel>(
            Func<Uri, StringContent, CancellationToken, Task<HttpResponseMessage>> action, Uri uri,
            TRequestModel requestModel, CancellationToken cancellationToken = default)
                where TResponseModel : class where TRequestModel : class
        {
            var request = new StringContent(_format.Serialize(requestModel), RequestEncoding, "application/json");
            var message = await action(uri, request, cancellationToken);
            var responseBody = await message.Content.ReadAsStringAsync();
            TResponseModel responseModel = message.IsSuccessStatusCode ? _format.Deserialize<TResponseModel>(responseBody) : default;
            return new RestResponse<TResponseModel>(responseBody, responseModel, message);
        }

        protected async Task<RestResponse<TResponseModel>> ExecuteWithoutRequestBodyAsync<TResponseModel>(
            Func<Uri, CancellationToken, Task<HttpResponseMessage>> action, Uri uri, CancellationToken cancellationToken = default)
                where TResponseModel : class
        {
            var message = await action(uri, cancellationToken);
            var responseBody = await message.Content.ReadAsStringAsync();
            TResponseModel responseModel = message.IsSuccessStatusCode ? _format.Deserialize<TResponseModel>(responseBody) : default;
            return new RestResponse<TResponseModel>(responseBody, responseModel, message);
        }

        public async Task<RestResponse<TResponseModel>> GetAsync<TResponseModel>(Uri uri, CancellationToken cancellationToken = default)
            where TResponseModel : class => await ExecuteWithoutRequestBodyAsync<TResponseModel>(
                HttpClientProvider.HttpClient.GetAsync, uri, cancellationToken);

        public async Task<RestResponse<TResponseModel>> DeleteAsync<TResponseModel>(Uri uri, CancellationToken cancellationToken = default)
            where TResponseModel : class => await ExecuteWithoutRequestBodyAsync<TResponseModel>(
                HttpClientProvider.HttpClient.DeleteAsync, uri, cancellationToken);

        public async Task<RestResponse<TResponseModel>> PatchAsync<TResponseModel, TRequestModel>(
            Uri uri, TRequestModel requestModel, CancellationToken cancellationToken = default)
                where TResponseModel : class where TRequestModel : class => await ExecuteWithRequestBodyAsync<TResponseModel, TRequestModel>(
                    HttpClientProvider.HttpClient.PostAsync, uri, requestModel, cancellationToken);

        public async Task<RestResponse<TResponseModel>> PostAsync<TResponseModel, TRequestModel>(
            Uri uri, TRequestModel requestModel, CancellationToken cancellationToken = default)
                where TResponseModel : class where TRequestModel : class => await ExecuteWithRequestBodyAsync<TResponseModel, TRequestModel>(
                    HttpClientProvider.HttpClient.PostAsync, uri, requestModel, cancellationToken);

        public async Task<RestResponse<TResponseModel>> PutAsync<TResponseModel, TRequestModel>(
            Uri uri, TRequestModel requestModel, CancellationToken cancellationToken = default)
                where TResponseModel : class where TRequestModel : class => await ExecuteWithRequestBodyAsync<TResponseModel, TRequestModel>(
                    HttpClientProvider.HttpClient.PutAsync, uri, requestModel, cancellationToken);
    }
}
