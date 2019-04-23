using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket.Models.Args
{
    public class DiffDepthArgs
    {
        public OrderBookUpdate OrderBookUpdate { get; set; }
    }
}
