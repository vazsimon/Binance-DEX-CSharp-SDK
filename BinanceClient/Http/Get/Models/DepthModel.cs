using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class DepthModel
    {
        public SortedDictionary<decimal,decimal> asks { get; set; }
        public SortedDictionary<decimal, decimal> bids { get; set; }
        public long height { get; set; }

        public static DepthModel FromJSONString(string json)
        {
            DepthModel dm = new DepthModel
            {
                asks = new SortedDictionary<decimal, decimal>(),
                bids = new SortedDictionary<decimal, decimal>()
            };
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            foreach (var entry in obj.asks)
            {
                dm.asks.Add(decimal.Parse(entry[0].Value), decimal.Parse(entry[1].Value));
            }
            foreach (var entry in obj.bids)
            {
                dm.bids.Add(decimal.Parse(entry[0].Value), decimal.Parse(entry[1].Value));
            }
            dm.height = obj.height;
            return dm;
        }
    }


}
