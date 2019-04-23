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
    public class OrderBookAsksConverter : JsonConverter
    {

        public OrderBookAsksConverter()
        {

        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SortedDictionary<decimal,decimal>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            SortedDictionary<decimal, decimal> sd = new SortedDictionary<decimal, decimal>();
            reader.Read();
            while (!(reader.TokenType == JsonToken.EndArray)) //ENd of whole book
            {
                reader.Read();//get past starting of array
                var level = Decimal.Parse((string)reader.Value);
                reader.Read();
                var qty = Decimal.Parse((string)reader.Value);
                sd.Add(level, qty);
                reader.Read();//EndArray for book level
                reader.Read();//startArray for next or endArray for book end
            }
            return sd;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
