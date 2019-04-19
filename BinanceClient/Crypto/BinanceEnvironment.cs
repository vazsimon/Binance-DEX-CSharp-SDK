using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Crypto
{
    public class BinanceEnvironment
    {
        private Network _environmentType;
        public Network EnvironmentType { get; set; }
        private string _hrp;
        public string Hrp { get { return _hrp; } }
        private string _chainId;
        public string ChainId { get { return _chainId; } }
        private string _httpsApiAddress;
        public string HttpsApiAddress { get { return _httpsApiAddress; } }

        private string _wssApiAddress;
        public string WssApiAddress { get { return _wssApiAddress; } }

        public static BinanceEnvironment GetEnvironment(Network env)
        {
            BinanceEnvironment be = new BinanceEnvironment();
            be.EnvironmentType = env;
            if (env == Network.Test)
            {
                be._hrp = "tbnb";
                be._chainId = "Binance-Chain-Nile";
                be._httpsApiAddress = "https://testnet-dex.binance.org/api/v1";
                be._wssApiAddress = "wss://testnet-dex.binance.org/api/";
            }
            else if (env == Network.Production)
            {
                be._hrp = "bnb";
                be._chainId = "";
                be._httpsApiAddress = "";
                be._wssApiAddress = "";
            }
            else
            {
                throw new Exception("Unknown environment");
            }
            return be;
        }

    }

    public enum Network
    {
        Test,
        Production,
        ProtocolTest
    }
}
