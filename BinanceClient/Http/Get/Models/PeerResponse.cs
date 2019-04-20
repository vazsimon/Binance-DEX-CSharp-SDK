using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
    public class PeerResponse
    {
        /// <summary>
        /// Authenticated identifier
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        /// <summary>
        /// Original listen address before PeersService changed it
        /// </summary>
        [JsonProperty(PropertyName = "original_listen_addr")]
        public string OriginalListenAddr { get; set; }
        /// <summary>
        /// Listen address
        /// </summary>
        [JsonProperty(PropertyName = "listen_addr")]
        public string listenAddr { get; set; }
        /// <summary>
        /// Access address (HTTP)
        /// </summary>
        [JsonProperty(PropertyName = "access_addr")]
        public string AccessAddr { get; set; }
        /// <summary>
        /// Stream address (WS)
        /// </summary>
        [JsonProperty(PropertyName = "stream_addr")]
        public string StreamAddr { get; set; }
        /// <summary>
        /// Chain ID
        /// </summary>
        [JsonProperty(PropertyName = "network")]
        public string Network { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }
        /// <summary>
        /// Moniker (Name)
        /// </summary>
        [JsonProperty(PropertyName = "moniker")]
        public string Moniker { get; set; }
        /// <summary>
        /// Array of capability tags: node, qs, ap, ws
        /// </summary>
        [JsonProperty(PropertyName = "capabilities")]
        public List<string> Capabilities { get; set; }
        /// <summary>
        /// Is an accelerated path to a validator node	
        /// </summary>
        [JsonProperty(PropertyName = "accelerated")]
        public bool Accelerated { get; set; }
    }    
}
