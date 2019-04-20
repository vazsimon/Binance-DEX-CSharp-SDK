using BinanceClient.Http.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class DepthResponse
    {
        public SortedDictionary<decimal,decimal> Asks { get; set; }
        public SortedDictionary<decimal, decimal> Bids { get; set; }
        public long Height { get; set; }

        public static DepthResponse FromJSONString(string json)
        {
            DepthResponse dr = new DepthResponse
            {
                Asks = new SortedDictionary<decimal, decimal>(),
                Bids = new SortedDictionary<decimal, decimal>(new ReverseComparer<decimal>(Comparer<decimal>.Default))
            };
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            foreach (var entry in obj.asks)
            {
                dr.Asks.Add(decimal.Parse(entry[0].Value), decimal.Parse(entry[1].Value));
            }
            foreach (var entry in obj.bids)
            {
                dr.Bids.Add(decimal.Parse(entry[0].Value), decimal.Parse(entry[1].Value));
            }
            dr.Height = obj.height;
            return dr;
        }
    }


}
