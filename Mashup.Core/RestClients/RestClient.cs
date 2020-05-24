using Mashup.Core.HttpClients;
using Mashup.Core.RestClients.Formats;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Core.RestClients
{
    /// <summary>
    /// Base class for REST clients.It wraps some boilerplate
    /// code for dealing with HttpClient.
    /// </summary>
    /// <remarks>
    /// FIXME Add support for formats other than JSON (XML, YAML).
    /// FIXME Add support for authentication.
    /// </remarks>
    /// <typeparam name="TFormat">The data format of the REST API.</typeparam>
    public abstract class RestClient<TFormat>
        where TFormat : IFormat, new()
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
                where TResponseModel : class
                where TRequestModel : class
        {
            var requestBody = new StringContent(_format.Serialize(requestModel), RequestEncoding, _format.MediaType);
            var message = await action(uri, requestBody, cancellationToken);
            var responseBody = await message.Content.ReadAsStringAsync();
            return new RestResponse<TResponseModel>(responseBody, _format, message);
        }

        protected async Task<RestResponse<TResponseModel>> ExecuteWithoutRequestBodyAsync<TResponseModel>(
            Func<Uri, CancellationToken, Task<HttpResponseMessage>> action, Uri uri, CancellationToken cancellationToken = default)
                where TResponseModel : class
        {
            var message = await action(uri, cancellationToken);
            var responseBody = await message.Content.ReadAsStringAsync();
            return new RestResponse<TResponseModel>(responseBody, _format, message);
        }

        public async Task<RestResponse<TResponseModel>> GetAsync<TResponseModel>(Uri uri,
            CancellationToken cancellationToken = default)
                where TResponseModel : class => await ExecuteWithoutRequestBodyAsync<TResponseModel>(
                    HttpClientProvider.HttpClient.GetAsync, uri, cancellationToken);

        public async Task<RestResponse<TResponseModel>> DeleteAsync<TResponseModel>(Uri uri,
            CancellationToken cancellationToken = default)
                where TResponseModel : class => await ExecuteWithoutRequestBodyAsync<TResponseModel>(
                    HttpClientProvider.HttpClient.DeleteAsync, uri, cancellationToken);

        public async Task<RestResponse<TResponseModel>> PatchAsync<TResponseModel, TRequestModel>(
            Uri uri, TRequestModel requestModel, CancellationToken cancellationToken = default)
                where TResponseModel : class
                where TRequestModel : class => await ExecuteWithRequestBodyAsync<TResponseModel, TRequestModel>(
                    HttpClientProvider.HttpClient.PostAsync, uri, requestModel, cancellationToken);

        public async Task<RestResponse<TResponseModel>> PostAsync<TResponseModel, TRequestModel>(
            Uri uri, TRequestModel requestModel, CancellationToken cancellationToken = default)
                where TResponseModel : class
                where TRequestModel : class => await ExecuteWithRequestBodyAsync<TResponseModel, TRequestModel>(
                    HttpClientProvider.HttpClient.PostAsync, uri, requestModel, cancellationToken);

        public async Task<RestResponse<TResponseModel>> PutAsync<TResponseModel, TRequestModel>(
            Uri uri, TRequestModel requestModel, CancellationToken cancellationToken = default)
                where TResponseModel : class
                where TRequestModel : class => await ExecuteWithRequestBodyAsync<TResponseModel, TRequestModel>(
                    HttpClientProvider.HttpClient.PutAsync, uri, requestModel, cancellationToken);
    }
}
