using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.BroadcastTransactions.Models
{
    public enum OrderType
    {
        Limit = 2
    }

    public enum Side
    {
        Buy=1,
        Sell=2
    }

    public enum TimeInForce
    {
        GTE = 1,
        IOC = 3
    }

    public enum VoteOptions
    {
        OptionYes = 1,
        OptionAbstain = 2,
        OptionNo = 3,
        OprionNoWithVeto = 4
    }
}
