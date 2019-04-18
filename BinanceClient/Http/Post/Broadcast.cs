using BinanceClient.Http.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Post
{
    public static class Broadcast
    {
        public static string BroadcastToBlockchain(string url, byte[] message)
        {
            string messageStr = BitConverter.ToString(message).Replace("-", "").ToLower();
            using (WebClient wc = new WebClient())
            {
                
                wc.Headers["Content-Type"] = "text/plain";
                return wc.UploadString(url, "POST", messageStr);
            }
        }
    }
}
