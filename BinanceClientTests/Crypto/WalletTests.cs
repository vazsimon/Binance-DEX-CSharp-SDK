using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinanceClient.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Crypto.Tests
{
    [TestClass()]
    public class WalletTests
    {        

        [TestMethod()]
        public void RestoreWalletFromMnemonicTest()
        {
            Wallet w = Wallet.RestoreWalletFromMnemonic("problem beach absurd giant grace process project bamboo diesel mass habit reward name maple album anger expire era universe frequent prosper weather pizza wine",
                Network.Test);

            Assert.AreEqual(w.PrivateKey, "1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035");
            Assert.AreEqual(w.Address, "tbnb1rv9zelawzxf7uz778cquvt35mqhrh7s2hcrahn");
        }

        [TestMethod()]
        public void WalletTest()
        {
            Wallet w = new Wallet("1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035", Network.Test);
            Assert.AreEqual(w.PrivateKey, "1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035");
            Assert.AreEqual(w.Address, "tbnb1rv9zelawzxf7uz778cquvt35mqhrh7s2hcrahn");
        }

        
        [TestMethod()]
        public void SignTest()
        {            
            Wallet w = new Wallet("1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035", Network.Test);
            string msgToSignHex = "9c01f0625dee0a28e774b32d0a14d8e1f06bf747f71907c130bf63f2b4a943b584b012074e4e422d4333461880c2d72f126c0a26eb5ae987210280ec8943329305e43b2e6112728423ef9f9a7e7125621c3545c2f30ce08bf83c12409ceabe0262a75b0da7556303580f56a094486cc9938a728f903a57054061bd833288979fbc8dc5ee07743df5110cb773c25d9974f34158a4f6ed6ac6899740c22009";byte[] bytesToSign = Helpers.StringToByteArrayFastest(msgToSignHex);
            string signedHexString = "DBBC0B77A204E6B1360061182F8170E48200AEF2DD091017EF1064A4C2AB2C04300B7B997705A30DE79B94D93048C7D0CC43809F7884971D1B6EAC7B50F2CFF2";
            var result = w.Sign(Helpers.StringToByteArrayFastest(msgToSignHex));
            var resultStr = BitConverter.ToString(result).Replace("-", "").ToUpper();
            Assert.AreEqual(resultStr, signedHexString);
        }

    }
}