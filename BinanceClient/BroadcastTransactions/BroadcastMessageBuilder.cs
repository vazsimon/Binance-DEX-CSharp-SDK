using BinanceClient.BroadcastTransactions.Models;
using BinanceClient.Crypto;
using BinanceClient.Encode;
using BinanceClientConsole;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.BroadcastTransactions
{
    public static class BroadcastMessageBuilder
    {
        public static byte[] BuildTokenFreezeMessage(string coin, decimal amount, Wallet wallet)
        {
            string hrpFrom;
            byte[] addrDec;
            Bech32.Bech32Engine.Decode(wallet.Address, out hrpFrom, out addrDec);
            TokenFreeze tf = new TokenFreeze
            {
                Amount = (long) Decimal.Round(amount * 100000000),
                Symbol = coin,
                From = ByteString.CopyFrom(addrDec)
            };
            var aminoMessage = AminoBuilder.buildAminoMessage(tf.ToByteArray(), AminoBuilder.AminoType.Freeze);
            StdSignBytesConverter signatureBytesConverter = new StdSignBytesConverter(tf, wallet);
            byte[] messageBytesForSign = signatureBytesConverter.GetCanonicalBytesForSignature();
            var signatureBytes = wallet.Sign(messageBytesForSign);
            var signatureBytesMessage = AminoBuilder.buildAminoSignature(signatureBytes, wallet);
            var stdMsg = AminoBuilder.buildStandardTransaction(aminoMessage, signatureBytesMessage, "");

            return stdMsg;
        }

        public static byte[] BuildTokenUnfreezeMessage(string coin, decimal amount, Wallet wallet)
        {
            string hrpFrom;
            byte[] addrDec;
            Bech32.Bech32Engine.Decode(wallet.Address, out hrpFrom, out addrDec);
            TokenUnfreeze tu = new TokenUnfreeze
            {
                Amount = (long)Decimal.Round(amount * 100000000),
                Symbol = coin,
                From = ByteString.CopyFrom(addrDec)
            };
            var aminoMessage = AminoBuilder.buildAminoMessage(tu.ToByteArray(), AminoBuilder.AminoType.Unfreeze);
            StdSignBytesConverter signatureBytesConverter = new StdSignBytesConverter(tu, wallet);
            byte[] messageBytesForSign = signatureBytesConverter.GetCanonicalBytesForSignature();
            var signatureBytes = wallet.Sign(messageBytesForSign);
            var signatureBytesMessage = AminoBuilder.buildAminoSignature(signatureBytes, wallet);
            var stdMsg = AminoBuilder.buildStandardTransaction(aminoMessage, signatureBytesMessage, "");

            return stdMsg;
        }


        public static byte[] BuildVoteMessage(int proposal_id, VoteOptions option, Wallet wallet)
        {
            string hrpFrom;
            byte[] addrDec;
            Bech32.Bech32Engine.Decode(wallet.Address, out hrpFrom, out addrDec);

            Vote vt = new Vote
            {
                Voter = ByteString.CopyFrom(addrDec),
                Option = (long) option,
                ProposalId = proposal_id
            };
            var aminoMessage = AminoBuilder.buildAminoMessage(vt.ToByteArray(), AminoBuilder.AminoType.Vote);
            StdSignBytesConverter signatureBytesConverter = new StdSignBytesConverter(vt, wallet);
            byte[] messageBytesForSign = signatureBytesConverter.GetCanonicalBytesForSignature();
            var signatureBytes = wallet.Sign(messageBytesForSign);
            var signatureBytesMessage = AminoBuilder.buildAminoSignature(signatureBytes, wallet);
            var stdMsg = AminoBuilder.buildStandardTransaction(aminoMessage, signatureBytesMessage, "");

            return stdMsg;
        }

        public static byte[] BuildCancelOrderMessage(string symbol, string refId, Wallet wallet)
        {
            string hrpFrom;
            byte[] addrDec;
            Bech32.Bech32Engine.Decode(wallet.Address, out hrpFrom, out addrDec);
            CancelOrder co = new CancelOrder
            {
                Refid = refId,
                Symbol = symbol,
                Sender = ByteString.CopyFrom(addrDec)
            };
            var aminoMessage = AminoBuilder.buildAminoMessage(co.ToByteArray(), AminoBuilder.AminoType.Cancel);
            StdSignBytesConverter signatureBytesConverter = new StdSignBytesConverter(co, wallet);
            byte[] messageBytesForSign = signatureBytesConverter.GetCanonicalBytesForSignature();
            var signatureBytes = wallet.Sign(messageBytesForSign);
            var signatureBytesMessage = AminoBuilder.buildAminoSignature(signatureBytes, wallet);
            var stdMsg = AminoBuilder.buildStandardTransaction(aminoMessage, signatureBytesMessage, "");

            return stdMsg;
        }

        public static byte[] BuildNewOrderMessage(string symbol, OrderType orderType, Side side, decimal price, decimal qty, TimeInForce tif, Wallet wallet)
        {
            string hrpFrom;
            byte[] addrDec;
            Bech32.Bech32Engine.Decode(wallet.Address, out hrpFrom, out addrDec);
            NewOrder no = new NewOrder
            {
                Sender = ByteString.CopyFrom(addrDec),
                Id = string.Format("{0}-{1}", BitConverter.ToString(addrDec).Replace("-", "").ToUpper(), wallet.Sequence + 1),
                Ordertype = (long)orderType,
                Side = (long)side,
                Price = (long)Decimal.Round(price * 100000000),
                Quantity = (long)Decimal.Round(qty * 100000000),
                Timeinforce = (long)tif,
                Symbol = symbol
            };
            var aminoMessage = AminoBuilder.buildAminoMessage(no.ToByteArray(), AminoBuilder.AminoType.NewOrder);
            StdSignBytesConverter signatureBytesConverter = new StdSignBytesConverter(no, wallet);
            byte[] messageBytesForSign = signatureBytesConverter.GetCanonicalBytesForSignature();
            var signatureBytes = wallet.Sign(messageBytesForSign);
            var signatureBytesMessage = AminoBuilder.buildAminoSignature(signatureBytes, wallet);
            var stdMsg = AminoBuilder.buildStandardTransaction(aminoMessage, signatureBytesMessage, "");

            return stdMsg;
        }


        public static byte[] BuildSendOneMessage(string toAddress, string coin, decimal amount, Wallet wallet, string memo="")
        {
            string hrpFrom;
            byte[] addrFromDec;
            Bech32.Bech32Engine.Decode(wallet.Address, out hrpFrom, out addrFromDec);
            string hrpTo;
            byte[] addrToDec;
            Bech32.Bech32Engine.Decode(toAddress, out hrpTo, out addrToDec);
            if (hrpFrom != hrpTo)
            {
                throw new BlockchainNetworkMismatchException(string.Format("Environment mismatch between from address ({0}) and to address ({1})", hrpFrom, hrpTo));
            }

            Send send = new Send();
            var input = new Send.Types.Input();
            input.Address = ByteString.CopyFrom(addrFromDec);
            var coinToSend = new Send.Types.Token { Amount = (long)Decimal.Round(amount * 100000000), Denom = coin };
            input.Coins.Add(coinToSend);
            var output = new Send.Types.Output();
            output.Address = ByteString.CopyFrom(addrToDec);
            output.Coins.Add(coinToSend);
            send.Inputs.Add(input);
            send.Outputs.Add(output);

            var aminoMessage = AminoBuilder.buildAminoMessage(send.ToByteArray(), AminoBuilder.AminoType.Send);
            StdSignBytesConverter signatureBytesConverter = new StdSignBytesConverter(send, wallet,memo);
            byte[] messageBytesForSign = signatureBytesConverter.GetCanonicalBytesForSignature();
            var signatureBytes = wallet.Sign(messageBytesForSign);
            var signatureBytesMessage = AminoBuilder.buildAminoSignature(signatureBytes, wallet);
            var stdMsg = AminoBuilder.buildStandardTransaction(aminoMessage, signatureBytesMessage, memo);

            return stdMsg;
        }

        public static byte[] BuildSendMultipleMessage(List<MultipleSendDestination> destinations, Wallet wallet, string memo="")
        {
            string hrpFrom;
            byte[] addrFromDec;
            Bech32.Bech32Engine.Decode(wallet.Address, out hrpFrom, out addrFromDec);
          
            Dictionary<string, decimal> coinInputs = new Dictionary<string, decimal>();
            foreach (var destination in destinations)
            {
                if (coinInputs.ContainsKey(destination.coin))
                {
                    coinInputs[destination.coin] += destination.Amount;
                }
                else
                {
                    coinInputs[destination.coin] = destination.Amount;
                }
            }
            Send send = new Send();
            var input = new Send.Types.Input();
            input.Address = ByteString.CopyFrom(addrFromDec);
            foreach (var coin in coinInputs)
            {
                var coinToSend = new Send.Types.Token { Amount = (long)Decimal.Round(coin.Value * 100000000), Denom = coin.Key };
                input.Coins.Add(coinToSend);
            }
            send.Inputs.Add(input);
            var outAddresses = destinations.Select(X => X.Address).ToList().Distinct();
            foreach (var outAddress in outAddresses)
            {
                var output = new Send.Types.Output();
                byte[] outAddrDec;
                string hrpTo;
                Bech32.Bech32Engine.Decode(outAddress, out hrpTo, out outAddrDec);
                if (hrpFrom != hrpTo)
                {
                    throw new BlockchainNetworkMismatchException(string.Format("Environment mismatch between from address ({0}) and to address ({1})", hrpFrom, hrpTo));
                }
                output.Address = ByteString.CopyFrom(outAddrDec);
                var outCoins = destinations.Where(X => X.Address == outAddress);
                foreach (var coin in outCoins)
                {
                    var coinToSend = new Send.Types.Token { Amount = (long)Decimal.Round(coin.Amount * 100000000), Denom = coin.coin };
                    output.Coins.Add(coinToSend);
                }
                send.Outputs.Add(output);
            }

            var aminoMessage = AminoBuilder.buildAminoMessage(send.ToByteArray(), AminoBuilder.AminoType.Send);
            StdSignBytesConverter signatureBytesConverter = new StdSignBytesConverter(send, wallet,memo);
            byte[] messageBytesForSign = signatureBytesConverter.GetCanonicalBytesForSignature();
            var signatureBytes = wallet.Sign(messageBytesForSign);
            var signatureBytesMessage = AminoBuilder.buildAminoSignature(signatureBytes, wallet);
            var stdMsg = AminoBuilder.buildStandardTransaction(aminoMessage, signatureBytesMessage, memo);

            return stdMsg;
        }
    }
}
