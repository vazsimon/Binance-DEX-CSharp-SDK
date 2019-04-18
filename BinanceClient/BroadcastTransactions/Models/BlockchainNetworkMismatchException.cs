using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.BroadcastTransactions.Models
{
    class BlockchainNetworkMismatchException : Exception
    {
        public BlockchainNetworkMismatchException()
        {
        }

        public BlockchainNetworkMismatchException(string message) : base(message)
        {
        }

        public BlockchainNetworkMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlockchainNetworkMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
