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
    public class Transfer : IWebsocketStream
    {
        Websockets.Websockets _ws;
        public event EventHandler<TransferArgs> OnTransferReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnTransferReceived;
            if (handler != null)
            {
                var msg = JsonConvert.DeserializeObject<TransferMessage>(payload);
                var arg = new TransferArgs { Transfer = msg.Data };
                handler(this, arg);
            }
        }

        public Transfer(Websockets.Websockets ws)
        {
            _ws = ws;
        }

        public void Subscribe(string address)
        {
            var msg = new { method = "subscribe", topic = "transfers", userAddress = address, Address = address };
            _ws.Send(msg);
        }

        public void Unsubscribe(string address)
        {
            var msg = new { method = "unsubscribe", topic = "transfers", userAddress = address, Address = address };
            _ws.Send(msg);
        }
    }

}
