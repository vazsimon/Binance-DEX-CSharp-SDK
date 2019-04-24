using BinanceClient.ConversionHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class DepthResponse
    {
        [JsonProperty(PropertyName = "asks")]
        [JsonConverter(typeof(OrderBookAsksConverter))]
        public SortedDictionary<decimal,decimal> Asks { get; set; }
        [JsonProperty(PropertyName = "bids")]
        [JsonConverter(typeof(OrderBookBidsConverter))]
        public SortedDictionary<decimal, decimal> Bids { get; set; }
        [JsonProperty(PropertyName = "height")]
        public long Height { get; set; }
    }


}
