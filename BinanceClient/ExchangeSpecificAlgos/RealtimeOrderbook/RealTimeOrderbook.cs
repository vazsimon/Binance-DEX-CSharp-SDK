using BinanceClient.Websocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.ExchangeSpecificAlgos
{
    public class RealTimeOrderbook
    {
        public SortedDictionary<decimal, decimal> BidBook { get; set; }
        public SortedDictionary<decimal, decimal> AskBook { get; set; }

        
        
        public RealtimeOrderBookState OrderBookState { get; set; }

        public object operationsLock;
        private object queueLock;
        private object stateLock;

        private long previousUpdateEventTime;

        private string _symbol;
        public string Symbol { get { return _symbol; } }

        private Queue<OrderBookUpdate> UpdateList;

        public event EventHandler<OrderBookUpdatedArgs> OnOrderbookUpdated;

        /// <summary>
        /// Class for storing and handling realtime orderbook data retrieved from http requests and wss streams (through handler class)
        /// </summary>
        /// <param name="symbol"></param>
        public RealTimeOrderbook(string symbol)
        {            
            _symbol = symbol;
            operationsLock = new object();
            queueLock = new object();
            stateLock = new object();
            UpdateList = new Queue<OrderBookUpdate>();

            previousUpdateEventTime = -1;

            SetState(RealtimeOrderBookState.Initializing);            
        }


        public async Task EnqueueUpdateAndProcessSafeAsync(OrderBookUpdate update)
        {
            await Task.Run( () => {
                lock (queueLock)
                {
                    UpdateList.Enqueue(update);
                }
                ProcessUpdatesSafe();
            });
        }

        public OrderBookUpdate dequeueUpdateSafe()
        {
            OrderBookUpdate upd = null;
            lock (queueLock)
            {
                if (UpdateList.Count > 0)
                {
                    upd = UpdateList.Dequeue();
                }                
            }
            return upd;
        }

        internal void SetOutOfSync()
        {
            SetState(RealtimeOrderBookState.OutOfSync);
        }

        public async Task InitAsync(SortedDictionary<decimal, decimal> bidBook, SortedDictionary<decimal, decimal> askBook)
        {
           await Task.Run(() =>
           {
               lock (operationsLock)
               {
                   BidBook = bidBook;
                   AskBook = askBook;
                   SetState(RealtimeOrderBookState.Operational);
               }
               ProcessUpdatesSafe();
           });
        }

      
        private void ProcessUpdatesSafe()
        {            
            lock (operationsLock)
            {
                bool stateSetOK = TestAndSetState(RealtimeOrderBookState.Operational, RealtimeOrderBookState.Updating);
                if (stateSetOK)
                {
                    while (UpdateList.Count > 0)
                    {
                        var upd = dequeueUpdateSafe();
                        if (upd != null)
                        {
                            ProcessUpdate(upd);
                        }
                    }
                    SetState(RealtimeOrderBookState.Operational);
                }
            }            
        }

        private bool TestAndSetState(RealtimeOrderBookState currentStateToTest, RealtimeOrderBookState nextState)
        {
            lock (stateLock)
            {
                if (currentStateToTest == OrderBookState)
                {
                    OrderBookState = nextState;
                    return true;
                }
                return false;
            }
        }

        private void SetState(RealtimeOrderBookState nextState)
        {
            lock (stateLock)
            {
                OrderBookState = nextState;
            }
        }

        private void ProcessUpdate(OrderBookUpdate upd)
        {
            if (previousUpdateEventTime < upd.EventTime)
            {
                foreach (var updAsk in upd.Asks)
                {
                    if (updAsk.Value == 0)
                    {
                        AskBook.Remove(updAsk.Key);
                    }
                    else
                    {
                        AskBook[updAsk.Key] = updAsk.Value;
                    }
                }
                foreach (var updBid in upd.Bids)
                {
                    if (updBid.Value == 0)
                    {
                        BidBook.Remove(updBid.Key);
                    }
                    else
                    {
                        BidBook[updBid.Key] = updBid.Value;
                    }
                }
                previousUpdateEventTime = upd.EventTime;
                var handler = OnOrderbookUpdated; ;
                if (handler != null)
                {
                    var arg = new OrderBookUpdatedArgs { UpdateTime = upd.EventTime };
                    handler(this, arg);
                }
            }
            else
            {
                SetState(RealtimeOrderBookState.OutOfSync);
                throw new WebsocketStreamOutOfSyncException(string.Format("Websocket stream out of sync for {0}", Symbol));
            }
        }
    }
}
