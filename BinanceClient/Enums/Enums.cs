using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Enums
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
        Unknown
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

    /// <summary>
    /// We need to maintain the account's sequence number to comply with the transaction replay protection
    /// </summary>
    public enum SequenceEnsureMode
    {
        /// <summary>
        /// Safest choice.. For non time-critical use-cases to ensures that no sequence or transaction issues go unnoticed. 
        /// </summary>
        VerifyBeforeSendAndWaitForConfirmation,
        /// <summary>
        /// Safe for sequence nubmer but we don't get an error if the transaction is rejected. For non time-critical use-cases. Before every call, we retrieve the actual sequence from the http api.
        /// Still delays the broadcast of the transaction. Also, this option ensures proper sequence number if other app is using the same private key
        /// </summary>
        VerifyBeforeSend,
        /// <summary>
        /// Optimal balance for normal use, default behavior. Only good if no other app is using the private key. After every call, we see if the transaction got recorded to the blockchain or not.
        /// If it was successful, we increase the sequencenumber. There is no delay (verification) before entering transactions (fastest order entry), but afterwards, we
        /// have to wait until the transaction is recorded on the blockchain to see if we need to increase the sequence.
        /// </summary>
        WaitForConfirmation,
        /// <summary>
        /// For extreme time critical scenarios, use with extra caution. The client assumes that all transactions are accepted by the network, doesn't do
        /// anything time consuming to verify. If a transaction fails, the programmer has to manually reset the sequence number in the wallet.
        /// </summary>
        Manual
    }


    public enum RPCBroadcastMode
    {
        /// <summary>
        /// This method just return transaction hash right away and there is no return from CheckTx or DeliverTx.
        /// </summary>
        async,
        /// <summary>
        /// The transaction will be broadcasted and returns with the response from CheckTx and DeliverTx.
        /// This method will waitCONTRACT: only returns error if mempool.CheckTx() errs or if we timeout waiting for tx to commit.
        /// If CheckTx or DeliverTx fail, no error will be returned, but the returned result will contain a non-OK ABCI code.
        /// </summary>
        commit,
        /// <summary>
        /// The transaction will be broadcasted and returns with the response from CheckTx.
        /// </summary>
        sync
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
