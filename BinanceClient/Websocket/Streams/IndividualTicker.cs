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
    public class IndividualTicker : IWebsocketStream
    {
        Websockets.WebsocketClient _ws;
        public event EventHandler<TickerIndividualArgs> OnTickerIndividualReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnTickerIndividualReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<IndividualSymbolsTickerMessage>(payload);
                var arg = new TickerIndividualArgs { Ticker = msg.Data };
                handler(this, arg);
            }
        }

        public IndividualTicker(Websockets.WebsocketClient ws)
        {
            _ws = ws;
        }

        public void Subscribe(string symbol)
        {
            var msg = new { method = "subscribe", topic = "ticker", symbols = new[] { symbol } };
            _ws.Send(msg);
        }

        public void Unsubscribe(string symbol)
        {
            var msg = new { method = "unsubscribe", topic = "ticker", symbols = new[] { symbol } };
            _ws.Send(msg);
        }
    }

}
