using BinanceClient.ConversionHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket.Models
{
    public class TradesMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<Trade> Data { get; set; }
    }
    
    public class Trade
    {
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "E")]
        public long EventHeight { get; set; }
        [JsonProperty(PropertyName = "t")]
        public string TradeId { get; set; }
        [JsonProperty(PropertyName = "p")]
        public decimal Price { get; set; }
        [JsonProperty(PropertyName = "q")]
        public decimal Quantity { get; set; }
        [JsonProperty(PropertyName = "b")]
        public string BuyerOrderId { get; set; }
        [JsonProperty(PropertyName = "a")]
        public string SellerOrderId { get; set; }
        [JsonProperty(PropertyName = "T")]
        public long TradeTime { get; set; }
        public DateTime TradeTimeDF { get { return JavaScriptDateConverter.ConvertFromSeconds(this.TradeTime / 1000000000); } }
        [JsonProperty(PropertyName = "sa")]
        public string SellerAddress { get; set; }
        [JsonProperty(PropertyName = "ba")]
        public string BuyerAddress { get; set; }

    }
}
