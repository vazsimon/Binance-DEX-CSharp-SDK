using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class TimeResponse
    {
        public DateTime ap_time { get; set; }
        public DateTime block_time { get; set; }
    }
}
