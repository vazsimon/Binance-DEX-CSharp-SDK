using BinanceClient.ConversionHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class TickerResponse
    {
        [JsonProperty(PropertyName = "askPrice")]
        public decimal AskPrice { get; set; }
        [JsonProperty(PropertyName = "asqQuantity")]
        public decimal AsqQuantity { get; set; }
        [JsonProperty(PropertyName = "bidPrice")]
        public decimal BidPrice { get; set; }
        [JsonProperty(PropertyName = "bidQuantity")]
        public decimal BidQuantity { get; set; }
        [JsonProperty(PropertyName = "closeTime")]
        public long CloseTime { get; set; }
        public DateTime CloseTimeDF { get { return JavaScriptDateConverter.Convert(CloseTime); } }
        [JsonProperty(PropertyName = "count")]
        public long Count { get; set; }
        [JsonProperty(PropertyName = "firstId")]
        public string FirstId { get; set; }
        [JsonProperty(PropertyName = "highPrice")]
        public decimal HighPrice { get; set; }
        [JsonProperty(PropertyName = "lastId")]
        public string LastId { get; set; }
        [JsonProperty(PropertyName = "lastPrice")]
        public decimal LastPrice { get; set; }
        [JsonProperty(PropertyName = "lastQuantity")]
        public decimal LastQuantity { get; set; }
        [JsonProperty(PropertyName = "lowPrice")]
        public decimal LowPrice { get; set; }
        [JsonProperty(PropertyName = "openPrice")]
        public decimal OpenPrice { get; set; }
        [JsonProperty(PropertyName = "openTime")]
        public long OpenTime { get; set; }
        public DateTime OpenTimeDF { get { return JavaScriptDateConverter.Convert(OpenTime); } }
        [JsonProperty(PropertyName = "prevClosePrice")]
        public decimal PrevClosePrice { get; set; }
        [JsonProperty(PropertyName = "priceChangePercent")]
        public decimal PriceChangePercent { get; set; }
        [JsonProperty(PropertyName = "quoteVolume")]
        public decimal QuoteVolume { get; set; }
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "volume")]
        public decimal Volume { get; set; }
        [JsonProperty(PropertyName = "weightedAvgPrice")]
        public decimal WeightedAvgPrice { get; set; }
    }
}
