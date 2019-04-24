using BinanceClient.ConversionHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    [JsonConverter(typeof(KlinesConverter))]
    public class KlinesResponse
    {
        public long OpenTime { get; set; }
        public DateTime OpenTimeDF { get { return JavaScriptDateConverter.Convert(OpenTime); } }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public long CloseTime { get; set; }
        public DateTime CloseTimeDF { get { return JavaScriptDateConverter.Convert(CloseTime); } }
        public decimal QuoteAssetVolume { get; set; }
        public long NumberOfTrades { get; set; }

    }
}
