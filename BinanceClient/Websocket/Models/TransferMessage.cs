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

    public class TransferMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public TransferInfo Data { get; set; }
    }

    public class TransferInfo
    {
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "E")]
        public long EventHeight { get; set; }        
        [JsonProperty(PropertyName = "H")]
        public string TransactionHash { get; set; }
        [JsonProperty(PropertyName = "f")]
        public string FromAddr { get; set; }
        [JsonProperty(PropertyName = "t")]
        public List<TransferDestination> Destinations { get; set; }
    }

    public class TransferDestination
    {
        [JsonProperty(PropertyName = "o")]
        public string ToAddress { get; set; }
        [JsonProperty(PropertyName = "c")]
        public List<TransferCoin> Coins { get; set; }
    }

    public class TransferCoin
    {
        [JsonProperty(PropertyName = "a")]
        public string Coin { get; set; }
        [JsonProperty(PropertyName = "A")]
        public decimal Amount { get; set; }
    }
    
}
