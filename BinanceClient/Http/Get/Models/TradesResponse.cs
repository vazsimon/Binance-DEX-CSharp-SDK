using BinanceClient.ConversionHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class TradesResponse
    {
        [JsonProperty(PropertyName = "total")]
        public long Total { get; set; }
        [JsonProperty(PropertyName = "trade")]
        public List<Trade> Trade { get; set; }
    }

    public class Trade
    {
        [JsonProperty(PropertyName = "baseAsset")]
        public string BaseAsset { get; set; }
        [JsonProperty(PropertyName = "blockHeight")]
        public long BlockHeight { get; set; }
        [JsonProperty(PropertyName = "buyFee")]
        public string BuyFee { get; set; }
        [JsonProperty(PropertyName = "buyerId")]
        public string BuyerId { get; set; }
        [JsonProperty(PropertyName = "buyerOrderId")]
        public string BuyerOrderId { get; set; }
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
        [JsonProperty(PropertyName = "quantity")]
        public decimal Quantity { get; set; }
        [JsonProperty(PropertyName = "quoteAsset")]
        public string QuoteAsset { get; set; }
        [JsonProperty(PropertyName = "sellFee")]
        public string SellFee { get; set; }
        [JsonProperty(PropertyName = "sellerId")]
        public string SellerId { get; set; }
        [JsonProperty(PropertyName = "sellerOrderId")]
        public string SellerOrderId { get; set; }
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "time")]
        [JsonConverter(typeof(DateFieldConverter))]
        public DateTime Time { get; set; }
        [JsonProperty(PropertyName = "tradeId")]
        public string TradeId { get; set; }
    }
}
