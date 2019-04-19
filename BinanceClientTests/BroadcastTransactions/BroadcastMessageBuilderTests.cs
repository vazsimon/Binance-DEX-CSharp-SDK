using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinanceClient.BroadcastTransactions;
using BinanceClient.BroadcastTransactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinanceClient.Crypto;

namespace BinanceClient.BroadcastTransactions.Tests
{
    [TestClass()]
    public class BroadcastMessageBuilderTests
    {
        [TestMethod()]
        public void BuildTokenFreezeMessageTest()
        {
            Wallet w = new Wallet("1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035", Network.Test);
            w.SetSequence(0);//Have to reset, wallet automatically refreshes it's sequence on creation
            var msgBytes = BroadcastMessageBuilder.BuildTokenFreezeMessage("BNB", 1, w);
            var msgStr = BitConverter.ToString(msgBytes).Replace("-", "").ToUpper();
            var expectedStr = "9A01F0625DEE0A24E774B32D0A141B0A2CFFAE1193EE0BDE3E01C62E34D82E3BFA0A1203424E421880C2D72F126E0A26EB5AE9872103D2B6A5194D34703971E6C7544B2E5CE687AF7CF58346F662936D8E26C20A30131240A9A436A15F7239EB2A9265C84C88925561A9B44E54A159F121921D10853D4EFB52520FC963942F602C552AA87B6D4B401632E3C1305B3D993DAF926D6717BB1118ECE028";
            Assert.AreEqual(msgStr, expectedStr);
        }

        [TestMethod()]
        public void BuildTokenUnfreezeMessageTest()
        {
            Wallet w = new Wallet("1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035", Network.Test);
            w.SetSequence(0);//Have to reset, wallet automatically refreshes it's sequence on creation
            var msgBytes = BroadcastMessageBuilder.BuildTokenUnfreezeMessage("BNB", (decimal)0.1, w); 
            var msgStr = BitConverter.ToString(msgBytes).Replace("-", "").ToUpper();
            var expectedStr = "9A01F0625DEE0A246515FF0D0A141B0A2CFFAE1193EE0BDE3E01C62E34D82E3BFA0A1203424E421880ADE204126E0A26EB5AE9872103D2B6A5194D34703971E6C7544B2E5CE687AF7CF58346F662936D8E26C20A301312404F385A8F66A8E6656D4BF31034F107AE80720624EE46B9AAC0F50177AC8B5658149C9DE45872B2B008C81EEDC448DEA87A6BE351CADD77942E77F97E3FBD25A618ECE028";
            Assert.AreEqual(msgStr, expectedStr);
        }

        [TestMethod()]
        public void BuildVoteMessageTest()
        {
            Wallet w = new Wallet("1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035", Network.Test);
            w.SetSequence(0);//Have to reset, wallet automatically refreshes it's sequence on creation
            var msgBytes = BroadcastMessageBuilder.BuildVoteMessage(3, VoteOptions.OptionAbstain, w);
            var msgStr = BitConverter.ToString(msgBytes).Replace("-", "").ToUpper();
            var expectedStr = "9401F0625DEE0A1EA1CADD36080312141B0A2CFFAE1193EE0BDE3E01C62E34D82E3BFA0A1802126E0A26EB5AE9872103D2B6A5194D34703971E6C7544B2E5CE687AF7CF58346F662936D8E26C20A301312405C0AFBBFB819190D5A9794CE3F2BB2216BFCCEEB9B4EF378FE2B967B7775B4FF2795216ED6C7282D473E367F474A66EC0E13B4523BE06B4397778134AB8E158918ECE028";
            Assert.AreEqual(msgStr, expectedStr);
        }

        [TestMethod()]
        public void BuildCancelOrderMessageTest()
        {
            Wallet w = new Wallet("1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035", Network.Test);
            w.SetSequence(0);//Have to reset, wallet automatically refreshes it's sequence on creation
            var msgBytes = BroadcastMessageBuilder.BuildCancelOrderMessage("000-EF6_BNB", "34D5035164CA688605D4423668931B692BBD2654-28", w);
            var msgStr = BitConverter.ToString(msgBytes).Replace("-", "").ToUpper();
            var expectedStr = "CA01F0625DEE0A54166E681B0A141B0A2CFFAE1193EE0BDE3E01C62E34D82E3BFA0A120B3030302D4546365F424E421A2B333444353033353136344341363838363035443434323336363839333142363932424244323635342D3238126E0A26EB5AE9872103D2B6A5194D34703971E6C7544B2E5CE687AF7CF58346F662936D8E26C20A3013124089FE21047FDC597C7AE434A8DB91E38796C933D029FA2CD7BA3B3C8D519C6D472F3B84CAA4FC75863033D7EE843BA2E4C0D8E0465BDC3EC7834FE581A7F355AC18ECE028";
            Assert.AreEqual(msgStr, expectedStr);
        }

        [TestMethod()]
        public void BuildNewOrderMessageTest()
        {
            Wallet w = new Wallet("1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035", Network.Test);
            w.SetSequence(0);//Have to reset, wallet automatically refreshes it's sequence on creation
            var msgBytes = BroadcastMessageBuilder.BuildNewOrderMessage("000-EF6_BNB", OrderType.Limit, Side.Buy, 499, (decimal)0.00001, TimeInForce.GTE, w);
            var msgStr = BitConverter.ToString(msgBytes).Replace("-", "").ToUpper();
            var expectedStr = "D901F0625DEE0A63CE6DC0430A141B0A2CFFAE1193EE0BDE3E01C62E34D82E3BFA0A122A314230413243464641453131393345453042444533453031433632453334443832453342464130412D311A0B3030302D4546365F424E42200228013080A696F2B90138E8074001126E0A26EB5AE9872103D2B6A5194D34703971E6C7544B2E5CE687AF7CF58346F662936D8E26C20A301312405121493068E6C918E07CBD4275621472B4937A03E51BC15C8FB68170508AFA817F4391B31FE1AA81BE9B55C50DCC4606A08E9CCB7AE2FE9D87913285D3C84F6D18ECE028";
            Assert.AreEqual(msgStr, expectedStr);
        }

        [TestMethod()]
        public void BuildSendOneMessageTest()
        {
            Wallet w = new Wallet("1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035", Network.Test);
            w.SetSequence(0);//Have to reset, wallet automatically refreshes it's sequence on creation
            var msgBytes = BroadcastMessageBuilder.BuildSendOneMessage("tbnb1qqy83vaerqz7yv4yfqtz8pt0gl0wssxyl826h4", "BNB", 1, w, "Happy days");
            var msgStr = BitConverter.ToString(msgBytes).Replace("-", "").ToUpper();
            var expectedStr = "CE01F0625DEE0A4C2A2C87FA0A220A141B0A2CFFAE1193EE0BDE3E01C62E34D82E3BFA0A120A0A03424E421080C2D72F12220A14000878B3B91805E232A4481623856F47DEE840C4120A0A03424E421080C2D72F126E0A26EB5AE9872103D2B6A5194D34703971E6C7544B2E5CE687AF7CF58346F662936D8E26C20A301312407B3908BFDF3BA12E5CE1C1E96B97AE2D30650E742C41E901421A43DFC46FB1003A01E9BF95DAD3E4AAD00D235FAE11A4F198B4E5AD553FF4B80479A8BAEC4B7818ECE0281A0A48617070792064617973";
            Assert.AreEqual(msgStr, expectedStr);
        }

        [TestMethod()]
        public void BuildSendMultipleMessageTest()
        {
            Wallet w = new Wallet("1E5B4C6DDDB2BDD6B40344808812D6D3254D7D4E105A52BA51C032EB7BAC1035", Network.Test);
            w.SetSequence(0);//Have to reset, wallet automatically refreshes it's sequence on creation
            List<MultipleSendDestination> destinations = new List<MultipleSendDestination>();
            destinations.Add(new MultipleSendDestination { Address = "tbnb1qqy83vaerqz7yv4yfqtz8pt0gl0wssxyl826h4", Amount = (decimal)0.3, coin = "BNB" });
            destinations.Add(new MultipleSendDestination { Address = "tbnb17fq8s55rdshy04zjq0lfkexs4jeclmfnaaa2f2", Amount = (decimal)0.12, coin = "BNB" });
            var msgBytes = BroadcastMessageBuilder.BuildSendMultipleMessage(destinations, w, "Hello");
            var msgStr = BitConverter.ToString(msgBytes).Replace("-", "").ToUpper();
            var expectedStr = "ED01F0625DEE0A702A2C87FA0A220A141B0A2CFFAE1193EE0BDE3E01C62E34D82E3BFA0A120A0A03424E421080BD831412220A14000878B3B91805E232A4481623856F47DEE840C4120A0A03424E42108087A70E12220A14F2407852836C2E47D45203FE9B64D0ACB38FED33120A0A03424E421080B6DC05126E0A26EB5AE9872103D2B6A5194D34703971E6C7544B2E5CE687AF7CF58346F662936D8E26C20A301312406F4528E91293C969422CED4B8879AA54738B3A253E012DC5EC17AFEDA9FB09DA692DA436570A5B374994BF1B17F8FFB96D1EEAD3481EAF4DADAE6F7A246D66C718ECE0281A0548656C6C6F";
            Assert.AreEqual(msgStr, expectedStr);
        }
    }
}