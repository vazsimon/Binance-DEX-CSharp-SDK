using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class AccountResponse
    {
        public long account_number { get; set; }
        public string address { get; set; }
        public List<Balance> balances { get; set; }
        public byte[] public_key { get; set; }
        public long sequence { get; set; }

        public class Balance
        {
            public string symbol { get; set; }
            public decimal free { get; set; }
            public decimal locked { get; set; }
            public decimal frozen { get; set; }
        }
    }

}
