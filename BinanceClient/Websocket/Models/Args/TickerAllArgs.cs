using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket.Models
{
    public class TickerAllArgs
    {
        public List<Ticker> Tickers { get; set; }
    }
}
