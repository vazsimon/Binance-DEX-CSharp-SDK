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
        public string base_asset_symbol { get; set; }
        /// <summary>
        /// symbol of quote asset
        /// </summary>
        public string quote_asset_symbol { get; set; }
        /// <summary>
        /// In decimal form, e.g. 1.00000000
        /// </summary>
        public decimal list_price { get; set; }
        /// <summary>
        /// Minimium price change in decimal form, e.g. 1.00000000
        /// </summary>
        public string tick_size { get; set; }
        /// <summary>
        /// Minimium trading quantity in decimal form, e.g. 1.00000000
        /// </summary>
        public decimal lot_size { get; set; }
    }
}
