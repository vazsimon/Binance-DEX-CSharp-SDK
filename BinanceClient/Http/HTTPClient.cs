using BinanceClient.Crypto;
using BinanceClient.Enums;
using BinanceClient.Http.Get;
using BinanceClient.Http.Get.Models;
using BinanceClient.Http.Post;
using BinanceClient.Http.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network = BinanceClient.Crypto.Network;

namespace BinanceClient.Http
{
    public class HTTPClient
    {
        BinanceEnvironment _env;
        public BinanceEnvironment Env { get { return _env; } }
        public bool RequestThrottling { get; set; }

        public HTTPClient(Network environment, bool requestThrottling = true)
        {
            _env = BinanceEnvironment.GetEnvironment(environment);
            RequestThrottling = requestThrottling;
        }


        /// <summary>
        /// Gets an account sequence for an address.
        /// </summary>
        /// <param name="account">The account address to query</param>
        /// <returns></returns>
        public long GetAccountSequence(string account)
        {
            //Call specific settings
            string urlPattern = "{0}/account/{1}/sequence";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, account);
            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountSequenceResponse>(response);
            return ret.Sequence;
        }

        public async Task<long> GetAccountSequenceAsync(string account)
        {
            var br = await Task<long>.Run(() =>
            {
                return GetAccountSequence(account);
            });
            return br;
        }



        /// <summary>
        /// Gets account metadata for an address.
        /// </summary>
        /// <param name="account">The account address to query</param>
        /// <returns></returns>
        public AccountResponse GetAccount(string account)
        {
            //Call specific settings
            string urlPattern = "{0}/account/{1}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, account);
            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountResponse>(response);
            return ret;
        }

        public async Task<AccountResponse> GetAccountAsync(string account)
        {
            var br = await Task<AccountResponse>.Run(() =>
            {
                return GetAccount(account);
            });
            return br;
        }



    /// <summary>
    /// Gets the latest block time and the current time according to the HTTP service.
    /// </summary>
    /// <returns></returns>
    public TimeResponse GetTime()
        {
            //Call specific settings
            string urlPattern = "{0}/time";
            string url = string.Format(urlPattern, _env.HttpsApiAddress);
            int callsPerSecondAllowed = 1;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<TimeResponse>(response);
            return ret;
        }

        public async Task<TimeResponse> GetTimeAsync()
        {
            var br = await Task<TimeResponse>.Run(() =>
            {
                return GetTime();
            });
            return br;
        }



    /// <summary>
    /// Gets runtime information about the node.
    /// </summary>
    /// <returns></returns>
    public string GetNodeInfo()
        {
            //Call specific settings
            string urlPattern = "{0}/node-info";
            string url = string.Format(urlPattern, _env.HttpsApiAddress);
            int callsPerSecondAllowed = 1;

            //Generic items for calls                      
            return GetStringResponse(url, urlPattern, callsPerSecondAllowed);
        }

        public async Task<string> GetNodeInfoAsync()
        {
            var br = await Task<string>.Run(() =>
            {
                return GetNodeInfo();
            });
            return br;
        }

    /// <summary>
    /// Gets the list of validators used in consensus.
    /// </summary>
    /// <returns></returns>
    public string GetValidators()
        {
            //Call specific settings
            string urlPattern = "{0}/validators";
            string url = string.Format(urlPattern, _env.HttpsApiAddress);
            int callsPerSecondAllowed = 10;

            //Generic items for calls                      
            return GetStringResponse(url, urlPattern, callsPerSecondAllowed);
        }

        public async Task<string> GetValidatorsAsync()
        {
            var br = await Task<string>.Run(() =>
            {
                return GetValidators();
            });
            return br;
        }

    /// <summary>
    /// Gets the list of network peers.
    /// </summary>
    /// <returns></returns>
    public List<PeerResponse> GetPeers()
        {
            //Call specific settings
            string urlPattern = "{0}/peers";
            string url = string.Format(urlPattern, _env.HttpsApiAddress);
            int callsPerSecondAllowed = 1;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PeerResponse>>(response);
            return ret;
        }

        public async Task<List<PeerResponse>> GetPeersAsync()
        {
            var br = await Task<List<PeerResponse>>.Run(() =>
            {
                return GetPeers();
            });
            return br;
        }

    /// <summary>
    /// Gets transaction metadata by transaction ID.
    /// </summary>
    /// <returns></returns>
    public string GetTransaction(string txHash)
        {
            //Call specific settings
            string urlPattern = "{0}/tx/{1}?format=json";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, txHash);
            int callsPerSecondAllowed = 10;

            //Generic items for calls                      
            return GetStringResponse(url, urlPattern, callsPerSecondAllowed);
        }

        public async Task<string> GetTransactionAsync(string txHash)
        {
            var br = await Task<string>.Run(() =>
            {
                return GetTransaction(txHash);
            });
            return br;
        }


        /// <summary>
        /// Gets a list of tokens that have been issued.
        /// </summary>
        /// <returns></returns>
        public List<TokensResponse> GetTokens(int limit = 500, int offset = 0)
        {
            //Call specific settings
            string urlPattern = "{0}/tokens?limit={1}&offset={2}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, limit, offset);
            int callsPerSecondAllowed = 1;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TokensResponse>>(response);
            return ret;
        }

        public async Task<List<TokensResponse>> GetTokensAsync(int limit = 500, int offset = 0)
        {
            var br = await Task<List<TokensResponse>>.Run(() =>
            {
                return GetTokens(limit, offset);
            });
            return br;
        }


        /// <summary>
        /// Gets the list of market pairs that have been listed.
        /// </summary>
        /// <returns></returns>
        public List<MarketsResponse> GetMarkets(int limit = 500, int offset = 0)
        {
            //Call specific settings
            string urlPattern = "{0}/markets?limit={1}&offset={2}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, limit, offset);
            int callsPerSecondAllowed = 1;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MarketsResponse>>(response);
            return ret;
        }

        public async Task<List<MarketsResponse>> GetMarketsAsync(int limit = 500, int offset = 0)
        {
            var br = await Task<List<MarketsResponse>>.Run(() =>
            {
                return GetMarkets(limit , offset);
            });
            return br;
        }

    /// <summary>
    /// Gets the order book depth data for a given pair symbol.
    /// </summary>
    /// <returns></returns>
    public DepthResponse GetDepth(string symbol, QueryLimit limit=QueryLimit.All)
        {
            //Call specific settings
            string urlPattern = "{0}/depth?symbol={1}&limit={2}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress,symbol, (int)limit);
            if (url.EndsWith("&limit=0"))
            {
                url = url.Replace("&limit=0", "");
            }
            int callsPerSecondAllowed = 10;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = DepthResponse.FromJSONString(response);
            return ret;
        }

        public async Task<DepthResponse> GetDepthAsync(string symbol, QueryLimit limit = QueryLimit.All)
        {
            var br = await Task<DepthResponse>.Run(() =>
            {
                return GetDepth(symbol, limit );
            });
            return br;
        }

    /// <summary>
    /// Gets candlestick/kline bars for a symbol. Bars are uniquely identified by their open time.
    /// </summary>
    /// <returns></returns>
    public List<KlinesResponse> GetKlines(string symbol, KlineInterval interval, int limit = 300, long startTime = 0, long endTime = 0 )
        {
            //Call specific settings
            string urlPattern = "{0}/klines?symbol={1}&interval={2}&limit={3}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, symbol, TransferNameConverter.Convert(interval),limit);
            StringBuilder sbQueryFilter = new StringBuilder();
            if (startTime > 0)
            {
                sbQueryFilter.AppendFormat("&startTime={0}", startTime);
            }
            if (endTime > 0)
            {
                sbQueryFilter.AppendFormat("&endTime={0}", endTime);
            }
            if (sbQueryFilter.Length > 0)
            {
                url = string.Format("{0}{1}", url, sbQueryFilter);
            }
            int callsPerSecondAllowed = 10;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = KlinesResponse.FromJSONString(response);
            return ret;
        }

        public async Task<List<KlinesResponse>> GetKlinesAsync(string symbol, KlineInterval interval, int limit = 300, long startTime = 0, long endTime = 0)
        {
            var br = await Task<List<KlinesResponse>>.Run(() =>
            {
                return GetKlines(symbol, interval, limit, startTime, endTime);
            });
            return br;
        }


    /// <summary>
    /// Description: Gets closed (filled and cancelled) orders for a given address.
    /// Query Window: Default query window is latest 7 days; The maximum start - end query window is 3 months.
    /// </summary>
    /// <returns></returns>
    public OrdersResponse GetOrdersClosed(string address, string symbol="", OrderStatusQuery status= OrderStatusQuery.All , OrderSideQuery side = OrderSideQuery.All, int limit = 500,
            int offset=0, bool totalRequired = false, long start = 0, long end = 0)
        {
            //Call specific settings
            string urlPattern = "{0}/orders/closed?address={1}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, address);
            StringBuilder sbQueyrFilter = new StringBuilder();
            
            if (end > 0)
            {
                sbQueyrFilter.AppendFormat("&end={0}", end);
            }
            if (limit != 500)
            {
                sbQueyrFilter.AppendFormat("&limit={0}", limit);
            }
            if (offset > 0)
            {
                sbQueyrFilter.AppendFormat("&offset={0}", offset);
            }
            if (start > 0)
            {
                sbQueyrFilter.AppendFormat("&start={0}", start);
            }
            if (status != OrderStatusQuery.All)
            {
                sbQueyrFilter.AppendFormat("&status={0}", status.ToString());
            }
            if (!string.IsNullOrWhiteSpace(symbol))
            {
                sbQueyrFilter.AppendFormat("&symbol={0}", symbol);
            }
            if (totalRequired)
            {
                sbQueyrFilter.Append("&total=1");
            }

            if (sbQueyrFilter.Length > 0)
            {
                url = string.Format("{0}{1}", url, sbQueyrFilter);
            }
            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<OrdersResponse>(response);
            return ret;
        }

        public async Task<OrdersResponse> GetOrdersClosedAsync(string address, string symbol = "", OrderStatusQuery status = OrderStatusQuery.All, OrderSideQuery side = OrderSideQuery.All, int limit = 500,
            int offset = 0, bool totalRequired = false, long start = 0, long end = 0)
        {
            var br = await Task<OrdersResponse>.Run(() =>
            {
                return GetOrdersClosed(address, symbol, status, side, limit, offset, totalRequired, start, end);
            });
            return br;
        }


    /// <summary>
    /// Gets open orders for a given address.
    /// </summary>
    /// <returns></returns>
    public OrdersResponse GetOrdersOpen(string address, string symbol = "", int limit = 500, int offset = 0, bool total = false, long start = 0, long end = 0)
        {
            //Call specific settings
            string urlPattern = "{0}/orders/open?address={1}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, address);
            StringBuilder sbQueyrFilter = new StringBuilder();
                       
            if (limit != 500)
            {
                sbQueyrFilter.AppendFormat("&limit={0}", limit);
            }
            if (offset > 0)
            {
                sbQueyrFilter.AppendFormat("&offset={0}", offset);
            }
            if (!string.IsNullOrWhiteSpace(symbol))
            {
                sbQueyrFilter.AppendFormat("&symbol={0}", symbol);
            }
            if (total)
            {
                sbQueyrFilter.Append("&total=1");
            }

            if (sbQueyrFilter.Length > 0)
            {
                url = string.Format("{0}{1}", url, sbQueyrFilter);
            }
            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<OrdersResponse>(response);
            return ret;
        }

        public async Task<OrdersResponse> GetOrdersOpenAsync(string address, string symbol = "", int limit = 500, int offset = 0, bool total = false, long start = 0, long end = 0)
        {
            var br = await Task.Run(() =>
            {
                return GetOrdersOpen(address, symbol, limit, offset, total, start, end);
            });
            return br;
        }



    /// <summary>
    /// Gets metadata for an individual order by its ID.
    /// </summary>
    /// <returns></returns>
    public Order GetOrderById(string id)
        {
            //Call specific settings
            string urlPattern = "{0}/orders/{1}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress, id);
            
            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(response);
            return ret;
        }

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            var br = await Task.Run(() =>
            {
                return GetOrderById(id);
            });
            return br;
        }

    /// <summary>
    /// Gets 24 hour price change statistics for a market pair symbol.
    /// </summary>
    /// <returns></returns>
    public List<TickerResponse> GetTicker(string symbol="")
        {
            //Call specific settings
            string urlPattern = "{0}/ticker/24hr";
            string url = string.Format(urlPattern, _env.HttpsApiAddress);
            StringBuilder sbQueyrFilter = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(symbol))
            {
                sbQueyrFilter.AppendFormat("&symbol={0}", symbol);
            }
            if (sbQueyrFilter.Length > 0)
            {
                url = string.Format("{0}{1}", url, sbQueyrFilter);
            }

            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TickerResponse>>(response);
            return ret;
        }

        public async Task<List<TickerResponse>> GetTickerAsync(string symbol = "")
        {
            var br = await Task.Run(() =>
            {
                return GetTicker(symbol);
            });
            return br;
        }




    /// <summary>
    /// Description: Gets a list of historical trades.
    /// Query Window: Default query window is latest 7 days; The maximum start - end query window is 3 months.
    /// </summary>
    /// <returns></returns>
    public TradesResponse GetTrades(long blockHeight = 0, string symbol = "", string address = "", OrderSideQuery side = OrderSideQuery.All, string buyerOrderId = "", string sellerOrderId = "",
            long limit = 500, long offset = 0, int start = 0, int end = 0, bool totalRequired = false)
        {
            //Call specific settings
            string urlPattern = "{0}/trades";
            string url = string.Format(urlPattern, _env.HttpsApiAddress);
            StringBuilder sbQueyrFilter = new StringBuilder();
            if (side != OrderSideQuery.All)
            {
                sbQueyrFilter.AppendFormat("&side={0}", (int)side);
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                sbQueyrFilter.AppendFormat("&address={0}", address);
            }
            if (!string.IsNullOrWhiteSpace(buyerOrderId))
            {
                sbQueyrFilter.AppendFormat("&buyerOrderId={0}", buyerOrderId);
            }
            if (!string.IsNullOrWhiteSpace(sellerOrderId))
            {
                sbQueyrFilter.AppendFormat("&sellerOrderId={0}", sellerOrderId);
            }
            if (blockHeight > 0)
            {
                sbQueyrFilter.AppendFormat("&height={0}", blockHeight);
            }
            if (end > 0)
            {
                sbQueyrFilter.AppendFormat("&end={0}", end);
            }
            if (limit != 500)
            {
                sbQueyrFilter.AppendFormat("&limit={0}", limit);
            }
            if (offset > 0)
            {
                sbQueyrFilter.AppendFormat("&offset={0}", offset);
            }
            if (start > 0)
            {
                sbQueyrFilter.AppendFormat("&start={0}", start);
            }
            if (!string.IsNullOrWhiteSpace(symbol))
            {
                sbQueyrFilter.AppendFormat("&symbol={0}", symbol);
            }
            if (totalRequired)
            {
                sbQueyrFilter.Append("&total=1");
            }



            if (sbQueyrFilter.Length > 0)
            {
                url = string.Format("{0}{1}", url, sbQueyrFilter);
            }

            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<TradesResponse>(response);
            return ret;
        }

        public async Task<TradesResponse> GetTradesAsync(long blockHeight = 0, string symbol = "", string address = "", OrderSideQuery side = OrderSideQuery.All, string buyerOrderId = "", string sellerOrderId = "",
            long limit = 500, long offset = 0, int start = 0, int end = 0, bool totalRequired = false)
        {
            var br = await Task.Run(() =>
            {
                return GetTrades(blockHeight, symbol, address, side, buyerOrderId, sellerOrderId,
             limit, offset, start, end, totalRequired);
            });
            return br;
        }


    /// <summary>
    /// Get historical trading fees of the address, including fees of trade/canceled order/expired order. Transfer and other transaction fees are not included. Order by block height DESC.
    /// </summary>
    /// <returns></returns>
    public BlockExchangeFeeResponse GetBlockExchangeFee(string address, long limit = 500, long offset = 0, int start = 0, int end = 0, bool totalRequired = false)
        {
            //Call specific settings
            string urlPattern = "{0}/block-exchange-fee?address={1}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress,address);
            StringBuilder sbQueyrFilter = new StringBuilder();
            if (end > 0)
            {
                sbQueyrFilter.AppendFormat("&end={0}", end);
            }
            if (limit != 500)
            {
                sbQueyrFilter.AppendFormat("&limit={0}", limit);
            }
            if (offset > 0)
            {
                sbQueyrFilter.AppendFormat("&offset={0}", offset);
            }
            if (start > 0)
            {
                sbQueyrFilter.AppendFormat("&start={0}", start);
            }
            if (totalRequired)
            {
                sbQueyrFilter.Append("&total=1");
            }



            if (sbQueyrFilter.Length > 0)
            {
                url = string.Format("{0}{1}", url, sbQueyrFilter);
            }

            int callsPerSecondAllowed = 5;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerSecondAllowed);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<BlockExchangeFeeResponse>(response);
            return ret;
        }

        public async Task<BlockExchangeFeeResponse> GetBlockExchangeFeeAsync(string address, long limit = 500, long offset = 0, int start = 0, int end = 0, bool totalRequired = false)
        {
            var br = await Task.Run(() =>
            {
                return GetBlockExchangeFee(address, limit, offset, start, end, totalRequired);
            });
            return br;
        }

    /// <summary>
    /// Gets a list of transactions. Multisend transaction is not available in this API.
    /// Query Window: Default query window is latest 24 hours; The maximum start - end query window is 3 months.
    /// </summary>
    /// <returns></returns>
    public TransactionsResponse GetTransactions(string address, TransactionSide transactionSide = TransactionSide.All, TxType txType = TxType.All,
            string txAsset = "", long blockHeight = 0, string symbol = "",
            long limit = 0, long offset = 0, int startTime = 0, int endTime = 0)
        {
            //Call specific settings
            string urlPattern = "{0}/transactions?address={1}";
            string url = string.Format(urlPattern, _env.HttpsApiAddress,address);
            StringBuilder sbQueyrFilter = new StringBuilder();
            if (transactionSide != TransactionSide.All)
            {
                sbQueyrFilter.AppendFormat("&side={0}", transactionSide.ToString());
            }
            if (txType != TxType.All)
            {
                sbQueyrFilter.AppendFormat("&txType={0}", txType.ToString());
            }
            if (!string.IsNullOrWhiteSpace(txAsset))
            {
                sbQueyrFilter.AppendFormat("&txAsset={0}", txAsset);
            }
            if (blockHeight > 0)
            {
                sbQueyrFilter.AppendFormat("&blockHeight={0}", blockHeight);
            }
            if (endTime > 0)
            {
                sbQueyrFilter.AppendFormat("&endTime={0}", endTime);
            }
            if (limit > 0)
            {
                sbQueyrFilter.AppendFormat("&limit={0}", limit);
            }
            if (offset > 0)
            {
                sbQueyrFilter.AppendFormat("&offset={0}", offset);
            }
            if (startTime > 0)
            {
                sbQueyrFilter.AppendFormat("&startTime={0}", startTime);
            }


            if (sbQueyrFilter.Length > 0)
            {
                url = string.Format("{0}{1}", url, sbQueyrFilter);
            }

            int callsPerMinuteAllowed = 60;

            //Generic items for calls                      
            var response = GetStringResponse(url, urlPattern, callsPerMinuteAllowed,60);

            //Call specific processing of returned values
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<TransactionsResponse>(response);
            return ret;
        }

        public async Task<TransactionsResponse> GetTransactionsAsync(string address, TransactionSide transactionSide = TransactionSide.All, TxType txType = TxType.All,
            string txAsset = "", long blockHeight = 0, string symbol = "",
            long limit = 0, long offset = 0, int startTime = 0, int endTime = 0)
        {
            var br = await Task.Run(() =>
            {
                return GetTransactions(address, transactionSide, txType, txAsset, blockHeight, symbol, limit, offset, startTime, endTime);
            });
            return br;
        }




















    public BroadcastResponse BroadcastToBlockchain(byte[] message, bool waitForConfirmation = true)
        {
            string url = string.Format("{0}/broadcast?sync={1}", _env.HttpsApiAddress, waitForConfirmation);
            try
            {                
                var response = Broadcast.BroadcastToBlockchain(url, message);
                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BroadcastResponse>>(response);
                return ret.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new BinanceHTTPApiRequestException(string.Format("An error occured during an API POST broadcast to ", url), ex);
            }            
        }
                                    

        /// <summary>
        /// Method to retrieve HTTP get response for an api call with path level throttling for API calls
        /// </summary>
        /// <param name="url">The exact path to be used in the call</param>
        /// <param name="urlPattern">the pattern that serves as key for the throttling mechanism</param>
        /// <param name="callsPerIntervalAllowed">The number of calls that can be sent out in a second without throttling (0 = no throttling)</param>
        /// <returns></returns>
        private string GetStringResponse(string url, string urlPattern, int callsPerIntervalAllowed, int intervalLenghtInSeconds=1)
        {
            try
            {
                if (!RequestThrottling)
                {
                    callsPerIntervalAllowed = 0;
                }
                return GetRequest.DownloadResult(url, urlPattern, intervalLenghtInSeconds, callsPerIntervalAllowed);
            }
            catch (Exception ex)
            {
                throw new BinanceHTTPApiRequestException(string.Format("An error occured during an API a GET call to {0}", url), ex);
            }
        }
    }
}
