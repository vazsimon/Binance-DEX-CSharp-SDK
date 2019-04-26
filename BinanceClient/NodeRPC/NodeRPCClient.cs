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
        }

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
    }
}
