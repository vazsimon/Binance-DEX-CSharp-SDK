using BinanceClient.BroadcastTransactions;
using BinanceClient.BroadcastTransactions.Models;
using BinanceClient.Crypto;
using BinanceClient.Http;
using BinanceClient.Http.Post.Models;
using BinanceClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient
{
    public class Client
    {
        private Wallet _wallet;
        public Wallet Wallet { get { return _wallet; } }

        private HTTPClient _httpClient;
        public HTTPClient HTTPClient { get { return _httpClient; } }

        public SequenceEnsureMode sequenceEnsureMode { get; set; }



        public bool WaitForTransactionConfirmationOnBroadcast { get {return sequenceEnsureMode == SequenceEnsureMode.VerifyBeforeSendAndWaitForConfirmation
                    || sequenceEnsureMode == SequenceEnsureMode.WaitForConfirmation; } }
        public bool VerifySequenceBeforeSend { get { return sequenceEnsureMode == SequenceEnsureMode.VerifyBeforeSend 
                    || sequenceEnsureMode == SequenceEnsureMode.VerifyBeforeSendAndWaitForConfirmation; } }

        //Lock object to ensure only one message is being created and submitted at a time to ensure sequence for transaction relplay protection.
        private object BroadcastLockObject;

        public Client(string privateKey, Network network,SequenceEnsureMode sequenceEnsureMode = SequenceEnsureMode.VerifyBeforeSendAndWaitForConfirmation)
        {
            _wallet = new Wallet(privateKey, network);
            _httpClient = new HTTPClient(network);
            this.sequenceEnsureMode = sequenceEnsureMode;
            BroadcastLockObject = new object();
        }

        public Client(Wallet wallet, SequenceEnsureMode sequenceEnsureMode = SequenceEnsureMode.VerifyBeforeSendAndWaitForConfirmation)
        {
            _wallet = wallet;
            _httpClient = new HTTPClient(wallet.Env.EnvironmentType);
            this.sequenceEnsureMode = sequenceEnsureMode;
            BroadcastLockObject = new object();
        }

        public async Task<BroadcastResponse> FreezeTokenAsync(string coin, decimal amount)
        {
            //Running the whole process atomically to ensure proper sequence
            var br = await Task<BroadcastResponse>.Run(() =>
            {
                return FreezeToken(coin, amount);
            });
            return br;                      
        }

        public BroadcastResponse FreezeToken(string coin, decimal amount)
        {
            //Ensure no other broadcast transaction interferes with sequence until it hit the blockchain
            lock (BroadcastLockObject)
            {
                if (VerifySequenceBeforeSend)
                {
                    Wallet.RefreshSequence();
                }
                var msg = BroadcastMessageBuilder.BuildTokenFreezeMessage(coin, amount, Wallet);
                var result = HTTPClient.BroadcastToBlockchain(msg, WaitForTransactionConfirmationOnBroadcast);
                if (result.ok)
                {
                    _wallet.IncrementSequence();
                }
                return result;
            }
        }


        public BroadcastResponse UnfreezeToken(string coin, decimal amount)
        {
            //Ensure no other broadcast transaction interferes with sequence until it hit the blockchain
            lock (BroadcastLockObject)
            {
                if (VerifySequenceBeforeSend)
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
        }


        public async Task<BroadcastResponse> UnfreezeTokenAsync(string coin, decimal amount)
        {
            //Running the whole process atomically to ensure proper sequence
            var br = await Task<BroadcastResponse>.Run(() =>
            {
                return UnfreezeToken(coin, amount);
            });
            return br;
        }

        public BroadcastResponse Vote(int proposal_id, VoteOptions option)
        {
            //Ensure no other broadcast transaction interferes with sequence until it hit the blockchain
            lock (BroadcastLockObject)
            {
                if (VerifySequenceBeforeSend)
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
        }

        public async Task<BroadcastResponse> VoteAsync(int proposal_id, VoteOptions option)
        {
            //Running the whole process atomically to ensure proper sequence
            var br = await Task<BroadcastResponse>.Run(() =>
            {
                return Vote(proposal_id, option);
            });
            return br;
        }

        public BroadcastResponse CancelOrder(string symbol, string refId)
        {
            //Ensure no other broadcast transaction interferes with sequence until it hit the blockchain
            lock (BroadcastLockObject)
            {
                if (VerifySequenceBeforeSend)
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
        }

        public async Task<BroadcastResponse> CancelOrderAsync(string symbol, string refId)
        {
            //Running the whole process atomically to ensure proper sequence
            var br = await Task<BroadcastResponse>.Run(() =>
            {
                return CancelOrder(symbol, refId);
            });
            return br;
        }

        public BroadcastResponse NewOrder(string symbol, OrderType orderType, Side side, decimal price, decimal qty, TimeInForce tif)
        {
            //Ensure no other broadcast transaction interferes with sequence until it hit the blockchain
            lock (BroadcastLockObject)
            {
                if (VerifySequenceBeforeSend)
                {
                    Wallet.RefreshSequence();
                }
                var msg = BroadcastMessageBuilder.BuildNewOrderMessage(symbol, orderType, side, price, qty, tif, Wallet);
                var result = HTTPClient.BroadcastToBlockchain(msg, WaitForTransactionConfirmationOnBroadcast);
                if (result.ok)
                {
                    _wallet.IncrementSequence();
                }
                return result;
            }
        }


        public async Task<BroadcastResponse> NewOrderAsync(string symbol, OrderType orderType, Side side, decimal price, decimal qty, TimeInForce tif)
        {
            //Running the whole process atomically to ensure proper sequence
            var br = await Task<BroadcastResponse>.Run(() =>
            {
                return NewOrder(symbol, orderType, side, price, qty, tif);
            });
            return br;
        }

        public BroadcastResponse Send(string toAddress, string coin, decimal amount,string memo = "")
        {
            //Ensure no other broadcast transaction interferes with sequence until it hit the blockchain
            lock (BroadcastLockObject)
            {
                if (VerifySequenceBeforeSend)
                {
                    Wallet.RefreshSequence();
                }
                var msg = BroadcastMessageBuilder.BuildSendOneMessage(toAddress, coin, amount, Wallet, memo);
                var result = HTTPClient.BroadcastToBlockchain(msg, WaitForTransactionConfirmationOnBroadcast);
                if (result.ok)
                {
                    _wallet.IncrementSequence();
                }
                return result;
            }
        }

        public async Task<BroadcastResponse> SendAsync(string toAddress, string coin, decimal amount, string memo = "")
        {
            //Running the whole process atomically to ensure proper sequence
            var br = await Task<BroadcastResponse>.Run(() =>
            {
                return Send(toAddress, coin, amount, memo = "");
            });
            return br;
        }


        /// <summary>
        /// Sending possibly many types coins to possibly many addresses, in one single transaction
        /// </summary>
        /// <param name="destinations"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public BroadcastResponse MultiSend(List<MultipleSendDestination> destinations,  string memo = "")
        {
            //Ensure no other broadcast transaction interferes with sequence until it hit the blockchain
            lock (BroadcastLockObject)
            {
                if (VerifySequenceBeforeSend)
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

        public async Task<BroadcastResponse> MultiSendAsync(List<MultipleSendDestination> destinations, string memo = "")
        {
            //Running the whole process atomically to ensure proper sequence
            var br = await Task<BroadcastResponse>.Run(() =>
            {
                return MultiSend(destinations, memo);
            });
            return br;
        }
    }
}
