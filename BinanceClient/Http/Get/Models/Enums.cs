using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Http.Get.Models
{
   public enum QueryLimit
    {
        five = 5,
        ten = 10,
        twenty = 20,
        fifty = 50,
        hundred = 100,
        fiveHundred = 500,
        thousand = 1000,
        All = 0
    }
}
