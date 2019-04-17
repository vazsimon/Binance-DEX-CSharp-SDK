using BinanceClient.Crypto;
using BinanceClient.Https.Get.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Https.Get
{
    class HTTPSClient
    {
        BinanceEnvironment _env;
        public BinanceEnvironment Env { get { return _env; } }
        public bool RequestThrottling { get; set; }

        public HTTPSClient(BinanceEnvironment environment, bool requestThrottling = true)
        {
            _env = environment;
            RequestThrottling = requestThrottling;
        }

        public long GetAccountSequence(string account)
        {
            string urlPattern = "{0}/account/{1}/sequence";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, account);
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountSequenceResponse>(
                GetRequest.DownloadResult(url,urlPattern));
            return ret.sequence;
        }
        
    }
}
