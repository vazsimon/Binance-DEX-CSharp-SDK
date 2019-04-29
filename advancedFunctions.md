# Binance-DEX-C#-SDK

Fully featured SDK for Binance DEX. Use it, enhance it further, make some profit in crypto, spread the word, support mass adoption. :)

---

[Summary](README.md)  | [Getting Started](gettingStarted.md) | [**Advanced Functions**](advancedFunctions.md)

---

Advanced functions
 ---

For successfully trading using the Binance DEX C# sdk, the getting started section is completely enough. However, there are some tweaks and functions that you might be interested in if you want to dive deeper into trading on Binance DEX.

- [Sequence Ensure Modes](#Sequence-Ensure-Modes)
- [NodeRPC Websocket Direct Transaction Broadcast](#NodeRPC-Websocket-Direct-Transaction-Broadcast)
- [Realtime Orderbook](#Realtime-Orderbook)
- [Top Of The Book Skimmer](#Top-Of-The-Book-Skimmer)
---
- [Node RPC websocket Subscriptions and queries](#Node-RPC-websocket-Subscriptions-and-queries)
---
- [Binance Chain Documentation](#Binance-Chain-Documentation)

## Sequence Ensure Mode Override

To prevent malicious actors from intercepting and later re-sending our signed transactions to the blockchain, every transaction must contain a sequence number. The sequence number for the private key has to be incremented by 1 on each transaction.
Because of this, we have to know the next sequence number that the blockchain is going to accept our transaction with before we build a message. There are many ways to achieve this, here are the modes that this sdk uses, with their corresponding enum values:

```SequenceEnsureMode.VerifyBeforeSend```

This makes a call through the HTTP client to the blockchain to get the current sequence number. This is the safest, works even if other applications are also using our private key. However, the additional call might cause us to miss the earliest block where we can get our transaction accepted.

```SequenceEnsureMode.WaitForConfirmation```

This doesn't make a call before building the message, it assumes we already have the proper sequence number. We can get the transaction into the earliest block. To ensure this, after sending out a transaction, it waits to see if it gets accepted into a block or not. If it was accepted, it increments the local representation of the sequence number.
This works well if no other applications are using our private key. This is the **default behaivor**.

```SequenceEnsureMode.VerifyBeforeSendAndWaitForConfirmation```

In this mode, it does both checks. The advantage is that we can see right away if a transaction didn't get accepted from the response, and other apps can use the private key also simultaneously (if they support ensuring the proper sequence number). 

```SequenceEnsureMode.Manual```

In this mode, it does't make any additional checks, we have to ensure the proper sequence number stored in the wallet manually. We can do it based on the realtime feeds provided by the websockets client. Optimal if we want to submit multiple transactions into the same block.


Setting the SequenceEnsureMode can be done in the constructor of the Client class with an optional parameter or can be done directly with setting this property:

```csharp
ar client = new Client("374C66493B79D2DE9C6FD8E300B279A0C43EE1BCA610E953F5E86D31FBCFD075", Network.Test, SequenceEnsureMode.Manual);
```

```csharp
var client = new Client("374C66493B79D2DE9C6FD8E300B279A0C43EE1BCA610E953F5E86D31FBCFD075", Network.Test);
client.sequenceEnsureMode = SequenceEnsureMode.Manual;
```

To update the sequence number on the wallet, we can either set it manually to a value or we can let the wallet to make a call to the HTTP api to refresh it.
```csharp
client.Wallet.SetSequence(42);
```


```csharp
client.Wallet.RefreshSequence();
```

---


## NodeRPC Websocket Direct Transaction Broadcast

Instead of broadcasting transactions to the HTTP endpoint, we can streamline transaction broadcasts by submitting them through the Node RPC websocket client, directly to a node. As this is not a core function, the Node RPC client is a standalone class, it is not included in the main client at the moment. For this reason, we have to get the NodeRPC websocket address for the connection ourselves.
We can do this by calling the ```GetPeers()``` function of the HTTP client and search for a seed node in the response and connect to it through the wss:// (secure websocket) protocol at the /websocket endpoint.

Once we have the connection address, we can build our transaction data through the ```BroadcastMessageBuilder``` class's Build...Message functions, like so:

```csharp
NodeRPCClient clientRPC = new NodeRPCClient("wss://seed-pre-s3.binance.org/websocket");
var msg = BroadcastMessageBuilder.BuildTokenFreezeMessage("BNB", (decimal)0.01, client.Wallet);
clientRPC.BroadcastTransaction(msg, RPCBroadcastMode.async);
client.Wallet.IncrementSequence();
```

We can broadcast the messages through the RPC websockets connection in 3 modes
- ```RPCBroadcastMode.async```
 "This method just return transaction hash right away and there is no return from CheckTx or DeliverTx."
- ```RPCBroadcastMode.commit``` "The transaction will be broadcasted and returns with the response from CheckTx and DeliverTx. This method will waitCONTRACT: only returns error if mempool.CheckTx() errs or if we timeout waiting for tx to commit. If CheckTx or DeliverTx fail, no error will be returned, but the returned result will contain a non-OK ABCI code."
- ```RPCBroadcastMode.sync``` "The transaction will be broadcasted and returns with the response from CheckTx."

> Note: The RPC websocket connection is automatically opened and it is kept open in the RPC websocket client. After the first request (broadcast or query), the connection handshake will not take time any more, we can write information into the websocket right away.


## Realtime Orderbook

The SDK has a built-in class for maintaining local realtime representations of orderbooks. It uses the same orderbook data representation format that we have seen in the getting started section. We can simultaneously subscribe to multiple orderbooks with this class, each orderbook has an event which fires at every update :
```csharp
realtimebooks = new RealtimeOrderBookHandler(client.HTTP, client.Websockets);
realtimebooks.Subscribe("000-EF6_BNB");
realtimebooks.BooksForSymbols["000-EF6_BNB"].OnOrderbookUpdated += Form1_OnOrderbookUpdated;

///...

private void Form1_OnOrderbookUpdated(object sender, OrderBookUpdatedArgs e)
{
	var lastUpdateTime = e.UpdateTime;
}
```

As the realtime orderbook is going to be updated from a different thread, when we are using it - to ensure it doesn't get modified halfway through our processings - we can obtain a lock on the orderbook's operationLock object. With this, we ensure the updating processes will wait until we release the lock for the given orderbook.
```csharp
lock (realtimebooks.BooksForSymbols["000-EF6_BNB"].operationsLock)
{
	var bestAsk = realtimebooks.BooksForSymbols["000-EF6_BNB"].AskBook.First();
	foreach (var orderBookItem in realtimebooks.BooksForSymbols["000-EF6_BNB"].AskBook)
	{
		//do some work here
	}
}
```
> Note: Please don't keep the operationsLock active longer than it is necessary. Until the lock is released, all the incoming updates are waiting in a queue to be processed for the given symbol.

## Top Of The Book Skimmer

The purpose of this class is to obtain a better average price for buys/sells by gradually trading on the top of the book in small steps rather than sending in one [large price aggressive order.](https://docs.binance.org/match.html#what-exactly-is-binance-dex-matching-logic) This class will keep taking away the top of the book with IOC orders until we receive fills for the whole desired quantity.
> Note: this is not an optimal strategy by any means, but on low liquidity markets, it is certainly better than the 1 big order method. The goal here was to illustrate the capabilities of the SDK.

This implementation of the skimmer works properly only if the following pre-conditions are all met:
- You don't have any open orders on the opposite side of accumulation/distribution
- you have enough account balance of the required coins to fully complete the accumulation/distribution
- you have to ensure no other app is using the private key during accumulation/distribution
- once accumulation/distribution is complete, if you need higher resiliency in the Binance client, please update the client's sequenceEnsureMode accordingly. This algo sets it to Manual to increase the chances of getting the orders in the nearest block.

2 examples of the usage:

```csharp
TopOfTheBookSkimmerHandler handler = new TopOfTheBookSkimmerHandler(client);
handler.Accumulate("000-EF6_BNB", (decimal)0.00004);
```

```csharp
TopOfTheBookSkimmerHandler handler = new TopOfTheBookSkimmerHandler(client);
handler.Distribute("000-EF6_BNB", (decimal)0.001);
```

---
## Node RPC websocket Subscriptions and queries

These functions will mostly be useful for you if you develop or get hold of a full amino decoder for C#. You can use the RPC websockets client to subscribe to events and get notifications from them:
```csharp
NodeRPCClient clientRPC = new NodeRPCClient("wss://seed-pre-s3.binance.org/websocket");
clientRPC.Subscribe("tm.event = 'Tx'");
clientRPC.OnEventReceived += ClientRPC_OnEventReceived;

//...

//we can unsubscribe if we no longer need this
clientRPC.Unbscribe("tm.event = 'Tx'");
//Or we can use the UnsubscribeAll to unsubscribe from everything
clientRPC.UnbscribeAll();

//...


private void ClientRPC_OnEventReceived(object sender, BinanceClient.NodeRPC.Models.Args.RPCEventArgs e)
{
	var result = e.Result;
}
```

On top of this, the Node RPC client also provides the following calls:
- AbciInfo
- ConsensusState
- DupmConsensusState
- Genesis
- Health
- NetInfo
- NumUnconfirmedTransactions
- Status
- ABCIQuery
- QueryBlockchainInfo
- QueryBlock
- QueryCommit
- QueryBlockResults
- QueryTx
- QueryTxSearch

## Binance Chain Documentation
The original documentation that was used to build this sdk:


https://docs.binance.org/index.html