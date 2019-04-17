using BinanceClient.Encode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BinanceClient.Crypto
{
    public class StdSignBytesConverter
    {
        public string account_number { get; set; }
        public string chain_id { get; set; }
        public byte[] data { get; set; } 
        public string memo { get; set; }
        public object[] msgs { get; set; }
        public string sequence { get; set; }
        public string source { get; set; }

        private Wallet _wallet;

  
        public StdSignBytesConverter(object msg, Wallet wallet)
        {
            msgs = new object[] { msg };
            data = null;
            memo = string.Empty;
            source = "0";

            account_number = wallet.AccountNumber.ToString();
            chain_id = wallet.ChainId;
            sequence = wallet.Sequence.ToString();
            _wallet = wallet;
        }

        public byte[] GetCanonicalBytesForSignature()
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(this, new JsonConverter[] { new ByteStringConverter(_wallet),});
            var jObj = (JObject)JsonConvert.DeserializeObject(jsonString);
            Sort(jObj);
            string newJson = jObj.ToString(Formatting.None);

            return Encoding.ASCII.GetBytes(newJson);  
        }

        private void Sort(JObject jObj)
        {
            var props = jObj.Properties().ToList();
            foreach (var prop in props)
            {
                prop.Remove();
            }

            foreach (var prop in props.OrderBy(p => p.Name))
            {
                var newProperty = new JProperty(prop.Name.ToLower(), prop.Value);
                jObj.Add(newProperty);
                if (newProperty.Value is JObject)
                {
                    Sort((JObject)newProperty.Value);
                }
                else if (newProperty.Value is JArray)
                {
                    foreach (var item in newProperty.Value.Values<JObject>())
                    {
                        Sort(item);
                    }
                }
            }
        }
    }
}
