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
    public class AllTicker : IWebsocketStream
    {
        Websockets.WebsocketClient _ws;
        public event EventHandler<TickerAllArgs> OnTickerAllReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnTickerAllReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<AllSymbolsTickerMessage>(payload);
                var arg = new TickerAllArgs { Tickers = msg.Data };
                handler(this, arg);
            }
        }

        public AllTicker(Websockets.WebsocketClient ws)
        {
            _ws = ws;
        }

        public void Subscribe()
        {
            var msg = new { method = "subscribe", topic = "allTickers", symbols = new[] { "$all" } };
            _ws.Send(msg);
        }

        public void Unsubscribe()
        {
            var msg = new { method = "unsubscribe", topic = "allTickers", symbols = new[] { "$all" } };
            _ws.Send(msg);
        }
    }

}
