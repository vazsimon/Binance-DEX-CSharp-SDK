using BinanceClient.Crypto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Encode
{
    /// <summary>
    /// Converter to decode binary addresses to the signing JSON, so that we can use protobuf messages directly when building signing JSON
    /// </summary>
    public class ByteStringConverter : JsonConverter
    {
        private Wallet wallet;

        public ByteStringConverter(Wallet w)
        {
            this.wallet = w;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Google.Protobuf.ByteString);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var m = Convert.FromBase64String((string)reader.Value);
            return m;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            byte[] addrBytes = ((Google.Protobuf.ByteString)value).ToByteArray();

            string addrDec = Bech32.Bech32Engine.Encode(wallet.Hrp, addrBytes);

            writer.WriteValue(addrDec);
        }
    }
}
