using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.BroadcastTransactions
{
    public class MultipleSendDestination
    {
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public string coin { get; set; }
    }
}
