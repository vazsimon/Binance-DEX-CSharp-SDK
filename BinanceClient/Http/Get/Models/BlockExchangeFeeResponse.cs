using BinanceClient.Http.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class BlockExchangeFeeResponse
    {
        [JsonProperty(PropertyName = "total")]
        public long Total { get; set; }
        [JsonProperty(PropertyName = "blockExchangeFee")]
        public List<BlockExchangeFee> BlockExchangeFee { get; set; }
    }

    public class BlockExchangeFee
    {
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "blockHeight")]
        public long BlockHeight { get; set; }
        [JsonProperty(PropertyName = "blockTime")]
        [JsonConverter(typeof(DateFieldConverter))]
        public DateTime BlockTime { get; set; }
        [JsonProperty(PropertyName = "fee")]
        public string Fee { get; set; }
        [JsonProperty(PropertyName = "tradeCount")]
        public long TradeCount { get; set; }
    }
}
