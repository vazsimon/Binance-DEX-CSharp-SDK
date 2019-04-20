using BinanceClient.Http.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class TransactionsResponse
    {
        [JsonProperty(PropertyName = "tx")]
        public List<Transaction> Tx { get; set; }
        [JsonProperty(PropertyName = "total")]
        public long Total { get; set; }
    }
    
    public class Transaction
    {
        [JsonProperty(PropertyName = "txHash")]
        public string TxHash { get; set; }
        [JsonProperty(PropertyName = "blockHeight")]
        public long BlockHeight { get; set; }
        [JsonProperty(PropertyName = "txType")]
        [JsonConverter(typeof(TxTypeConverter))]
        public TxType TxType { get; set; }
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty(PropertyName = "fromAddr")]
        public string FromAddr { get; set; }
        [JsonProperty(PropertyName = "toAddr")]
        public string ToAddr { get; set; }
        [JsonProperty(PropertyName = "value")]
        public decimal Value { get; set; }
        [JsonProperty(PropertyName = "txAsset")]
        public string TxAsset { get; set; }
        [JsonProperty(PropertyName = "txFee")]
        public string TxFee { get; set; }
        [JsonProperty(PropertyName = "txAge")]
        public long TxAge { get; set; }
        [JsonProperty(PropertyName = "orderId")]
        public string OrderId { get; set; }
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }
    }
}
