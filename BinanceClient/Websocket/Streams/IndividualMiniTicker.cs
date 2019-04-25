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
    public class IndividualMiniTicker : IWebsocketStream
    {
        Websockets.WebsocketClient _ws;
        public event EventHandler<MiniTickerIndividualArgs> OnMiniTickerIndividualReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnMiniTickerIndividualReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<IndividualSymbolMiniTickerMessage>(payload);
                var arg = new MiniTickerIndividualArgs { MiniTicker = msg.Data };
                handler(this, arg);
            }
        }

        public IndividualMiniTicker(Websockets.WebsocketClient ws)
        {
            _ws = ws;
        }

        public void Subscribe(string symbol)
        {
            var msg = new { method = "subscribe", topic = "miniTicker", symbols = new[] { symbol } };
            _ws.Send(msg);
        }

        public void Unsubscribe(string symbol)
        {
            var msg = new { method = "unsubscribe", topic = "miniTicker", symbols = new[] { symbol } };
            _ws.Send(msg);
        }
    }

}
