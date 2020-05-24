using Mashup.Core.Exceptions;
using Mashup.Core.RestClients.Formats;
using System.Net.Http;

namespace Mashup.Core.RestClients
{
    public class RestResponse<TModel>
        where TModel : class
    {
        public string Body { get; }
        public TModel Content { get; }
        public IFormat Format { get; }
        public HttpResponseMessage Message { get; }
        
        public RestResponse(string body, IFormat format, HttpResponseMessage message)
        {
            Body = body;
            Content = message.IsSuccessStatusCode ? format.Deserialize<TModel>(body) : default;
            Format = format;
            Message = message;
        }

        public void EnsureSuccessStatusCode()
        {
            if (!Message.IsSuccessStatusCode)
            {
                throw new RestResponseException($"Request to endpoint \"{Message.RequestMessage.RequestUri}\" failed")
                {
                    StatusCode = Message.StatusCode
                };
            }
        }
    }
}
