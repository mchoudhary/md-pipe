# md-pipe

Subscription Requests:

Following shows examples of trade and order book update subs. Please note although I have shown trade and order book upd in separate examples, any combination of symbol-exchange-channel can be subscribed to at the same time:

```csharp

await pipe.Subscribe(EventVenues.BINANCE_FUTURES, newSubscriptions: new(){ new SubscriptionRequest(symbol: "BTCUSDT",
                                                                              exchange: EventVenues.BINANCE_FUTURES,
                                                                              channel: EventChannel.TRADE,
                                                                              tsSubmission: DateTime.UtcNow) });

await pipe.Subscribe(EventVenues.BINANCE_FUTURES, newSubscriptions: new(){ new SubscriptionRequest(symbol: "ETHUSDT",
                                                                              exchange: EventVenues.BINANCE_FUTURES,
                                                                              channel: EventChannel.TRADE,
                                                                              tsSubmission: DateTime.UtcNow) });
```

![image](https://github.com/mchoudhary/md-pipe/assets/2091102/ad9b5c7d-b5a0-41e3-be26-fb947d615cd0)

```csharp

await pipe.Subscribe(EventVenues.BINANCE_FUTURES, newSubscriptions: new(){ new SubscriptionRequest(symbol: "BTCUSDT",
                                                                              exchange: EventVenues.BINANCE_FUTURES,
                                                                              channel: EventChannel.ORDERBOOK_L2_INCR,
                                                                              tsSubmission: DateTime.UtcNow) });

await pipe.Subscribe(EventVenues.BINANCE_FUTURES, newSubscriptions: new(){ new SubscriptionRequest(symbol: "ETHUSDT",
                                                                              exchange: EventVenues.BINANCE_FUTURES,
                                                                              channel: EventChannel.ORDERBOOK_L2_INCR,
                                                                              tsSubmission: DateTime.UtcNow) });
```

![image](https://github.com/mchoudhary/md-pipe/assets/2091102/fe1133d7-922c-471c-a5d3-bb8c9c9dc4f2)
