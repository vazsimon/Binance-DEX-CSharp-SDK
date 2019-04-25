using BinanceClient.Http;
using BinanceClient.Websockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.ExchangeSpecificAlgos
{
    /// <summary>
    /// Class to connect to HTTP and Websockets clients to maintain multiple realtime orderbooks
    /// </summary>
    public class RealtimeOrderBookHandler
    {
        private HTTPClient _httpClient;
        private WebsocketClient _websocketClient;
        
        public Dictionary<string,RealTimeOrderbook> BooksForSymbols { get; set; }

        
        public RealtimeOrderBookHandler(HTTPClient httpClient, WebsocketClient websocketClient)
        {            
            _httpClient = httpClient;
            _websocketClient = websocketClient;
            BooksForSymbols = new Dictionary<string, RealTimeOrderbook>();
            _websocketClient.DiffDepth.OnDiffDepthReceived += DiffDepth_OnDiffDepthReceived;            
        }


        //single point of subscription, all depth updates are flowing through this event
        private void DiffDepth_OnDiffDepthReceived(object sender, Websocket.Models.Args.DiffDepthArgs e)
        {
            if (BooksForSymbols.ContainsKey(e.OrderBookUpdate.Symbol))
            {
                BooksForSymbols[e.OrderBookUpdate.Symbol].EnqueueUpdateAndProcessSafeAsync(e.OrderBookUpdate);
            }
        }


        public void Subscribe(string symbol)
        {
            bool createNew = true;
            if (BooksForSymbols.ContainsKey(symbol))
            {
                if (BooksForSymbols[symbol].OrderBookState != RealtimeOrderBookState.OutOfSync)
                {
                    //already have one operational
                    createNew = false;
                }
            }
            if (createNew)
            {
                BooksForSymbols[symbol] = new RealTimeOrderbook(symbol);
                _websocketClient.DiffDepth.Subscribe(symbol);
                var currentBook = _httpClient.GetDepth(symbol, Enums.QueryLimit.thousand);
                //Intentionally not awaited, no need for result here
                BooksForSymbols[symbol].InitAsync(currentBook.Bids, currentBook.Asks);
            }            
        }

        public void Unsubscribe(string symbol)
        {
            _websocketClient.DiffDepth.Unsubscribe(symbol);
            BooksForSymbols[symbol].SetOutOfSync();
        }

    }
}
