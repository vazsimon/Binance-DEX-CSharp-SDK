using BinanceClient.BroadcastTransactions;
using BinanceClient.BroadcastTransactions.Models;
using BinanceClient.Crypto;
using BinanceClient.Http;
using BinanceClient.Http.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient
{
    public class BinanceClient
    {
        private Wallet _wallet;
        public Wallet Wallet { get { return _wallet; } }

        private HTTPClient _httpClient;
        public HTTPClient HTTPClient { get { return _httpClient; } }

        /// <summary>
        /// If this is set to true, the submit broadcast transaction call will wait until the transaction is confirmed on the blockchain. This usually takes
        /// around 1 second. Only turn this off if you are absolutely sure that your transaction will not be rejected by the blockchain.
        /// </summary>
        public bool WaitForTransactionConfirmationOnBroadcast { get; set; } = true;

        public BinanceClient(string privateKey, EnvironmentType network)
        {
            _wallet = new Wallet(privateKey, network);
            _httpClient = new HTTPClient(network);
        }

        public BinanceClient(Wallet wallet)
        {
            _wallet = wallet;
            _httpClient = new HTTPClient(wallet.Env.EnvironmentType);
        }

        public BroadcastResponse FreezeToken(string coin, decimal amount)
        {
            if (Wallet.RequiresVerifySequenceBeforeSend)
            {
                Wallet.RefreshSequence();
            }
            var msg = BroadcastMessageBuilder.BuildTokenFreezeMessage(coin, amount, Wallet);
            var result = HTTPClient.BroadcastToBlockchain(msg,WaitForTransactionConfirmationOnBroadcast);
            if (result.ok)
            {
                _wallet.IncrementSequence();
            }
            return result;
        }

        public BroadcastResponse UnfreezeToken(string coin, decimal amount)
        {
            if (Wallet.RequiresVerifySequenceBeforeSend)
            {
                Wallet.RefreshSequence();
            }
            var msg = BroadcastMessageBuilder.BuildTokenUnfreezeMessage(coin, amount, Wallet);
            var result = HTTPClient.BroadcastToBlockchain(msg, WaitForTransactionConfirmationOnBroadcast);
            if (result.ok)
            {
                _wallet.IncrementSequence();
            }
            return result;
        }

        public BroadcastResponse Vote(int proposal_id, VoteOptions option)
        {
            if (Wallet.RequiresVerifySequenceBeforeSend)
            {
                Wallet.RefreshSequence();
            }
            var msg = BroadcastMessageBuilder.BuildVoteMessage(proposal_id, option, Wallet);
            var result = HTTPClient.BroadcastToBlockchain(msg, WaitForTransactionConfirmationOnBroadcast);
            if (result.ok)
            {
                _wallet.IncrementSequence();
            }
            return result;
        }

        public BroadcastResponse CancelOrder(string symbol, string refId)
        {
            if (Wallet.RequiresVerifySequenceBeforeSend)
            {
                Wallet.RefreshSequence();
            }
            var msg = BroadcastMessageBuilder.BuildCancelOrderMessage(symbol, refId, Wallet);
            var result = HTTPClient.BroadcastToBlockchain(msg, WaitForTransactionConfirmationOnBroadcast);
            if (result.ok)
            {
                _wallet.IncrementSequence();
            }
            return result;
        }

        public BroadcastResponse NewOrder(string symbol, OrderType orderType, Side side, decimal price, decimal qty, TimeInForce tif)
        {
            if (Wallet.RequiresVerifySequenceBeforeSend)
            {
                Wallet.RefreshSequence();
            }
            var msg = BroadcastMessageBuilder.BuildNewOrderMessage(symbol, orderType, side, price,qty, tif, Wallet);
            var result = HTTPClient.BroadcastToBlockchain(msg, WaitForTransactionConfirmationOnBroadcast);
            if (result.ok)
            {
                _wallet.IncrementSequence();
            }
            return result;
        }

        public BroadcastResponse Send(string toAddress, string coin, decimal amount,string memo = "")
        {
            if (Wallet.RequiresVerifySequenceBeforeSend)
            {
                Wallet.RefreshSequence();
            }
            var msg = BroadcastMessageBuilder.BuildSendOneMessage(toAddress,coin, amount, Wallet,memo);
            var result = HTTPClient.BroadcastToBlockchain(msg, WaitForTransactionConfirmationOnBroadcast);
            if (result.ok)
            {
                _wallet.IncrementSequence();
            }
            return result;
        }


        /// <summary>
        /// Sending possibly many types coins to possibly many addresses, in one single transaction
        /// </summary>
        /// <param name="destinations"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public BroadcastResponse BatchSend(List<MultipleSendDestination> destinations,  string memo = "")
        {
            if (Wallet.RequiresVerifySequenceBeforeSend)
            {
                Wallet.RefreshSequence();
            }
            var msg = BroadcastMessageBuilder.BuildSendMultipleMessage(destinations, Wallet, memo);
            var result = HTTPClient.BroadcastToBlockchain(msg, WaitForTransactionConfirmationOnBroadcast);
            if (result.ok)
            {
                _wallet.IncrementSequence();
            }
            return result;
        }
    }
}
