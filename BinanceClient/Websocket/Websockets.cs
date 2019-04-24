using BinanceClient.Crypto;
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
        public AllMiniTicker AllMiniTicker { get; set; }
        public IndividualMiniTicker IndividualMiniTicker { get; set; }
        public AllTicker AllTicker { get; set; }
        public IndividualTicker IndividualTicker { get; set; }
        public Klines Klines { get; set; }
        public BookDepth BookDepth { get; set; }
        public DiffDepth DiffDepth { get; set; }
        public Trades Trades { get; set; }
        public Transfer Transfer { get; set; }
        public Account Account { get; set; }
        public Orders Orders { get; set; }

        public Websockets(Network network)
        {
            var networkEnv = BinanceEnvironment.GetEnvironment(network);
            _ws = new WebSocket(networkEnv.WssApiAddress);
            _ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            _ws.EmitOnPing = true;

            _ws.OnMessage += _ws_OnMessage;
            _ws.OnError += _ws_OnError;

            BlockHeight = new BlockHeight(this);
            AllMiniTicker = new AllMiniTicker(this);
            IndividualMiniTicker = new IndividualMiniTicker(this);
            AllTicker = new AllTicker(this);
            IndividualTicker = new IndividualTicker(this);
            Klines = new Klines(this);
            BookDepth = new BookDepth(this);
            DiffDepth = new DiffDepth(this);
            Trades = new Trades(this);
            Transfer = new Transfer(this);
            Account = new Account(this);
            Orders = new Orders(this);
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
            //Pre-parsing message. Doesn't fully deserialize now to dynamic to improve performance
            if (e.Data.StartsWith("{\"stream\":\"orders\""))
            {
                Orders.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"trades\""))
            {
                Trades.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"accounts\""))
            {
                Account.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"marketDiff\""))
            {
                DiffDepth.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"marketDepth\""))
            {
                BookDepth.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"blockheight\""))
            {
                BlockHeight.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"allMiniTickers\""))
            {
                AllMiniTicker.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"miniTicker\""))
            {
                IndividualMiniTicker.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"allTickers\""))
            {
                AllTicker.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"ticker\""))
            {
                IndividualTicker.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"kline_"))
            {
                Klines.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"transfers\""))
            {
                Transfer.ProcessRecievedMessage(e.Data);
            }
            else if (e.Data.StartsWith("{\"stream\":\"transfers\""))
            {
                Transfer.ProcessRecievedMessage(e.Data);
            }
            else if (!string.IsNullOrWhiteSpace(e.Data))
            {
                //We might received an error text from backend, have to raise attention if so.                
                if (e.Data.Contains("error"))
                {
                    throw new WebSocketConnectionException(string.Format("Websocket DEX backend sent error message: {0}",e.Data));
                }
            }
        }

        public void Connect()
        {
            _ws.Connect();
        }
    }
}
