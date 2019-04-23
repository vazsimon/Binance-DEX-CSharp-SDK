using System.Collections.Generic;

namespace BinanceClient.Websocket.Models
{
    public class OrdersArg
    {
        public List<ExecutionReport> OrderUpdates { get; set; }
    }
}