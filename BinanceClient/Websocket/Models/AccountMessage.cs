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

    public class AccountMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public AccountInfo Data { get; set; }
    }

    public class AccountInfo
    {
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "E")]
        public long EventHeight { get; set; }       
        [JsonProperty(PropertyName = "B")]
        public List<AccountBalance> Balances { get; set; }
    }

    public class AccountBalance
    {
        [JsonProperty(PropertyName = "a")]
        public string Coin { get; set; }
        [JsonProperty(PropertyName = "f")]
        public decimal Free { get; set; }
        [JsonProperty(PropertyName = "l")]
        public decimal Locked { get; set; }
        [JsonProperty(PropertyName = "r")]
        public decimal Frozen { get; set; }
    }
    
}
