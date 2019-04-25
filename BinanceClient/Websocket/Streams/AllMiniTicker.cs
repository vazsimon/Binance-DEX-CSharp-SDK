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
    public class AllMiniTicker : IWebsocketStream
    {
        Websockets.WebsocketClient _ws;
        public event EventHandler<MiniTickerAllArgs> OnMiniTickerAllReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnMiniTickerAllReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<AllSymbolsMiniTickerMessage>(payload);
                var arg = new MiniTickerAllArgs { Tickers = msg.Data };
                handler(this, arg);
            }
        }

        public AllMiniTicker(Websockets.WebsocketClient ws)
        {
            _ws = ws;
        }

        public void Subscribe()
        {
            var msg = new { method = "subscribe", topic = "allMiniTickers", symbols = new[] { "$all" } };
            _ws.Send(msg);
        }

        public void Unsubscribe()
        {
            var msg = new { method = "unsubscribe", topic = "allMiniTickers", symbols = new[] { "$all" } };
            _ws.Send(msg);
        }
    }

}
