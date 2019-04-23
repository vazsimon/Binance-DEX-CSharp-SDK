using BinanceClient.BroadcastTransactions.Models;
using BinanceClient.ConversionHelpers;
using BinanceClient.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class OrdersResponse
    {
        [JsonProperty(PropertyName = "order")]
        public List<Order> Order { get; set; }
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }

    public class Order
    {
        [JsonProperty(PropertyName = "cumulateQuantity")]
        public decimal CumulateQuantity { get; set; }
        [JsonProperty(PropertyName = "fee")]
        public string Fee { get; set; }
        [JsonProperty(PropertyName = "lastExecutedPrice")]
        public decimal LastExecutedPrice { get; set; }
        [JsonProperty(PropertyName = "lastExecutedQuantity")]
        public decimal LastExecutedQuantity { get; set; }
        [JsonProperty(PropertyName = "orderCreateTime")]
        public DateTime OrderCreateTime { get; set; }
        [JsonProperty(PropertyName = "orderId")]
        public string OrderId { get; set; }
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }
        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }
        [JsonProperty(PropertyName = "quantity")]
        public decimal Quantity { get; set; }
        [JsonProperty(PropertyName = "side")]
        public OrderSide Side { get; set; }
        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "timeInForce")]
        public TimeInForce  TimeInForce { get; set; }
        [JsonProperty(PropertyName = "tradeId")]
        public string TradeId { get; set; }
        [JsonProperty(PropertyName = "transactionHash")]
        public string TransactionHash { get; set; }
        [JsonProperty(PropertyName = "transactionTime")]
        public DateTime TransactionTime { get; set; }
        [JsonProperty(PropertyName = "type")]
        public OrderType Type { get; set; }
    }
}
