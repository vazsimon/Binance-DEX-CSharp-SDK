using BinanceClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.ExchangeSpecificAlgos.OrderBookSkimmer
{
    public class TopOfTheBookSkimmerHandler
    {
        private Client _client;

        private Dictionary<string, TopOfTheBookSkimmer> Skimmers;
        
        public TopOfTheBookSkimmerHandler(Client client)
        {
            _client = client;
            Skimmers = new Dictionary<string, TopOfTheBookSkimmer>();
            _client.Websockets.Orders.OnOrdersReceived += Orders_OnOrdersReceived;
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
