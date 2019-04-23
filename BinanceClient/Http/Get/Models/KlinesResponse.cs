using BinanceClient.ConversionHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class KlinesResponse
    {
        public long OpenTimeJSFormat { get; set; }
        public DateTime OpenTime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public long CloseTimeJSFormat { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal QuoteAssetVolume { get; set; }
        public long NumberOfTrades { get; set; }

        public static List<KlinesResponse> FromJSONString(string json)
        {
            List<KlinesResponse> ret = new List<KlinesResponse>();
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            foreach (var klineRaw in obj)
            {
                KlinesResponse kline = new KlinesResponse
                {
                    OpenTimeJSFormat = klineRaw[0],
                    OpenTime = JavaScriptDateConverter.Convert(klineRaw[0].Value),
                    Open = klineRaw[1],
                    High = klineRaw[2],
                    Low = klineRaw[3],
                    Close = klineRaw[4],
                    Volume = klineRaw[5],
                    CloseTimeJSFormat = klineRaw[6],
                    CloseTime = JavaScriptDateConverter.Convert(klineRaw[6].Value),
                    QuoteAssetVolume = klineRaw[7],
                    NumberOfTrades = klineRaw[8]
                };
                ret.Add(kline);
            }
            return ret;
        }
    }
}
