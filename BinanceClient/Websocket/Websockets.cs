using BinanceClient.Websocket;
using BinanceClient.Websocket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace BinanceClient.Websockets
{
    public class Websockets
    {
        private WebSocket _ws;
        public bool Connected { get { return _ws.IsAlive; } }
        public BlockHeight BlockHeight { get; set; }


        public Websockets(string url)
        {
            _ws = new WebSocket(url);
            _ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            _ws.EmitOnPing = true;

            _ws.OnMessage += _ws_OnMessage;
            _ws.OnError += _ws_OnError;

            BlockHeight = new BlockHeight(this);
        }

        public void Send(dynamic msg)
        {
            if (!Connected)
            {
                _ws.Connect();
            }
            string s = JsonConvert.SerializeObject(msg);
            _ws.Send(s);
        }


        private void _ws_OnError(object sender, ErrorEventArgs e)
        {
            throw new WebSocketConnectionException("Error occured on websocket stream", e.Exception);
        }

        private void _ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Data.StartsWith("{\"stream\":\"blockheight\""))
            {
                BlockHeight.ProcessRecievedMessage(e.Data);
            }
        }

        public void Connect()
        {
            _ws.Connect();
        }
    }
}
