using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket.Models
{
    public class MiniTickerAllArgs
    {
        public List<MiniTicker> Tickers { get; set; }
    }
}
