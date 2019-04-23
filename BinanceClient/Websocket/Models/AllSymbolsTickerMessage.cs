using BinanceClient.ConversionHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket.Models
{
    public class AllSymbolsTickerMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<Ticker> Data { get; set; }
    }

    public class Ticker
    {
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "E")]
        public long EventTime { get; set; }
        public DateTime EventTimeDF { get { return JavaScriptDateConverter.Convert(EventTime);} }
        [JsonProperty(PropertyName = "s")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "p")]
        public decimal PriceChange { get; set; }
        [JsonProperty(PropertyName = "P")]
        public decimal PriceChangePercent { get; set; }
        [JsonProperty(PropertyName = "w")]
        public decimal WeightedAveragePrice { get; set; }
        [JsonProperty(PropertyName = "x")]
        public decimal PreviousDayClose { get; set; }
        [JsonProperty(PropertyName = "c")]
        public decimal Close { get; set; }
        [JsonProperty(PropertyName = "Q")]
        public decimal CloseTradeQuantity { get; set; }
        [JsonProperty(PropertyName = "b")]
        public decimal BestBidPrice { get; set; }
        [JsonProperty(PropertyName = "B")]
        public decimal BestBidQuantity { get; set; }
        [JsonProperty(PropertyName = "a")]
        public decimal BestAskPrice { get; set; }
        [JsonProperty(PropertyName = "A")]
        public decimal BestAskQuantity { get; set; }
        [JsonProperty(PropertyName = "o")]
        public decimal Open { get; set; }
        [JsonProperty(PropertyName = "h")]
        public decimal High { get; set; }
        [JsonProperty(PropertyName = "l")]
        public decimal Low { get; set; }
        [JsonProperty(PropertyName = "v")]
        public decimal Volume { get; set; }
        [JsonProperty(PropertyName = "q")]
        public decimal QuoteVolume { get; set; }
        [JsonProperty(PropertyName = "O")]
        public long StatisticsOpenTime { get; set; }
        [JsonProperty(PropertyName = "C")]
        public long StatisticsCloseTime { get; set; }
        [JsonProperty(PropertyName = "F")]
        public string FirstTradeId { get; set; }
        [JsonProperty(PropertyName = "L")]
        public string LastTradeId { get; set; }
        [JsonProperty(PropertyName = "n")]
        public long NumberOfTrades { get; set; }
    }
}
