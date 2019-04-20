using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class MarketsResponse
    {
        /// <summary>
        /// symbol of base asset
        /// </summary>
        [JsonProperty(PropertyName = "base_asset_symbol")]
        public string Base_asset_symbol { get; set; }
        /// <summary>
        /// symbol of quote asset
        /// </summary>
        [JsonProperty(PropertyName = "quote_asset_symbol")]
        public string Quote_asset_symbol { get; set; }
        /// <summary>
        /// In decimal form, e.g. 1.00000000
        /// </summary>
        [JsonProperty(PropertyName = "list_price")]
        public decimal List_price { get; set; }
        /// <summary>
        /// Minimium price change in decimal form, e.g. 1.00000000
        /// </summary>
        [JsonProperty(PropertyName = "tick_size")]
        public string Tick_size { get; set; }
        /// <summary>
        /// Minimium trading quantity in decimal form, e.g. 1.00000000
        /// </summary>
        [JsonProperty(PropertyName = "lot_size")]
        public decimal Lot_size { get; set; }
    }
}
