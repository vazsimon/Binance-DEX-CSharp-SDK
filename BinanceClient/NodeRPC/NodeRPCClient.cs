using BinanceClient.Enums;
using BinanceClient.NodeRPC.Models.Args;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.NodeRPC
{
    public class NodeRPCClient
    {
        private string address;
        WebsocketJsonRpcConnector connector;
        public NodeRPCClient(string address)
        {
            connector = new WebsocketJsonRpcConnector(address);
            this.address = address;
            connector.OnEventReceived += Connector_OnEventReceived;
        }

        public event EventHandler<RPCEventArgs> OnEventReceived;

        public dynamic AbciInfo()
        {
            return connector.GetResponse("abci_info");
        }

        public dynamic ConsensusState()
        {
            return connector.GetResponse("consensus_state");
        }

        public dynamic DupmConsensusState()
        {
            return connector.GetResponse("dump_consensus_state");
        }

        public dynamic Genesis()
        {
            return connector.GetResponse("genesis");
        }

        public dynamic Health()
        {
            return connector.GetResponse("health");
        }

        public dynamic NetInfo()
        {
            return connector.GetResponse("net_info");
        }

        public dynamic NumUnconfirmedTransactions()
        {
            return connector.GetResponse("num_unconfirmed_txs");
        }

        public dynamic Status()
        {
            return connector.GetResponse("status");
        }

        public dynamic ABCIQuery(string path, byte[] data, long? height = null, bool? prove = null)
        {
            var payLoad = new { path = path, data = Convert.ToBase64String(data), height = height != null ? height.Value.ToString() : null , prove = prove };
            var payloadStr = JsonConvert.SerializeObject(payLoad,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            return connector.GetResponse("abci_query", payloadStr);
        }

        public dynamic QueryBlockchainInfo(long minHeight, long maxHeight)
        {
            var payload = string.Format("{{\"minHeight\":\"{0}\",\"maxHeight\":\"{1}\"}}", minHeight, maxHeight);
            return connector.GetResponse("blockchain", payload);
        }
        
        public dynamic QueryBlock(long height)
        {
            var payload = string.Format("{{\"height\":\"{0}\"}}", height);
            return connector.GetResponse("block", payload);
        }

        public dynamic QueryCommit(long height=0)
        {
            if (height > 0)
            {
                var payload = string.Format("{{\"height\":\"{0}\"}}", height);
                return connector.GetResponse("commit", payload);
            }
            else
            {
                return connector.GetResponse("commit");
            }
        }

        public dynamic QueryBlockResults(long height = 0)
        {
            if (height > 0)
            {
                var payload = string.Format("{{\"height\":\"{0}\"}}", height);
                return connector.GetResponse("block_results", payload);
            }
            else
            {
                return connector.GetResponse("block_results");
            }
        }

        public dynamic QueryTx(byte[] hash, bool? prove = null)
        {
            var payLoad = new {  hash = Convert.ToBase64String(hash), prove = prove };
            var payloadStr = JsonConvert.SerializeObject(payLoad,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            return connector.GetResponse("tx", payloadStr);
        }

        public dynamic QueryTxSearch(string query, int page = 1, int per_page = 30, bool? prove = null)
        {
            var payLoad = new { query = query, page = page.ToString(), per_page = per_page.ToString(), prove = prove };
            var payloadStr = JsonConvert.SerializeObject(payLoad,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            return connector.GetResponse("tx_search", payloadStr);
        }



























        public dynamic BroadcastTransaction(byte[] msg, RPCBroadcastMode broadcastMode)
        {
            var messageStr = Convert.ToBase64String(msg);
            var payload = string.Format("{{\"tx\":\"{0}\"}}", messageStr);
            string method = string.Empty;
            switch (broadcastMode)
            {
                case RPCBroadcastMode.async:
                    method = "broadcast_tx_async";
                    break;
                case RPCBroadcastMode.commit:
                    method = "broadcast_tx_commit";
                    break;
                case RPCBroadcastMode.sync:
                    method = "broadcast_tx_sync";
                    break;
            }
            return connector.GetResponse(method, payload);
        }

        public dynamic Subscribe(string query)
        {
            var payload = string.Format("{{\"query\":\"{0}\"}}", query);
            return connector.GetResponse("subscribe", payload);            
        }

        public dynamic Unbscribe(string query)
        {
            var payload = string.Format("{{\"query\":\"{0}\"}}", query);
            return connector.GetResponse("unsubscribe", payload);
        }

        public dynamic UnbscribeAll()
        {
            return connector.GetResponse("unsubscribe_all");
        }

        private void Connector_OnEventReceived(object sender, RPCEventArgs e)
        {
            OnEventReceived?.Invoke(this, e);
        }
    }
}
