# Binance-DEX-C#-SDK

Fully featured SDK for Binance DEX. Use it, enhance it further, make some profit in crypto, spread the word, support mass adoption. :)

---

[Summary](README.md) | [**Getting Started**](gettingStarted.md) | [Advanced Functions](advancedFunctions.md) 

---

## Getting started

---

- [Installation](#Installation)
- [Wallet](#Wallet)
- [Client](#Client)
  - [Broadcast HTTP](#Broadcast-HTTP)
  - [Market Data HTTP](#Market-Data-HTTP)
  - [Realtime Market Data Websocket](#Realtime-Market-Data-Websocket)
## Installation
Search for the BinanceDEXClient in the public NuGet library or

open tools > NuGet Package Manager > package Manager console:

	PM> Install-Package BinanceDEXClient -Version 1.0.2.1

Alternatively, you can also clone or download the repository and build the binaries yourself.

> Note: The class library got updated to .net standard 2.0 for maximum compatability.



## Wallet

For every transaction we send to the dex blockchain, we need to have a valid address and the private key corresponding to it. On testnet, addresses start with "tbnb", on mainnet, addresses start with "bnb". We can generate one using this static function of the Wallet class:
```csharp
var walletInfoTestNet = Wallet.CreateRandomAccount(Network.Test);
var walletInfoMainNet = Wallet.CreateRandomAccount(Network.Mainnet);
```
The result format:
```json
{
	"Mnemonic": "smart exile bring giggle involve control category maze sock saddle abstract tuition fun derive confirm improve local exist away only benefit behave topic else",
	"PrivateKey": "374C66493B79D2DE9C6FD8E300B279A0C43EE1BCA610E953F5E86D31FBCFD075",
	"Address": "tbnb1kwctdz8pdtevf866a68h6wk0wgs83uyjwtg282",
	"Network": Network.Test
}
```

##### Restoring a Wallet from mnemonic
```csharp
var wallet = Wallet.RestoreWalletFromMnemonic("smart exile bring giggle involve control category maze sock saddle abstract tuition fun derive confirm improve local exist away only benefit behave topic else", Network.Test);
```

##### Initializing a wallet
This is done automatically by the Client class, but if we need a wallet for some reason, we can create one with supplying the private key for the constructor:
```csharp
var wallet = new Wallet("374C66493B79D2DE9C6FD8E300B279A0C43EE1BCA610E953F5E86D31FBCFD075", Network.Test);
```

## Client

This is our main object, we can access almost all the functions of the SDK through this. We can create one by supplying a private key in the constructor or by supplying an already intitialsed wallet.
For performance considerations, - If you don't have any strong reason to do otherwise - please only use one client per private key throughout the entire application. Also, it is better if you create the client once and keep the reference to it somewhere than to re-create everything and go through the initialization sequence before every request.

```csharp
var client = new Client("374C66493B79D2DE9C6FD8E300B279A0C43EE1BCA610E953F5E86D31FBCFD075", Network.Test);
var clientCreatedWithWallet = new Client(wallet);
```

### Broadcast HTTP
Here you can find examples of sending each available broadcast transaction through the HTTP interface:

**Placing order:**
```csharp
var response = client.NewOrder("000-EF6_BNB", OrderType.Limit, Side.Sell, (decimal)499.999, (decimal)0.00001, TimeInForce.GTE);
```
> Note: TimeInForce can be either  [Immediate or Cancel (IOC)](https://docs.binance.org/faq.html#what-is-immediate-or-cancel-order) or [Good Till Expiry (GTE)](https://docs.binance.org/faq.html#what-is-order-expire). Only limit orders are allowed at the moment by the blockchain.

**Cancelling an order by symbol and orderId:**
```csharp
var response = client.CancelOrder("000-EF6_BNB", "34D5035164CA688605D4423668931B692BBD2654-28");
```
**Transfer coin:**
```csharp
var response = client.Send("tbnb1qqy83vaerqz7yv4yfqtz8pt0gl0wssxyl826h4", "BNB", 42);
```
**Transfer coin, with message:**
```csharp
var response = client.Send("tbnb1qqy83vaerqz7yv4yfqtz8pt0gl0wssxyl826h4", "000-EF6", 42, "Now sending with a message");
```
**Transfer multiple coins.** This can involve transferring multiple types of coins	to muliple addresses. Using this transaction type will result in having [better fees](https://docs.binance.org/trading-spec.html#multi-send-fees) than sending multiple coins to multiple addresses one by one.
```csharp
List<MultipleSendDestination> destinations = new List<MultipleSendDestination>();
destinations.Add(new MultipleSendDestination { Address = "tbnb1qqy83vaerqz7yv4yfqtz8pt0gl0wssxyl826h4", Amount = (decimal)0.3, coin = "BNB" });
destinations.Add(new MultipleSendDestination { Address = "tbnb17fq8s55rdshy04zjq0lfkexs4jeclmfnaaa2f2", Amount = (decimal)0.12, coin = "BNB" });
var response = client.MultiSend(destinations);
```
**Freeze coin:**

```csharp
var response = client.FreezeToken("BNB", 1);
```
**Unfreeze coin:**

```csharp
var response = client.UnfreezeToken("BNB", 1);
```

**Vote:**

```csharp
var response = client.Vote(proposalId, VoteOptions.OptionYes);
```


### Market Data HTTP
The HTTP client provides calls to access the HTTP api's endpoints. We can call it with the built-in HTTP client from our main client object or we can create a standalone HTTPClient object.
Some examples are shown here below:

Getting the 5 minutes candles for the "000-EF6_BNB" symbol:
```csharp
var candles = client.HTTP.GetKlines("000-EF6_BNB", KlineInterval.m5);
```
Getting the current orderbook for "000-EF6_BNB" and saving the top of the book qty and price of the bid side to local variables:
```csharp
var orderBook = client.HTTP.GetDepth("000-EF6_BNB");
var bestBid = orderBook.Bids.FirstOrDefault();
decimal bestBidPrice = bestBid.Key;
decimal bestBidQty = bestBid.Value;
```
> In this implementation, for easy computations, the orderbook is represented in 2 SortedDictionary<decimal,decimal> types, one for bids and one for asks. The key is the price level and the value is the quantity available at the given price level. The bids are sorted descending, the asks are sorted ascending, so the first KeyValuePair in a standard enumerator will always be the top of the book (best offer) and we can iterate down the book as deep as we want to, always in the proper order.


Getting the account info (free, locked and frozen balances, sequence number):
```csharp
var acc = client.HTTP.GetAccount("tbnb1qqy83vaerqz7yv4yfqtz8pt0gl0wssxyl826h4");
```

The full list of available calls in the HTTP client:

- GetAccount
- GetAccountAsync
- GetAccountSequence
- GetAccountSequenceAsync
- GetBlockExchangeFee
- GetBlockExchangeFeeAsync
- GetDepth
- GetDepthAsync
- GetKlines
- GetKlinesAsync
- GetMarkets
- GetMarketsAsync
- GetNodeInfo
- GetNodeInfoAsync
- GetOrderById
- GetOrderByIdAsync
- GetOrdersClosed
- GetOrdersClosedAsync
- GetOrdersOpen
- GetOrdersOpenAsync
- GetPeers
- GetPeersAsync
- GetTicker
- GetTickerAsync
- GetTime
- GetTimeAsync
- GetTokens
- GetTokensAsync
- GetTrades
- GetTradesAsync
- GetTransaction
- GetTransactionAsync
- GetValidators
- GetValidators
- GetValidatorsAsync


### Realtime Market Data Websocket

The websocket client provides realtime updates as new information is flowing in from the corresponding streams from the DEX websocket api. To receive updates, we need to subscribe to the stream that we are interested in, and we need to hook our event handler function onto the stream's appropriate event. The event arguments are all strongly typed.

In this example, we are listening for fills/order acknowledgements for an address:
```csharp
client.Websockets.Orders.OnOrdersReceived += Orders_OnOrdersReceived;
client.Websockets.Orders.Subscribe("tbnb1xn2sx5tyef5gvpw5ggmx3ycmdy4m6fj57zanqd");

//...
//And when we don't want to receive any more updates, we can unsubscribe:
client.Websockets.Orders.Unsubscribe("tbnb1xn2sx5tyef5gvpw5ggmx3ycmdy4m6fj57zanqd");


//And the handler function:

private void Orders_OnOrdersReceived(object sender, OrdersArg e)
{
	var symbolForOrder = e.OrderUpdates.First().Symbol;
}
```

In this example, we are getting all the trades happening on a given instrument:
```csharp
client.Websockets.Trades.OnTradesReceived += Trades_OnTradesReceived;
client.Websockets.Trades.Subscribe("000-EF6_BNB");

//And the handler function:

private void Trades_OnTradesReceived(object sender, TradesArgs e)
{
	var firstTrade = e.Trades.FirstOrDefault();
	var price = firstTrade.Price;
	var qty = firstTrade.Quantity;
	string buyerAddress = firstTrade.BuyerAddress;
	string sellerAddress = firstTrade.SellerAddress;
	string symbol = firstTrade.Symbol;
}
```
> Note: We can subscribe to multipe addresses/instruments, in our handler, we have to check to which one the event corresponds to. We have all the data we need for this in the eventArgs 


The full list of streams we can subscribe to:
- Account
- AllMiniTicker
- BlockHeight
- BookDepth
- DiffDepth
- IndividualMiniTicker
- IndividualTicker
- Klines
- Orders
- Trades
- Transfers


---
> Note on added DateTime properties of the .NET message class representations : When we receive data from DEX backend, we always receive datetime fields as long (javascript syntax, in millisecond, second or an additional format, depending on the call). The sdk binds every value as it receives to the class properties. On top of that, for easier processing, the classes provide the same datetime field data also in standard .NET datetime format in a property which name starts with the original field's name and is postfixed with "DF". Like (long) EventTime and (DateTime) EventTimeDF. This date format property only gets calculated when it is accessed, it is always directly linked to the underlying long field.
