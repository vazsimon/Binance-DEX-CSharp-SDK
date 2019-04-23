using System.Collections.Generic;

namespace BinanceClient.Websocket.Models
{
    public class TradesArgs
    {
        public List<Trade> Trades { get; set; }
    }
}