using BinanceClient;
using BinanceClient.BroadcastTransactions;
using BinanceClient.Crypto;
using BinanceClient.Enums;
using BinanceClient.ExchangeSpecificAlgos;
using BinanceClient.ExchangeSpecificAlgos.OrderBookSkimmer;
using BinanceClient.NodeRPC;
using BinanceClient.Websocket.Models;
using BinanceClient.Websocket.Models.Args;
using Newtonsoft.Json;
using System;

namespace DemoConsole
{
    class Program
    {
        static string privateKey = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Please paste private key here for TESTNET (with funds to use)");
            privateKey = Console.ReadLine();
            string choice = "";
            while (choice != "q")
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Please choose:");
                Console.WriteLine("1 - Wallet");
                Console.WriteLine("2 - HTTP");
                Console.WriteLine("3 - Websocket");
                Console.WriteLine("4 - Transaction Broadcast");
                Console.WriteLine("5 - Transaction broadcast through Node RPC websockets - streamline sending 20 transactions");
                Console.WriteLine("6 - Realtime orderbook");
                Console.WriteLine("7 - Orderbook skimmer");
                Console.WriteLine("q - Exit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Wallet();
                        break;
                    case "2":
                        Http();
                        break;
                    case "3":
                        Websocket();
                        break;
                    case "4":
                        Broadcast();
                        break;
                    case "5":
                        BroadcastRPC();
                        break;
                    case "6":
                        RealtimeOrderBook();
                        break;
                    case "7":
                        Skimmer();
                        break;
                    default:
                        break;
                }
            }

        }

        private static void Skimmer()
        {
            Client client = new Client(privateKey, Network.Test);
            TopOfTheBookSkimmerHandler handler = new TopOfTheBookSkimmerHandler(client);
            handler.Accumulate("000-EF6_BNB", (decimal)0.001);
            Console.WriteLine("Skimmer started check transactions");
        }

        static RealtimeOrderBookHandler realtimebooks;

        private static void RealtimeOrderBook()
        {
            Client client = new Client(privateKey, Network.Test);
            realtimebooks = new RealtimeOrderBookHandler(client.HTTP, client.Websockets);
            realtimebooks.Subscribe("000-EF6_BNB");
            realtimebooks.BooksForSymbols["000-EF6_BNB"].OnOrderbookUpdated += Program_OnOrderbookUpdated;
            Console.WriteLine("Orderbook set up successfully");
        }

        private static void Program_OnOrderbookUpdated(object sender, OrderBookUpdatedArgs e)
        {
            Console.WriteLine("Orderbook update");
            Console.WriteLine(e.UpdateTime);
        }

        private static void BroadcastRPC()
        {
            Client client = new Client(privateKey, Network.Test);
            NodeRPCClient clientRPC = new NodeRPCClient("wss://seed-pre-s3.binance.org/websocket");
            //Streamline send 20 times
            for (int i = 0; i < 20; i++)
            {
                var msg = BroadcastMessageBuilder.BuildTokenFreezeMessage("BNB", (decimal)0.001, client.Wallet);
                var resp = clientRPC.BroadcastTransaction(msg, RPCBroadcastMode.async);
                client.Wallet.IncrementSequence();
                cwf(resp);
            }
        }

        private static void Broadcast()
        {
            Client client = new Client(privateKey, Network.Test);
            var resp = client.FreezeToken("BNB", (decimal)0.01);
            cwf(resp);
        }

        private static void Websocket()
        {
            Client client = new Client(privateKey, Network.Test);
            client.Websockets.BlockHeight.OnBlockHeightReceived += BlockHeight_OnBlockHeightReceived;
            client.Websockets.BlockHeight.Subscribe();

            client.Websockets.IndividualMiniTicker.OnMiniTickerIndividualReceived += IndividualMiniTicker_OnMiniTickerIndividualReceived;
            client.Websockets.IndividualMiniTicker.Subscribe("000-EF6_BNB");
            client.Websockets.IndividualMiniTicker.Subscribe("IBB-8DE_BNB");
        }

        private static void IndividualMiniTicker_OnMiniTickerIndividualReceived(object sender, MiniTickerIndividualArgs e)
        {
            Console.WriteLine("MiniTicker");
            cwf(e.MiniTicker);
        }

        private static void BlockHeight_OnBlockHeightReceived(object sender, BlockHeightArgs e)
        {
            Console.WriteLine("Blockheight");
            Console.WriteLine(e.BlockHeight);
            Console.WriteLine("----------");
        }

        private static void Wallet()
        {
            var newKeys = BinanceClient.Crypto.Wallet.CreateRandomAccount(Network.Test);
            Console.WriteLine("Random wallet created");
            Console.WriteLine(JsonConvert.SerializeObject(newKeys, Formatting.Indented));
            Console.WriteLine("Restoring wallet from mnemonic");
            var w = BinanceClient.Crypto.Wallet.RestoreWalletFromMnemonic(newKeys.Mnemonic, Network.Test);
            Console.WriteLine("Restored private key");
            Console.WriteLine(w.PrivateKey);
        }

        private static void Http()
        {
            Client client = new Client(privateKey, Network.Test);
            var accR = client.HTTP.GetAccount("tbnb1xn2sx5tyef5gvpw5ggmx3ycmdy4m6fj57zanqd");
            cwf(accR);
            Console.WriteLine("GetAccount");
            Console.WriteLine("Press enter");
            Console.ReadLine();

            var accSR = client.HTTP.GetAccountSequence("tbnb1xn2sx5tyef5gvpw5ggmx3ycmdy4m6fj57zanqd");
            cwf(accSR);
            Console.WriteLine("GetAccountSequence");
            Console.WriteLine("Press enter");
            Console.ReadLine();

            var r1 = client.HTTP.GetBlockExchangeFee("tbnb1xn2sx5tyef5gvpw5ggmx3ycmdy4m6fj57zanqd");
            cwf(r1);
            Console.WriteLine("GetBlockExchangeFee");
            Console.WriteLine("Press enter");
            Console.ReadLine();

            var r2 = client.HTTP.GetDepth("000-EF6_BNB");
            cwf(r2);
            Console.WriteLine("GetDepth");
            Console.WriteLine("Press enter");
            Console.ReadLine();

            var r3 = client.HTTP.GetKlines("000-EF6_BNB", KlineInterval.h1);
            cwf(r3);
            Console.WriteLine("GetKlines");
            Console.WriteLine("Press enter");
            Console.ReadLine();
        }

        private static void cwf(object acc)
        {
            Console.WriteLine(JsonConvert.SerializeObject(acc, Formatting.Indented));
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
