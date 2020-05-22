using System.Net.Http;

namespace Mashup.Core.HttpClients
{
    /**
     * One HttpClient per application
     * https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netcore-3.1
     */
    public interface IHttpClientProvider
    {
        public HttpClient HttpClient { get; }
    }
}
