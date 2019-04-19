using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Crypto.Models
{
    public class CreateRandomAccountResponse
    {
        public string Mnemonic { get; set; }
        public string PrivateKey { get; set; }
        public string Address { get; set; }
        public Network Network { get; set; }
    }
}
