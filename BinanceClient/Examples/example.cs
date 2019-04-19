using BinanceClient.BroadcastTransactions;
using BinanceClient.BroadcastTransactions.Models;
using BinanceClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Examples
{
    public static class Examples
    {
        public static void RunAllBroadcastExamples(string privateKey)
        {            
            Client client = new Client(privateKey, Crypto.Network.Test);
            
            //Freezing 1 BNB on the account. in the resp, we have the information that we received on submitting the broadcast message. We are using the default option
            //to wait for the transaction confirmation SequenceEnsureMode.VerifyBeforeSendAndWaitForConfirmation, so the response is totally accurate
            var resp = client.FreezeToken("BNB", 1);
            
            //now unfreezing
            client.UnfreezeToken("BNB", 1);

            //send me some BNB
            client.Send("tbnb17fq8s55rdshy04zjq0lfkexs4jeclmfnaaa2f2", "BNB", (decimal)0.00001, "message for transfer");

            //send all kinds of coins
            List<MultipleSendDestination> destinations = new List<MultipleSendDestination>();
            destinations.Add(new MultipleSendDestination { Address = "tbnb1qqy83vaerqz7yv4yfqtz8pt0gl0wssxyl826h4", Amount = (decimal)0.3, coin = "BNB" });
            destinations.Add(new MultipleSendDestination { Address = "tbnb17fq8s55rdshy04zjq0lfkexs4jeclmfnaaa2f2", Amount = (decimal)0.12, coin = "BNB" });
            client.MultiSend(destinations,"All kinds");

            //send buy order for coin
             client.NewOrder("000-EF6_BNB", OrderType.Limit, Side.Buy, 499, (decimal)0.00001, TimeInForce.GTE);

            //Canceling order. Please modify the ID and instrument
            string orderIdToCancel = "34D5035164CA688605D4423668931B692BBD2654-28";
            string orderSymbolToCancel = "000-EF6_BNB";
            client.CancelOrder(orderSymbolToCancel, orderIdToCancel);

            //Vote for something. please modify id
            client.Vote(3, VoteOptions.OptionAbstain);
        }
    }
}
