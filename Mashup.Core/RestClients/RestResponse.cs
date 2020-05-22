using System.Net;
using System.Net.Http;

namespace Mashup.Core.RestClients
{
    public class RestResponse<T>
    {
        public string Body { get; }
        public T Content { get; }
        public bool IsSuccessStatusCode { get; }
        public string ReasonPhrase { get; }
        public HttpStatusCode StatusCode { get; }

        public RestResponse(string body, T content, HttpResponseMessage message)
        {
            Body = body;
            Content = content;
            IsSuccessStatusCode = message.IsSuccessStatusCode;
            ReasonPhrase = message.ReasonPhrase;
            StatusCode = message.StatusCode;
        }
    }
}
