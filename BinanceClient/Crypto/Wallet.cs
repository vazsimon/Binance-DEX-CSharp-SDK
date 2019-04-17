using Bech32;
using BinanceClient.Encode;
using NBitcoin;
using NBitcoin.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Crypto
{
    public class Wallet
    {
        private byte[] _publicKeyBytes;
        public byte[] PublicKey { get { return _publicKeyBytes; } }

        private string _privateKeyStr;
        public string PrivateKey { get { return _privateKeyStr; } }
        private string _addressStr;
        public string Address { get { return _addressStr; }}

        private byte[] _AddressBytes;
        public byte[] AddressBytes { get { return _AddressBytes; } }

        private Key _privateKey;
        private PubKey _publicKey;

        private Int64 _accountNumber = -1;
        public Int64 AccountNumber { get { return _accountNumber; }}

        private Int64 _sequence = -1;
        public Int64 Sequence { get { return _sequence; } }

        private string _chainId;
        public string ChainId { get { return _chainId; } }

        private const string keyPath = "44'/714'/0'/0/0";

        BinanceEnvironment _env;
        public BinanceEnvironment Env { get { return _env; } }

        public string Hrp { get {return _env.Hrp; } }

        public bool VerifySequenceBeforeSend { get; set; }

        private void Init()
        {
            VerifySequenceBeforeSend = true;
            _accountNumber = 667614;
            _sequence = 3;
            _chainId = _env.ChainId;            
        }


        public static Wallet RestoreWalletFromMnemonic(string mnemonicStr, Environment env)
        {
            
            Mnemonic mnemonic = new Mnemonic(mnemonicStr);

            var seed = mnemonic.DeriveSeed();
            var pk = new ExtKey(seed);
            var kp = KeyPath.Parse(keyPath);
            var key = pk.Derive(kp);

            var privKey = key.PrivateKey;
            var bytes = privKey.ToBytes(); 

            Wallet w = new Wallet();
            w._env = BinanceEnvironment.GetEnvironment(env);
            w._privateKey = privKey;
            w._privateKeyStr = BitConverter.ToString(bytes).Replace("-", string.Empty);
            w._publicKey = w._privateKey.PubKey.Compress();
            w._publicKeyBytes = w._publicKey.ToBytes();
            w._AddressBytes = w._publicKey.Hash.ToBytes();
            w._addressStr = Bech32Engine.Encode(w.Env.Hrp, w._AddressBytes);


            w.Init();

            return w;
        }

        public Wallet(string privateKey, Environment env)
        {
            _env = BinanceEnvironment.GetEnvironment(env);
            _privateKey = new Key(Helpers.StringToByteArrayFastest(privateKey));
            _privateKeyStr = privateKey;
            _publicKey = _privateKey.PubKey.Compress();
            _publicKeyBytes = _publicKey.ToBytes();
            _AddressBytes = _publicKey.Hash.ToBytes();
            _addressStr = Bech32Engine.Encode(_env.Hrp, _AddressBytes);

            Init();
        }

        private Wallet()
        {
            //Empty wallet only allowed to be created in this original class to ensure proper initialization of wallets at all time
        }

        public byte[] Sign(byte[] message)
        {
            var msgHashed = Hashes.SHA256(message);
            uint256 hashUi = new uint256(msgHashed);
            ECDSASignature signature = _privateKey.Sign(hashUi, false);


            byte[] result = new byte[64];
            signature.R.ToByteArrayUnsigned().CopyTo(result, 0);
            signature.S.ToByteArrayUnsigned().CopyTo(result, 32);
            return result;
        }

        public string SignMessage(byte[] message)
        {
            return _privateKey.SignMessage(message);
        }
    }
}
