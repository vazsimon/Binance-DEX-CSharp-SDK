using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.ExchangeSpecificAlgos
{
    public enum RealtimeOrderBookState
    {
        Initializing,
        Operational,
        Updating,
        OutOfSync
    }
}
