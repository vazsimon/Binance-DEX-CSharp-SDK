using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class TokensResponse
    {
        /// <summary>
        /// token name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// unique token trade symbol
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        /// <summary>
        /// token symbol
        /// </summary>
        [JsonProperty(PropertyName = "original_symbol")]
        public string Original_symbol { get; set; }
        /// <summary>
        /// total token supply in decimal form, e.g. 1.00000000
        /// </summary>
        [JsonProperty(PropertyName = "total_supply")]
        public decimal Total_supply { get; set; }
        /// <summary>
        /// Address which issue the token
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }
    }
}
