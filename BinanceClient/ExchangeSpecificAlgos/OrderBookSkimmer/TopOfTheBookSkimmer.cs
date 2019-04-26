using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinanceClient.Enums;
using BinanceClient.Websocket;
using Newtonsoft.Json;

namespace BinanceClient.ExchangeSpecificAlgos.OrderBookSkimmer
{
    public class TopOfTheBookSkimmer
    {
        private Client _client;
        private string _symbol;
        public string Symbol { get { return _symbol; } }
        private RealTimeOrderbook _realTimeOrderBook;
        private object tradingLock;

        public TopOfTheBookSkimmer(string symbol, Client client)
        {
            _symbol = symbol;
            _client = client;
            client.RTBooks.Subscribe(symbol);
            _realTimeOrderBook = client.RTBooks.BooksForSymbols[symbol];
            _realTimeOrderBook.OnOrderbookUpdated += _realTimeOrderBook_OnOrderbookUpdated;
            AmountToHandle = 0;
            tradingLock = new object();
            //setting up preconditions initially to execute buy
            _orderBookUpdateReceived = true;
            _orderUpdateReceived = true;           
        }


        //symbol level subscription, can happen direcly in here, in the symbol-specific skimmer
        private void _realTimeOrderBook_OnOrderbookUpdated(object sender, OrderBookUpdatedArgs e)
        {
            // check if ready to buy, update flag
            _orderBookUpdateReceived = true;
            CheckConditionsAndProcess();
        }

        public decimal AmountToHandle { get; set; }
        private bool _orderUpdateReceived { get; set; }
        private bool _orderBookUpdateReceived { get; set; }

        internal void Accumulate(decimal amount)
        {
            AmountToHandle += amount;
            Task.Run(() => { CheckConditionsAndProcess(); });
        }

        internal void Distribute(decimal amount)
        {
            AmountToHandle -= amount;
            Task.Run(() => { CheckConditionsAndProcess(); });
        }

        internal async Task ProcessOrderAsync(ExecutionReport update)
        {
            await Task.Run(() =>
            {
                if (update.Side == Enums.OrderSide.Buy && (update.OrderStatus == OrderStatus.FullyFill || update.OrderStatus == OrderStatus.PartialFill))
                {
                    //cumulative will be OK, as we are using IOC orders
                    AmountToHandle -= update.CumulativeFillQuantity;
                }
                else if (update.Side == Enums.OrderSide.Sell && (update.OrderStatus == OrderStatus.FullyFill || update.OrderStatus == OrderStatus.PartialFill))
                {
                    AmountToHandle += update.CumulativeFillQuantity;
                }
                _orderUpdateReceived = true;
                CheckConditionsAndProcess();
                //update status of process, update flag
            });
        }

        private void CheckConditionsAndProcess()
        {
            if (AmountToHandle != 0)
            {
                lock (tradingLock)
                {
                    if (_orderUpdateReceived && _orderBookUpdateReceived)
                    {
                        //reset preconditions for next iteration
                        _orderUpdateReceived = false;
                        _orderBookUpdateReceived = false;
                        //we can send out new top of the book order
                        decimal newOrderPrice= 0;
                        decimal newOrderQty = 0;
                        lock (_realTimeOrderBook.operationsLock)
                        {
                            //acquiring an operationlock to ensure the realtime processes don't update it in the background while we are working with it
                            if (AmountToHandle > 0)
                            {
                                //buying
                                if (_realTimeOrderBook.AskBook.Count > 0)
                                {
                                    var top = _realTimeOrderBook.AskBook.FirstOrDefault();
                                    newOrderPrice = top.Key;
                                    newOrderQty = top.Value;
                                }
                            }
                            else if (AmountToHandle < 0)
                            {
                                //selling
                                if (_realTimeOrderBook.BidBook.Count > 0)
                                {
                                    var top = _realTimeOrderBook.BidBook.FirstOrDefault();
                                    newOrderPrice = top.Key;
                                    newOrderQty = top.Value;
                                }
                            }
                        }
                        //continuing outside of the lock, we gathered the info we need, we unblock the updates
                        if (AmountToHandle > 0)
                        {
                            //buy
                            if (newOrderQty > AmountToHandle)
                            {
                                newOrderQty = AmountToHandle;
                            }
                            _client.NewOrder(Symbol, OrderType.Limit, Side.Buy,newOrderPrice, newOrderQty, TimeInForce.IOC);
                        }
                        else if (AmountToHandle < 0)
                        {
                            //sell
                            if (newOrderQty > -1 * AmountToHandle)
                            {
                                newOrderQty = -1 * AmountToHandle;
                            }
                            _client.NewOrder(Symbol, OrderType.Limit, Side.Sell, newOrderPrice, newOrderQty, TimeInForce.IOC);                            
                        }
                        //Now we just wait for the ACK and an order book update to restart cycle until we bring down the amountToHandle to 0
                    }
                }
            }            
        }
    }
}
