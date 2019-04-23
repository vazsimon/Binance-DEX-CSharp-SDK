using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinanceClient.Enums;
using BinanceClient.Http.Get.Models;
using BinanceClient.Websocket.Models;
using BinanceClient.Websockets;
using Newtonsoft.Json;

namespace BinanceClient.Websocket
{
    public class Klines : IWebsocketStream
    {
        Websockets.Websockets _ws;
        public event EventHandler<KlineArgs> OnKlineReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnKlineReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<KlineMessage>(payload);
                var arg = new KlineArgs { Kline = msg.Data.Kline };
                handler(this, arg);
            }
        }

        public Klines(Websockets.Websockets ws)
        {
            _ws = ws;
        }

        public void Subscribe(string symbol, KlineInterval interval)
        {
            var intervalStr = string.Format("kline_{0}", TransferNameConverter.Convert(interval));
            var msg = new { method = "subscribe", topic = intervalStr, symbols = new[] { symbol } };
            _ws.Send(msg);
        }

        public void Unsubscribe(string symbol, KlineInterval interval)
        {
            var intervalStr = string.Format("kline_{0}", TransferNameConverter.Convert(interval));
            var msg = new { method = "unsubscribe", topic = intervalStr, symbols = new[] { symbol } };
            _ws.Send(msg);
        }
    }

}
