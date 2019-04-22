using BinanceClient.Websocket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket
{

    public class IndividualSymbolMiniTickerMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public MiniTicker Data { get; set; }
    }
    
}
