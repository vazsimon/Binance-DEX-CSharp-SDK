using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class AccountSequenceResponse
    {
        [JsonProperty(PropertyName = "sequence")]
        public long Sequence { get; set; }
    }
}
