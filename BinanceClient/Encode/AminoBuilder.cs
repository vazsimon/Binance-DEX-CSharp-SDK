using BinanceClient.Crypto;
using BinanceClientConsole;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinanceClient.Crypto;
using static BinanceClientConsole.StdSignature.Types;

namespace BinanceClient.Encode
{
    public static class AminoBuilder
    {
        public enum AminoType
        {
            StandardTransaction,
            StandardSignature,
            PubKey,
            Send,
            NewOrder,
            Cancel,
            Freeze,
            Unfreeze,
            Vote
        }

        public static Dictionary<AminoType, byte[]> MessageCodes;

        static AminoBuilder()
        {
            MessageCodes = new Dictionary<AminoType, byte[]>();
            MessageCodes.Add(AminoType.StandardTransaction, BinanceClient.Crypto.Helpers.StringToByteArrayFastest("F0625DEE"));
            MessageCodes.Add(AminoType.Send, BinanceClient.Crypto.Helpers.StringToByteArrayFastest("2A2C87FA"));
            MessageCodes.Add(AminoType.NewOrder, BinanceClient.Crypto.Helpers.StringToByteArrayFastest("CE6DC043"));
            MessageCodes.Add(AminoType.Cancel, BinanceClient.Crypto.Helpers.StringToByteArrayFastest("166E681B"));
            MessageCodes.Add(AminoType.Freeze, BinanceClient.Crypto.Helpers.StringToByteArrayFastest("E774B32D"));
            MessageCodes.Add(AminoType.Unfreeze, BinanceClient.Crypto.Helpers.StringToByteArrayFastest("6515FF0D"));
            MessageCodes.Add(AminoType.Vote, BinanceClient.Crypto.Helpers.StringToByteArrayFastest("A1CADD36"));
            MessageCodes.Add(AminoType.PubKey, BinanceClient.Crypto.Helpers.StringToByteArrayFastest("EB5AE987"));
        }

        public static byte[] buildAminoMessage(byte[] message, AminoType messageType)
        {
            MemoryStream ms = new MemoryStream();
            if (MessageCodes.ContainsKey(messageType))
            {
                ms.Write(MessageCodes[messageType],0, MessageCodes[messageType].Length);
            }
            if (messageType == AminoType.PubKey)
            {
                //We only need to write the lenght into the message here                
                byte lengthBytes = (byte) message.Length; //This will always be 33 (address size in bytes), Cast is safe
                ms.WriteByte(lengthBytes);
            }
            ms.Write(message, 0, message.Length);
            return ms.ToArray();
        }

        public static byte[] buildAminoSignature(byte[] signature, Wallet wallet)
        {
            StdSignature sig = new StdSignature()
            {
                AccountNumber = wallet.AccountNumber,
                Sequence = wallet.Sequence,
                Signature = ByteString.CopyFrom(signature),
                PubKey = ByteString.CopyFrom(buildAminoMessage(wallet.PublicKey, AminoType.PubKey))
            };
            return sig.ToByteArray();
        }


        public static byte[] buildStandardTransaction(byte[] msg, byte[] stdSignature, string memo)
        {
            StdTx tx = new StdTx()
            {
                Memo = memo,
                Source = 0
            };
            tx.Msgs.Add(ByteString.CopyFrom(msg));
            tx.Signatures.Add(ByteString.CopyFrom(stdSignature));

            var txBytes = tx.ToByteArray();

            MemoryStream ms = new MemoryStream();
            var messageCode = MessageCodes[AminoType.StandardTransaction];
            var totalLength = (ulong)(txBytes.Length + messageCode.Length);
            
            //C# version of Protobuf doesn't support writing without tag with variant encoding, so we have to do the plumbing by hand. see function
            var lengthBytes = GetVarintBytes(totalLength); 

         
            ms.Write(lengthBytes, 0, lengthBytes.Length);
            ms.Write(messageCode, 0, messageCode.Length);
            ms.Write(txBytes, 0, txBytes.Length);

            return ms.ToArray();
        }

        private static byte[] GetVarintBytes(ulong value)
        {
            var buffer = new byte[10];
            var pos = 0;
            do
            {
                var byteVal = value & 0x7f;
                value >>= 7;

                if (value != 0)
                {
                    byteVal |= 0x80;
                }

                buffer[pos++] = (byte)byteVal;

            } while (value != 0);

            var result = new byte[pos];
            Buffer.BlockCopy(buffer, 0, result, 0, pos);

            return result;
        }
    }    
}
