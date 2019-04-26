using BinanceClient.Websocket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace BinanceClient.NodeRPC
{
    public class WebsocketJsonRpcConnector
    {
        private WebSocket _ws;
        public bool Connected { get { return _ws.IsAlive; } }

        private long id;
        private Dictionary<long, dynamic> responses;
        private Dictionary<long, ManualResetEventSlim> resetEvents;
        private object preparationLock;


        public WebsocketJsonRpcConnector(string address)
        {
            _ws = new WebSocket(address);
            _ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            _ws.EmitOnPing = true;

            _ws.OnMessage += _ws_OnMessage;
            _ws.OnError += _ws_OnError;
            id = 0;
            responses = new Dictionary<long, dynamic>();
            resetEvents = new Dictionary<long, ManualResetEventSlim>();
            preparationLock = new object();
            if (!Connected)
            {
                _ws.Connect();
            }
        }

        private void _ws_OnError(object sender, ErrorEventArgs e)
        {
            throw new WebSocketConnectionException("Error occured on RPC websocket stream", e.Exception);
        }

        public dynamic GetResponse(string method)
        {

            string s;
            long currentId;
            ManualResetEventSlim mre;
            lock (preparationLock)
            {
                id++;
                currentId = id;
                mre = new ManualResetEventSlim(false);
                resetEvents[id] = mre;
                var newMsg = new { jsonrpc = "2.0", method = method, id = id };
                s = JsonConvert.SerializeObject(newMsg);                
            }
            if (!Connected)
            {
                _ws.Connect();
            }
            _ws.Send(s);
            mre.Wait();
            if (responses.ContainsKey(currentId))
            {
                var returnVal = responses[currentId];
                responses.Remove(currentId);
                return returnVal;
            }
            return null;
        }

        private void _ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (!e.IsPing)
            {
                dynamic msg = JsonConvert.DeserializeObject(e.Data);
                responses[msg.id.Value] = msg.result;
                resetEvents[msg.id.Value].Set();
                resetEvents.Remove(msg.id.Value);
            }            
        }
    }
}
