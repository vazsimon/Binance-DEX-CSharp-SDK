using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class FeesResponse
    {
        public string msg_type { get; set; }
        public decimal fee { get; set; }
        public int fee_for { get; set; }
        public decimal multi_transfer_fee { get; set; }
        public decimal lower_limit_as_multi { get; set; }
        public FixedFeeParams fixed_fee_params { get; set; }
        public List<DexFee> dex_fee_fields { get; set; }


        public class DexFee
        {
            public string fee_name { get; set; }
            public decimal fee_value { get; set; }
        }

        public class FixedFeeParams
        {
            public string msg_type { get; set; }
            public decimal fee { get; set; }
            public int fee_for { get; set; }
        }
    }
}
