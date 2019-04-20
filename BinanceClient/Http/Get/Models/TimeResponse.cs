using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class TimeResponse
    {
        [JsonProperty(PropertyName = "ap_time")]
        public DateTime ApTtime { get; set; }
        [JsonProperty(PropertyName = "block_time")]
        public DateTime BlockTime { get; set; }
    }
}
