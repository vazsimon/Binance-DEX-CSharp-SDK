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
    public class Orders : IWebsocketStream
    {
        Websockets.WebsocketClient _ws;
        public event EventHandler<OrdersArg> OnOrdersReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnOrdersReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<OrderMessage>(payload);
                var arg = new OrdersArg { OrderUpdates = msg.Data };
                handler(this, arg);
            }
        }

        public Orders(Websockets.WebsocketClient ws)
        {
            _ws = ws;
        }

        public void Subscribe(string address)
        {
            var msg = new { method = "subscribe", topic = "orders", userAddress = address, Address = address };
            _ws.Send(msg);
        }

        public void Unsubscribe(string address)
        {
            var msg = new { method = "unsubscribe", topic = "orders", userAddress = address, Address = address };
            _ws.Send(msg);
        }
    }

}
