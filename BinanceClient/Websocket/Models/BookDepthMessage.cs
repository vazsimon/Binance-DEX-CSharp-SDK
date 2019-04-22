using BinanceClient.Http.Helpers;
using BinanceClient.Websocket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket
{

    public class BookDepthMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public OrderBookSnapshot Data { get; set; }
    }

    public class OrderBookSnapshot
    {
        [JsonProperty(PropertyName = "lastUpdateId")]
        public long LastUpdateId { get; set; }
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "bids")]
        [JsonConverter(typeof(OrderBookBidsConverter))]
        public SortedDictionary<decimal,decimal> Bids { get; set; }
        [JsonProperty(PropertyName = "asks")]
        [JsonConverter(typeof(OrderBookAsksConverter))]
        public SortedDictionary<decimal, decimal> Asks { get; set; }
    }
    
}
