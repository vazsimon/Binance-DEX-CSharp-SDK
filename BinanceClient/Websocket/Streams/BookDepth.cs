using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinanceClient.Websocket.Models;
using BinanceClient.Websocket.Models.Args;
using BinanceClient.Websockets;
using Newtonsoft.Json;

namespace BinanceClient.Websocket
{
    public class BookDepth : IWebsocketStream
    {
        Websockets.Websockets _ws;
        public event EventHandler<BookDepthArgs> OnBookDepthReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnBookDepthReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<BookDepthMessage>(payload);
                var arg = new BookDepthArgs { OrderBookSnapshot = msg.Data };
                handler(this, arg);
            }
        }

        public BookDepth(Websockets.Websockets ws)
        {
            _ws = ws;
        }

        public void Subscribe(string symbol)
        {
            var msg = new { method = "subscribe", topic = "marketDepth", symbols = new[] { symbol } };
            _ws.Send(msg);
        }

        public void Unsubscribe(string symbol)
        {
            var msg = new { method = "unsubscribe", topic = "marketDepth", symbols = new[] { symbol } };
            _ws.Send(msg);
        }
    }

}
