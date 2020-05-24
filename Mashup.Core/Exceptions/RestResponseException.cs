using System;
using System.Runtime.Serialization;

namespace Mashup.Core.Exceptions
{
    public class RestResponseException : HttpResponseException
    {
        public RestResponseException()
        {
        }

        public RestResponseException(string message) : base(message)
        {
        }

        public RestResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RestResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
