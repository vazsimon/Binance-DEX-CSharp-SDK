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
        public string id { get; set; }
        /// <summary>
        /// Original listen address before PeersService changed it
        /// </summary>
        public string original_listen_addr { get; set; }
        /// <summary>
        /// Listen address
        /// </summary>
        public string listen_addr { get; set; }
        /// <summary>
        /// Access address (HTTP)
        /// </summary>
        public string access_addr { get; set; }
        /// <summary>
        /// Stream address (WS)
        /// </summary>
        public string stream_addr { get; set; }
        /// <summary>
        /// Chain ID
        /// </summary>
        public string network { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// Moniker (Name)
        /// </summary>
        public string moniker { get; set; }
        /// <summary>
        /// Array of capability tags: node, qs, ap, ws
        /// </summary>
        public List<string> capabilities { get; set; }
        /// <summary>
        /// Is an accelerated path to a validator node	
        /// </summary>
        public bool accelerated { get; set; }
    }    
}
