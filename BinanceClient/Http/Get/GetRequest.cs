using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get
{
    static class GetRequest
    {
        static Dictionary<string, List<DateTime>> LastCallTimes;

        static GetRequest()
        {
            LastCallTimes = new Dictionary<string, List<DateTime>>();
        }

        internal static string DownloadResult(string url, string urlPattern="", int rateLimit = 0)
        {
            if (rateLimit > 0)
            {
                //Throttling goes here
                if (LastCallTimes.ContainsKey(urlPattern))
                {
                    var callList = LastCallTimes[urlPattern];
                    bool clearedToGo = false;
                    while (!clearedToGo)
                    {
                        callList.RemoveAll(X => X < DateTime.Now.AddSeconds(-1));
                        if (callList.Count < rateLimit)
                        {
                            clearedToGo = true;
                            callList.Add(DateTime.Now);
                        }
                        else
                        {
                            Thread.Sleep(50);
                        }
                    }
                }
                else
                {
                    LastCallTimes[urlPattern] = new List<DateTime>();
                    LastCallTimes[urlPattern].Add(DateTime.Now);
                }
            }
            using (WebClient wc = new WebClient())
            {
                return wc.DownloadString(url);
            }
        }
    }
}
