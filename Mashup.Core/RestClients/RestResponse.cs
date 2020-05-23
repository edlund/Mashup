using System.Net.Http;

namespace Mashup.Core.RestClients
{
    public class RestResponse<T>
    {
        public string Body { get; }
        public T Content { get; }
        public HttpResponseMessage Message { get; }
        
        public RestResponse(string body, T content, HttpResponseMessage message)
        {
            Body = body;
            Content = content;
            Message = message;
        }

        public void EnsureSuccessStatusCode()
        {
            if (!Message.IsSuccessStatusCode)
            {
                throw new RestException($"Request to endpoint \"{Message.RequestMessage.RequestUri}\" failed")
                {
                    StatusCode = Message.StatusCode
                };
            }
        }
    }
}
