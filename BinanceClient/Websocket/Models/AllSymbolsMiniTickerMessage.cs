using BinanceClient.Http.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket.Models
{
    public class AllSymbolsMiniTickerMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<MiniTicker> Data { get; set; }
    }

    public class MiniTicker
    {
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "E")]
        public long EventTime { get; set; }
        public DateTime EventTimeDF { get { return JavaScriptDateConverter.Convert(EventTime);} }
        [JsonProperty(PropertyName = "s")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "c")]
        public decimal Close { get; set; }
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
    }
}
