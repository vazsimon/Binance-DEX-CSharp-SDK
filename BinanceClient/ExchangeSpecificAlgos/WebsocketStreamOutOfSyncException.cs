using System;
using System.Runtime.Serialization;

namespace BinanceClient.ExchangeSpecificAlgos
{
    [Serializable]
    internal class WebsocketStreamOutOfSyncException : Exception
    {
        public WebsocketStreamOutOfSyncException()
        {
        }

        public WebsocketStreamOutOfSyncException(string message) : base(message)
        {
        }

        public WebsocketStreamOutOfSyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebsocketStreamOutOfSyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}