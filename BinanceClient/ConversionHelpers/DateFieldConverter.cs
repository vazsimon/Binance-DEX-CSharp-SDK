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
    public class DateFieldConverter : JsonConverter
    {

        public DateFieldConverter()
        {

        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var m = JavaScriptDateConverter.Convert((long)reader.Value);
            return m;
        }

        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((long)value).ToString());
        }
    }
}
