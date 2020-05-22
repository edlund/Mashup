using System.Net.Http;

namespace Mashup.Core.HttpClients
{
    public class HttpClientProvider : IHttpClientProvider
    {
        public HttpClient HttpClient { get; } = null;

        public HttpClientProvider() : this(new HttpClient())
        {
        }

        public HttpClientProvider(HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mashup/1.0 (Contact: erik.edlund@32767.se)");
        }
    }
}
