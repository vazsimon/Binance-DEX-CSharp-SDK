using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket.Models
{
    class WebSocketConnectionException : Exception
    {
        public WebSocketConnectionException()
        {
        }

        public WebSocketConnectionException(string message) : base(message)
        {
        }

        public WebSocketConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebSocketConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
