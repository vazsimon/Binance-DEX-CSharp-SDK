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
        public string name { get; set; }
        /// <summary>
        /// unique token trade symbol
        /// </summary>
        public string symbol { get; set; }
        /// <summary>
        /// token symbol
        /// </summary>
        public string original_symbol { get; set; }
        /// <summary>
        /// total token supply in decimal form, e.g. 1.00000000
        /// </summary>
        public decimal total_supply { get; set; }
        /// <summary>
        /// Address which issue the token
        /// </summary>
        public string owner { get; set; }
    }
}
