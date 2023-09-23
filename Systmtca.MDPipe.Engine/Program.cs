using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Pipes;
using Systmtca.MDPipe.Venues;
using Systmtca.MDPipe.Venues.Core.Data;

IVenueWebsocketFeedFactory factory = new VenueWebsocketFeedFactory();
IEventsPipe pipe = new RealtimeEventsPipe(socketFactory: factory, onEventCallback: OnMessage);


static void OnMessage(string data)
{
    dynamic message = SpanJson.JsonSerializer.Generic.Utf16.Deserialize<dynamic>(data);
    // Process the message according to the specific channels and symbols subscribed

    //if (message != null)
    //{

    //    DateTime ts_venue = DataConverters.ParseDateTimeStr(message.time.ToString());
    //    DateTime ts_now = DateTime.UtcNow;
    //    double seconds = (ts_now - ts_venue).TotalMilliseconds;
    //    Console.WriteLine($"Received message: {seconds} milli");
    //}

    Console.WriteLine($"Received message: {message}");
}


await pipe.Subscribe(EventVenues.BINANCE, newSubscriptions: new(){ new SubscriptionRequest(symbol: "BTCUSDT",
                                                                              exchange: EventVenues.BINANCE,
                                                                              channel: EventChannel.ORDERBOOK_L2_INCR,
                                                                              tsSubmission: DateTime.UtcNow) });

await pipe.Subscribe(EventVenues.BINANCE_FUTURES, newSubscriptions: new(){ new SubscriptionRequest(symbol: "BTCUSDT",
                                                                              exchange: EventVenues.BINANCE_FUTURES,
                                                                              channel: EventChannel.TRADE,
                                                                              tsSubmission: DateTime.UtcNow) });

await pipe.Subscribe(EventVenues.BINANCE_FUTURES, newSubscriptions: new(){ new SubscriptionRequest(symbol: "ETHUSDT",
                                                                              exchange: EventVenues.BINANCE_FUTURES,
                                                                              channel: EventChannel.ORDERBOOK_L2_INCR,
                                                                              tsSubmission: DateTime.UtcNow) });

await pipe.Subscribe(EventVenues.BINANCE_FUTURES, newSubscriptions: new(){ new SubscriptionRequest(symbol: "ETHUSDT",
                                                                              exchange: EventVenues.BINANCE_FUTURES,
                                                                              channel: EventChannel.TRADE,
                                                                              tsSubmission: DateTime.UtcNow) });

//await pipe.Subscribe(EventVenues.COINBASE, newSubscriptions: new(){ new SubscriptionRequest(symbol: "ETH-BTC",
//                                                                              exchange: EventVenues.COINBASE,
//                                                                              channel: EventChannel.ORDERBOOK_L2_INCR,
//                                                                              tsSubmission: DateTime.UtcNow) });
//await pipe.Subscribe(EventVenues.COINBASE, newSubscriptions: new(){ new SubscriptionRequest(symbol: "SOL-USD",
//                                                                              exchange: EventVenues.COINBASE,
//                                                                              channel: EventChannel.ORDERBOOK_L2_INCR,
//                                                                              tsSubmission: DateTime.UtcNow) });

Console.WriteLine("Waiting for response");
Console.ReadLine();
