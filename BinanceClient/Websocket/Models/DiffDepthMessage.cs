using BinanceClient.ConversionHelpers;
using BinanceClient.Websocket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket
{

    public class DiffDepthMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public OrderBookUpdate Data { get; set; }
    }

    public class OrderBookUpdate
    {
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "E")]
        public long EventTime { get; set; }
        public DateTime EventTimeDF { get { return JavaScriptDateConverter.ConvertFromSeconds(this.EventTime); } }
        [JsonProperty(PropertyName = "s")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "b")]
        [JsonConverter(typeof(OrderBookBidsConverter))]
        public SortedDictionary<decimal,decimal> Bids { get; set; }
        [JsonProperty(PropertyName = "a")]
        [JsonConverter(typeof(OrderBookAsksConverter))]
        public SortedDictionary<decimal, decimal> Asks { get; set; }
    }
    
}
