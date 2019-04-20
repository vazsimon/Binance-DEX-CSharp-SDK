using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class AccountResponse
    {
        [JsonProperty(PropertyName = "account_number")]
        public long AccountNumber { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "balances")]
        public List<Balance> Balances { get; set; }
        [JsonProperty(PropertyName = "public_key")]
        public byte[] PublicKey { get; set; }
        [JsonProperty(PropertyName = "sequence")]
        public long Sequence { get; set; }

        public class Balance
        {
            [JsonProperty(PropertyName = "symbol")]
            public string Symbol { get; set; }
            [JsonProperty(PropertyName = "free")]
            public decimal Free { get; set; }
            [JsonProperty(PropertyName = "locked")]
            public decimal Locked { get; set; }
            [JsonProperty(PropertyName = "frozen")]
            public decimal Frozen { get; set; }
        }
    }

}
