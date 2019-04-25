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
    public class BlockHeight : IWebsocketStream
    {
        Websockets.WebsocketClient _ws;
        public event EventHandler<BlockHeightArgs> OnBlockHeightReceived;

        public void ProcessRecievedMessage(string payload)
        {
            var handler = OnBlockHeightReceived;
            if (handler != null)
            {
                dynamic msg = JsonConvert.DeserializeObject(payload);
                var bha = new BlockHeightArgs { BlockHeight = msg.data.h.Value };
                handler(this, bha);
            }
        }

        public BlockHeight(Websockets.WebsocketClient ws)
        {
            _ws = ws;
        }

        public void Subscribe()
        {
            var msg = new { method = "subscribe", topic = "blockheight", symbols = new[] { "$all" } };
            _ws.Send(msg);
        }

        public void Unsubscribe()
        {
            var msg = new { method = "unsubscribe", topic = "blockheight", symbols = new[] { "$all" } };
            _ws.Send(msg);
        }
    }
    
}
