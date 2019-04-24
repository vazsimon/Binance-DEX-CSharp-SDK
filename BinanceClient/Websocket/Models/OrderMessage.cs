using BinanceClient.BroadcastTransactions.Models;
using BinanceClient.ConversionHelpers;
using BinanceClient.Enums;
using BinanceClient.Http.Get.Models;
using BinanceClient.Websocket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Websocket
{

    public class OrderMessage
    {
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<ExecutionReport> Data { get; set; }
    }

    public class ExecutionReport
    {
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "E")]
        public long EventHeight { get; set; }
        [JsonProperty(PropertyName = "s")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "S")]
        public OrderSide Side { get; set; }
        [JsonProperty(PropertyName = "o")]
        public OrderType OrderType { get; set; }
        [JsonProperty(PropertyName = "q")]
        public decimal Quantity { get; set; }
        [JsonProperty(PropertyName = "p")]
        public decimal Price { get; set; }
        [JsonProperty(PropertyName = "x")]
        public string CurrentExecutionType { get; set; }
        [JsonProperty(PropertyName = "X")]
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus OrderStatus { get; set; }
        [JsonProperty(PropertyName = "i")]
        public string OrderId { get; set; }
        [JsonProperty(PropertyName = "l")]
        public decimal LastExecutedQty { get; set; }
        [JsonProperty(PropertyName = "z")]
        public decimal CumulativeFillQuantity { get; set; }
        [JsonProperty(PropertyName = "L")]
        public decimal LastExecutedPrice { get; set; }
        [JsonProperty(PropertyName = "n")]
        public string Commission { get; set; }
        [JsonProperty(PropertyName = "T")]
        public long TransactionTime { get; set; }
        public DateTime TransactionTimeDF { get { return JavaScriptDateConverter.Convert(this.TransactionTime / 1000000); } }
        [JsonProperty(PropertyName = "t")]
        public string TradeId { get; set; }
        [JsonProperty(PropertyName = "O")]
        public long OrderCreationTime { get; set; }
        public DateTime OrderCreationTimeDF { get { return JavaScriptDateConverter.Convert(this.OrderCreationTime / 1000000); } }
    }   
    
}
