using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceClient.Models
{
    /// <summary>
    /// We need to maintain the account's sequence number to comply with the transaction replay protection
    /// </summary>
    public enum SequenceEnsureMode
    {
        /// <summary>
        /// Safest choice, default behavior. For non time-critical use-cases Ensures that no sequence or transaction issues go unnoticed
        /// </summary>
        VerifyBeforeSendAndWaitForConfirmation,
        /// <summary>
        /// Safe for sequence nubmer but we don't get an error if the transaction is rejected. For non time-critical use-cases. Before every call, we retrieve the actual sequence from the http api.
        /// Still delays the broadcast of the transaction
        /// </summary>
        VerifyBeforeSend,
        /// <summary>
        /// Optimal balance for normal use, if no other app is using the private key. After every call, we see if the transaction got recorded to the blockchain or not.
        /// If it was successful, we increase the sequencenumber. There is no delay (verification) before entering transactions (fastest order entry), but afterwards, we
        /// have to wait until the transaction is recorded on the blockchain to see if we need to increase the sequence.
        /// </summary>
        WaitForConfirmation,
        /// <summary>
        /// For extreme time critical scenarios, use with extra caution. The client assumes that all transactions are accepted by the network, doesn't do
        /// anything time consuming to verify. If a transaction fails, the programmer has to manually reset the sequence number in the wallet.
        /// </summary>
        Manual
    }
}
