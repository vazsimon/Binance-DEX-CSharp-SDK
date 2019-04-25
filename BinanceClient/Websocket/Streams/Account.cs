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
    public class Account : IWebsocketStream
    {
        Websockets.WebsocketClient _ws;
        public event EventHandler<AccountArgs> OnAccountReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnAccountReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<AccountMessage>(payload);
                var arg = new AccountArgs { Account = msg.Data };
                handler(this, arg);
            }
        }

        public Account(Websockets.WebsocketClient ws)
        {
            _ws = ws;
        }

        public void Subscribe(string address)
        {
            var msg = new { method = "subscribe", topic = "accounts", userAddress = address, Address = address };
            _ws.Send(msg);
        }

        public void Unsubscribe(string address)
        {
            var msg = new { method = "unsubscribe", topic = "accounts", userAddress = address, Address = address };
            _ws.Send(msg);
        }
    }

}
