using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http
{
    class BinanceHTTPApiRequestException : Exception
    {
        public BinanceHTTPApiRequestException()
        {
        }

        public BinanceHTTPApiRequestException(string message) : base(message)
        {
        }

        public BinanceHTTPApiRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BinanceHTTPApiRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
