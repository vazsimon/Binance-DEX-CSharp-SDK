using BinanceClient.Crypto;
using BinanceClient.Http.Get;
using BinanceClient.Http.Get.Models;
using BinanceClient.Http.Post;
using BinanceClient.Http.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvironmentType = BinanceClient.Crypto.EnvironmentType;

namespace BinanceClient.Http
{
    public class HTTPClient
    {
        BinanceEnvironment _env;
        public BinanceEnvironment Env { get { return _env; } }
        public bool RequestThrottling { get; set; }

        public HTTPClient(EnvironmentType environment, bool requestThrottling = true)
        {
            _env = BinanceEnvironment.GetEnvironment(environment);
            RequestThrottling = requestThrottling;
        }


        /// <summary>
        /// Gets an account sequence for an address.
        /// </summary>
        /// <param name="account">The account address to query</param>
        /// <returns></returns>
        public long GetAccountSequence(string account)
        {
            //Call specific settings
            string urlPattern = "{0}/account/{1}/sequence";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, account);
            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountSequenceResponse>(response);
            return ret.sequence;
        }


        /// <summary>
        /// Gets account metadata for an address.
        /// </summary>
        /// <param name="account">The account address to query</param>
        /// <returns></returns>
        public AccountResponse GetAccount(string account)
        {
            //Call specific settings
            string urlPattern = "{0}/account/{1}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, account);
            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountResponse>(response);
            return ret;
        }


        public BroadcastResponse BroadcastToBlockchain(byte[] message, bool waitForConfirmation = true)
        {
            string url = string.Format("{0}/broadcast?sync={1}", _env.HttpsApiAddress, waitForConfirmation);
            try
            {                
                var response = Broadcast.BroadcastToBlockchain(url, message);
                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BroadcastResponse>>(response);
                return ret.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new BinanceHTTPApiRequestException(string.Format("An error occured during an API POST broadcast to ", url), ex);
            }            
        }


















        /// <summary>
        /// Method to retrieve HTTP get response for an api call with path level throttling for API calls
        /// </summary>
        /// <param name="url">The exact path to be used in the call</param>
        /// <param name="urlPattern">the pattern that serves as key for the throttling mechanism</param>
        /// <param name="callsPerSecondAllowed">The number of calls that can be sent out in a second without throttling (0 = no throttling)</param>
        /// <returns></returns>
        private string GetStringResponse(string url, string urlPattern, int callsPerSecondAllowed)
        {
            try
            {
                if (!RequestThrottling)
                {
                    callsPerSecondAllowed = 0;
                }
                return GetRequest.DownloadResult(url, urlPattern, callsPerSecondAllowed);
            }
            catch (Exception ex)
            {
                throw new BinanceHTTPApiRequestException(string.Format("An error occured during an API a GET call to {0}", url), ex);
            }
        }
    }
}
