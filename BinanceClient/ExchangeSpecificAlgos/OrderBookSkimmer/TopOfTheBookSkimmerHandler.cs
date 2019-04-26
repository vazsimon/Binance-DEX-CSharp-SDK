using BinanceClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.ExchangeSpecificAlgos.OrderBookSkimmer
{
    /// <summary>
    /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!WARNING!!!!!!!!!!!!!!!!!!!!!!
    /// Performance optimized class, this implementation doesn't make any additional checks that could take away time from sending out the orders and getting the fills.
    /// Not suitable for all use-cases at this form.
    /// For proper operation, you have to ensure the following pre-conditions
    ///     - You don't have any open orders on the opposite side of accumulation/distribution
    ///     - you have enough account balance of the required coins to fully complete the accumulation/distribution
    ///     - you have to ensure no other app is using the private key during accumulation/distribution
    ///     - once accumulation/distribution is complete, if you need higher resiliency in the Binance client, please update the client's sequenceEnsureMode accordingly. 
    ///       This algo sets it to Manual to increase the chances of getting the orders in the nearest block.
    /// </summary>
    public class TopOfTheBookSkimmerHandler
    {
        private Client _client;

        private Dictionary<string, TopOfTheBookSkimmer> Skimmers;
        
        public TopOfTheBookSkimmerHandler(Client client)
        {
            _client = client;
            Skimmers = new Dictionary<string, TopOfTheBookSkimmer>();
            _client.Websockets.Orders.OnOrdersReceived += Orders_OnOrdersReceived;
            _client.Websockets.Orders.Subscribe(_client.Wallet.Address);
            //------------------------------------------------TURNING OFF all Sequence number ensuring for speed (except for the auto-increment), we are using websocket streams for info------------------
            //-------------------------------------------------ALGO IS NOT SAFE AT EVERY USE CASE, be careful on:---------------------------------
            //-------------------------------------------------Failed transactions, other processes using same private key
            _client.sequenceEnsureMode = SequenceEnsureMode.Manual;
        }


        //Single point of subscription, all order data is flowing in here
        private void Orders_OnOrdersReceived(object sender, Websocket.Models.OrdersArg e)
        {
            foreach (var update in e.OrderUpdates)
            {
                if (Skimmers.ContainsKey(update.Symbol))
                {
                    Skimmers[update.Symbol].ProcessOrderAsync(update);
                }
            }            
        }

        public void Accumulate(string symbol, decimal amount)
        {
            if (!Skimmers.ContainsKey(symbol))
            {
                Skimmers[symbol] = new TopOfTheBookSkimmer(symbol, _client);
            }    
            Skimmers[symbol].Accumulate(amount);
        }

        public void Distribute(string symbol, decimal amount)
        {
            if (!Skimmers.ContainsKey(symbol))
            {
                Skimmers[symbol] = new TopOfTheBookSkimmer(symbol, _client);
            }
            Skimmers[symbol].Distribute(amount);
        }
    }
}
