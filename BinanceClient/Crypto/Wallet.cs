using Bech32;
using BinanceClient.Crypto.Models;
using BinanceClient.Encode;
using BinanceClient.Http;
using BinanceClient.Http.Get;
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
   
        private void Init()
        {
            HTTPClient httpClient = new HTTPClient(Env.Network);
            try
            {
                var accountInfo = httpClient.GetAccount(Address);
                _accountNumber = accountInfo.AccountNumber;
                _sequence = accountInfo.Sequence;

                _chainId = _env.ChainId;
            }
            catch (BinanceHTTPApiRequestException ex)
            {
                if (ex.InnerException.Message.Contains("404"))
                {
                    // new empty wallet, we can still show the address. Transaction will not work, as the wallet is empty and the blockchain didn't give it a number yet.
                }
                else
                {
                    throw;
                }                
            }            
        }

   
        public void RefreshSequence()
        {
            HTTPClient httpClient = new HTTPClient(Env.Network);
            _sequence = httpClient.GetAccountSequence(Address);
        }

        public void IncrementSequence()
        {
            _sequence++;
        }

        public void SetSequence(long sequence)
        {
            _sequence = sequence;
        }

        public static CreateRandomAccountResponse CreateRandomAccount(Network network)
        {
            var env = BinanceEnvironment.GetEnvironment(network);
            var response = new CreateRandomAccountResponse();
            response.Network = network;
            Mnemonic mnemonic = new Mnemonic(Wordlist.English, WordCount.TwentyFour);
            response.Mnemonic = mnemonic.ToString();
            var seed = mnemonic.DeriveSeed();
            var pk = new ExtKey(seed);
            var kp = KeyPath.Parse(keyPath);
            var key = pk.Derive(kp);
            var privKey = key.PrivateKey;
            response.PrivateKey = BitConverter.ToString(privKey.ToBytes()).Replace("-", string.Empty);
            response.Address = Bech32Engine.Encode(env.Hrp, privKey.PubKey.Hash.ToBytes());

            return response;
        }


        public static Wallet RestoreWalletFromMnemonic(string mnemonicStr, Network env)
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

        public Wallet(string privateKey, Network env)
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

    }
}
