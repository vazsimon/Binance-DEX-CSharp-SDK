using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
   public enum QueryLimit
    {
        five = 5,
        ten = 10,
        twenty = 20,
        fifty = 50,
        hundred = 100,
        fiveHundred = 500,
        thousand = 1000,
        All = 0
    }

    public enum KlineInterval
    {
        m1,
        m3,
        m5,
        m15,
        m30,
        h1,
        h2,
        h4,
        h6,
        h8,
        h12,
        d1,
        d3,
        w1,
        M1
    }

    public enum OrderStatusQuery
    {
        All = 0,
        Ack,
        PartialFill,
        IocNoFill,
        FullyFill,
        Canceled,
        Expired,
        FailedBlocking,
        FailedMatching,        
    }

    public enum OrderStatus
    {
        Ack,
        PartialFill,
        IocNoFill,
        FullyFill,
        Canceled,
        Expired,
        FailedBlocking,
        FailedMatching,
    }

    public enum OrderSideQuery
    {
        All = 0,
        Buy = 1,
        Sell = 2        
    }

    public enum OrderSide
    {
        Buy = 1,
        Sell = 2
    }

    public enum TransactionSide
    {
        RECEIVE,
        SEND,
        All
    }

    public enum TxType
    {
        NEW_ORDER,
        ISSUE_TOKEN,
        BURN_TOKEN,
        LIST_TOKEN,
        CANCEL_ORDER,
        FREEZE_TOKEN,
        UN_FREEZE_TOKEN,
        TRANSFER,
        PROPOSAL,
        VOTE,
        MINT,
        DEPOSIT,
        All
    }

    public static class TransferNameConverter
    {
        public static string Convert(KlineInterval interval)
        {
            var s = interval.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(s[1]);
            sb.Append(s[0]);
            return sb.ToString();
        }
    }
}
