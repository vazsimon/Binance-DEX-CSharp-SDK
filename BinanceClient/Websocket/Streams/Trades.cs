using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinanceClient.Websocket.Models;
using BinanceClient.Websockets;
using Newtonsoft.Json;

namespace BinanceClient.Websocket
{
    public class Trades : IWebsocketStream
    {
        Websockets.WebsocketClient _ws;
        public event EventHandler<TradesArgs> OnTradesReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnTradesReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<TradesMessage>(payload);
                var arg = new TradesArgs { Trades = msg.Data };
                handler(this, arg);
            }
        }

        public Trades(Websockets.WebsocketClient ws)
        {
            _ws = ws;
        }

        public void Subscribe(string symbol)
        {
            var msg = new { method = "subscribe", topic = "trades", symbols = new[] { symbol } };
            _ws.Send(msg);
        }

        public void Unsubscribe(string symbol)
        {
            var msg = new { method = "unsubscribe", topic = "trades", symbols = new[] { symbol } };
            _ws.Send(msg);
        }
    }

}
