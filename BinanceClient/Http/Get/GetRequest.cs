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
        private static object GetRequestLockObject;


        static GetRequest()
        {
            LastCallTimes = new Dictionary<string, List<DateTime>>();
            GetRequestLockObject = new object();
        }

        internal static string DownloadResult(string url, string urlPattern, int rateCheckIntervalSeconds, int rateLimit)
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
                        lock (GetRequestLockObject)
                        {
                            callList.RemoveAll(X => X < DateTime.Now.AddSeconds(-1  * rateCheckIntervalSeconds));
                            if (callList.Count < rateLimit)
                            {
                                clearedToGo = true;
                                callList.Add(DateTime.Now);
                            }
                        }
                        
                        if (!clearedToGo)
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
