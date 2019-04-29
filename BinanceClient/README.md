# Binance-DEX-C#-SDK

Fully featured SDK for Binance DEX. Use it, enhance it further, make some profit in crypto, spread the word, support mass adoption. :)

---

[**Summary**](README.md)  | [Getting Started](gettingStarted.md) | [Advanced Functions](advancedFunctions.md) 

---

What this SDK offers:

- **wallet** (account) functions
	- **create** random wallet (mnemonic, private key, address)
	- **restore** wallet (from mnemonic)
	- **sign** message
- **Broadcast** transactions to blockchain through https
	- Full coverage - **new** order, **cancel** order, **send** coins (also multisend), **freeze** coin, **unfreeze** coin, **vote**	
- **HTTP client** with full coverage of endpoints.
  - Providing **market data** through strongly typed classes
  -  providing blockchain state and environment information
  - provides automatic **request throttling** set to specific call/sec or call/minute rates per endpoint according to the api rate limits to avoid ip ban on heavy usage. (can be disabled)
- **Websocket client** with full coverage of streams
  - provides **realtime market data** and **account** update events
- **Node RPC websocket client** with full coverage of transaction broadcasts, streams and informational calls
  - provides all 3 types of methods for quickly **submitting transactions directly to a node's RPC endpoint** through a websocket channel
  - provides the ability to **subscribe to blockchain event streams** (amino translation not included)
  - provides the ability to make **queries**, call **informational endpoints** on the node (amino translation not included)
- Binance DEX specific trading utilities
  - **Realtime local orderbooks**
    - Keeps a local representation of orderbooks, creating and maintaining them automatically using the http and the websocket client
    - Provides events on order book updates
    - designed to be usable in multithreaded trading applications
  - **Orderbook skimmer**
    - Binance DEX performs periodic auction matching per block,[ resulting agressively priced, bigger orders on low liquidity markets to receive sub-optimal average price for fills.](https://docs.binance.org/match.html#what-exactly-is-binance-dex-matching-logic) This class tries to prevent this by trying to get only the top of the book for us in every block with the best possible matching price, until we accumulate/distribute the whole amount we originally wanted.
    - The class uses realtime local orderbook and the websocket client's order stream to achieve it's goal
- One **client wrapper class** that can be used to handle almost all trading related functions the easiest way possible
  - **Intuitive usage**, setting up the wallet/client and sending out an order takes literally 2 lines of code
  - For broadcast transactions, it provides **3 alternative methods for ensuring proper sequence number** for the transaction replay protection  - from highest speed to highest resiliency (default)
  - the class can be safely used also in **multithreaded environments**
 ---
