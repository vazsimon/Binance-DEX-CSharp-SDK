using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket
{
    interface IWebsocketStream
    {
        void ProcessRecievedMessage(string payload);       

    }
}
