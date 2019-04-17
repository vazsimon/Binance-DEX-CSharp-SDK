using Bech32;
using BinanceClient.Crypto;
using NBitcoin;
using NBitcoin.Crypto;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient
{
    public class PrototypeItems
    {
        public Key key;
        public PrototypeItems()
        {
            key = new Key();
        }

        private const string keyPath = "44'/714'/0'/0/0";



        public static string GetPrivateKeyFromMnemonic()
        {
            //var privKey = key.ToString(Network.Main);
            //return privKey;

            //Mnemonic mnemonic = new Mnemonic("gift supreme quality enact air cannon course foil december dirt tent human area bundle decline actor flame panther banana this owner sudden sword repeat");
            Mnemonic mnemonic = new Mnemonic("protect fat transfer enforce siren miss twenty pistol flee hard adapt demand marine convince member advance silk pave august decorate bonus spider clarify dose");
            ////var seed = mnemonic.DeriveSeed();
            //var ek = mnemonic.DeriveExtKey();

            //var pk = new ExtKey().Derive(KeyPath.Parse(keyPath));
            //var network = Network.GetNetwork("BTC");

            var seed = mnemonic.DeriveSeed();
            var pk = new ExtKey(seed);
            var kp = KeyPath.Parse(keyPath);
            var key = pk.Derive(kp);

            var privKey = key.PrivateKey;
            var bytes = privKey.ToBytes();
            var s1 = BitConverter.ToString(bytes).Replace("-", string.Empty);
            
            //-----------------------------------------Mnemonic end--------------------------------------
            
            
            //var privateKey = "30c5e838578a29e3e9273edddd753d6c9b38aca2446dd84bdfe2e5988b0da0a1";   Javascript test
            var privateKey = "30c5e838578a29e3e9273edddd753d6c9b38aca2446dd84bdfe2e5988b0da0a1";   //Second addr
            //var pk2 = "3dcc267e1f7edca86e03f0963b2d0b7804552d3014caddcfc435a4d7bc240cf5";
            //string testMessage = "testmessage";
            string msg = "7b226163636f756e745f6e756d626572223a2231222c22636861696e5f6964223a22626e62636861696e2d31303030222c226d656d6f223a22222c226d736773223a5b7b226964223a22423635363144434331303431333030353941374330384634384336343631304331463646393036342d3130222c226f7264657274797065223a322c227072696365223a3130303030303030302c227175616e74697479223a313230303030303030302c2273656e646572223a22626e63316b6574706d6e71736779637174786e7570723667636572707073306b6c797279687a36667a6c222c2273696465223a312c2273796d626f6c223a224254432d3543345f424e42222c2274696d65696e666f726365223a317d5d2c2273657175656e6365223a2239227d";
            string sig = "9c0421217ef92d556a14e3f442b07c85f6fc706dfcd8a72d6b58f05f96e95aa226b10f7cf62ccf7c9d5d953fa2c9ae80a1eacaf0c779d0253f1a34afd17eef34";

            var barray = Helpers.StringToByteArrayFastest(privateKey);
            var back = BitConverter.ToString(barray).Replace("-","").ToLower();
            bool wentOk = privateKey == back;


            var pkTest = new Key(Helpers.StringToByteArrayFastest(privateKey));
            var complressed = pkTest.PubKey.Compress();
            var hash = complressed.Hash.ToBytes();
            var addrTest = Bech32Engine.Encode("tbnb", hash);

            var msgBytes = Helpers.StringToByteArrayFastest(msg);
            var msgHashed = Hashes.Hash256(msgBytes);
            var sigNew = pkTest.SignCompact(msgHashed);            
            
            //string pubKey = BitConverter.ToString(key2.PubKey.ToBytes());


            //string pubKey1 = GetPublicKeyFromPrivateKeyEx(privateKey);



            return "";
        }


        
    }
}
