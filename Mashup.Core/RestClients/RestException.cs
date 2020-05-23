using System;
using System.Net;
using System.Runtime.Serialization;

namespace Mashup.Core.RestClients
{
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public RestException()
        {
        }

        public RestException(string message) : base(message)
        {
        }

        public RestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
