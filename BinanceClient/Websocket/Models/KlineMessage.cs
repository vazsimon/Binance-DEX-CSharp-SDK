using BinanceClient.Http.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket.Models
{
    public class KlineMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public KlineWrap Data { get; set; }
    }

    public class KlineWrap
    {
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "E")]
        public long EventTime { get; set; }
        public DateTime EventTimeDF { get { return JavaScriptDateConverter.Convert(EventTime); } }
        [JsonProperty(PropertyName = "s")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "k")]
        public Kline Kline { get; set; }
    }

    public class Kline
    {
        [JsonProperty(PropertyName = "t")]
        public long OpenTime { get; set; }
        public DateTime OpenTimeDF { get { return JavaScriptDateConverter.Convert(OpenTime); } }
        [JsonProperty(PropertyName = "T")]
        public long CloseTime { get; set; }
        public DateTime CloseTimeDF { get { return JavaScriptDateConverter.Convert(CloseTime); } }
        [JsonProperty(PropertyName = "s")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "i")]
        public string Interval { get; set; }
        [JsonProperty(PropertyName = "f")]
        public string FirstTradeId { get; set; }
        [JsonProperty(PropertyName = "L")]
        public string LastTradeId { get; set; }
        [JsonProperty(PropertyName = "o")]
        public decimal OpenPrice { get; set; }
        [JsonProperty(PropertyName = "c")]
        public decimal ClosePrice { get; set; }
        [JsonProperty(PropertyName = "h")]
        public decimal HighPrice { get; set; }
        [JsonProperty(PropertyName = "l")]
        public decimal LowPrice { get; set; }
        [JsonProperty(PropertyName = "v")]
        public decimal Volume { get; set; }
        [JsonProperty(PropertyName = "n")]
        public long NumberOfTrades { get; set; }
        [JsonProperty(PropertyName = "x")]
        public bool IsClosed { get; set; }
        [JsonProperty(PropertyName = "q")]
        public decimal QuoteAssetVolume { get; set; }

    }
}
