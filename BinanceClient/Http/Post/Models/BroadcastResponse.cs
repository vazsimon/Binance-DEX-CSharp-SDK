using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Post.Models
{
    public class BroadcastResponse
    {
        public string hash { get; set; }
        public string log { get; set; }
        public bool ok { get; set; }
    }
}
