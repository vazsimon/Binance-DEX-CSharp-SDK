using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Https.Get
{
    static class GetRequest
    {
        static Dictionary<string, List<DateTime>> LastCallTimes;

        static GetRequest()
        {
            LastCallTimes = new Dictionary<string, List<DateTime>>();
        }

        internal static string DownloadResult(string url, string urlPattern=null, int rateLimit = 0)
        {
            if (rateLimit > 0)
            {
                //Throttling goes here
            }
            using (WebClient wc = new WebClient())
            {
                return wc.DownloadString(url);
            }
        }
    }
}
