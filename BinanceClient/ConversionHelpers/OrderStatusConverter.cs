using BinanceClient.Enums;
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
    /// Converter to decode the order status directly to c# enum
    /// </summary>
    public class OrderStatusConverter : JsonConverter
    {

        public OrderStatusConverter()
        {

        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var m = Enum.Parse(typeof(OrderStatus), (string)reader.Value);
            return m;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((OrderStatus)value).ToString() );
        }
    }
}
