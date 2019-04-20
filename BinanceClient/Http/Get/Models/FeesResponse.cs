using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class FeesResponse
    {
        [JsonProperty(PropertyName = "msg_type")]
        public string Msg_type { get; set; }
        [JsonProperty(PropertyName = "fee")]
        public decimal Fee { get; set; }
        [JsonProperty(PropertyName = "fee_for")]
        public int Fee_for { get; set; }
        [JsonProperty(PropertyName = "multi_transfer_fee")]
        public decimal Multi_transfer_fee { get; set; }
        [JsonProperty(PropertyName = "lower_limit_as_multi")]
        public decimal Lower_limit_as_multi { get; set; }
        [JsonProperty(PropertyName = "fixed_fee_params")]
        public FixedFeeParams Fixed_fee_params { get; set; }
        [JsonProperty(PropertyName = "dex_fee_fields")]
        public List<DexFee> Dex_fee_fields { get; set; }


        public class DexFee
        {
            [JsonProperty(PropertyName = "fee_name")]
            public string Fee_name { get; set; }
            [JsonProperty(PropertyName = "fee_value")]
            public decimal Fee_value { get; set; }
        }

        public class FixedFeeParams
        {
            [JsonProperty(PropertyName = "msg_type")]
            public string Msg_type { get; set; }
            [JsonProperty(PropertyName = "fee")]
            public decimal Fee { get; set; }
            [JsonProperty(PropertyName = "fee_for")]
            public int Fee_for { get; set; }
        }
    }
}
