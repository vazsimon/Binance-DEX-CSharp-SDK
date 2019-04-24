using BinanceClient.Http.Get.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.ConversionHelpers
{
    /// <summary>
    /// Converter to decode the javascript integer time to c# DateTime
    /// </summary>
    public class KlinesConverter : JsonConverter
    {

        public KlinesConverter()
        {

        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            KlinesResponse kr = new KlinesResponse();
            reader.Read();
            if (!(reader.TokenType == JsonToken.EndArray)) 
            {
                kr.OpenTime = (long) reader.Value;
                reader.Read();
                kr.Open = Decimal.Parse((string)reader.Value);
                reader.Read();
                kr.High = Decimal.Parse((string)reader.Value);
                reader.Read();
                kr.Low = Decimal.Parse((string)reader.Value);
                reader.Read();
                kr.Close = Decimal.Parse((string)reader.Value);
                reader.Read();
                kr.Volume = Decimal.Parse((string)reader.Value);
                reader.Read();
                kr.CloseTime = (long)reader.Value;
                reader.Read();
                kr.QuoteAssetVolume = Decimal.Parse((string)reader.Value);
                reader.Read();
                kr.NumberOfTrades = (long)reader.Value;
                reader.Read();
            }
            return kr;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
