using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues
{
    /// <summary>
    /// wss://localhost:9000/md-pipe/realtime/native
    /// wss://localhost:9000/md-pipe/realtime/normalized
    /// wss://localhost:9000/md-pipe/historical/native
    /// wss://localhost:9000/md-pipe/historical/normalized
    /// </summary>
    public abstract partial class VenueWebsocketFeed
    {
        protected abstract IDictionary<string, dynamic> MapSubscriptionRequest(List<SubscriptionRequest> newSubscriptions);

        /// <summary>
        /// Certain exchanges expect the subscription details be passed in with the URL during connection e.g. Gemini.
        /// This method can be overriden to enrich the URL with the subscription details.
        /// </summary>
        public virtual async Task Subscribe(List<SubscriptionRequest> newSubscriptions)
        {
            IDictionary<string, dynamic> mappedSubscriptions = MapSubscriptionRequest(newSubscriptions);

            foreach (KeyValuePair<string, dynamic> sub in mappedSubscriptions)
            {
                string channel = sub.Value.Channel;
                string id = sub.Key;
                dynamic subMessage = sub.Value.SubscriptionMessage;

                switch (channel)
                {
                    case EventChannel.TRADE:
                        if (!TradeSocket.Value.IsConnected)
                            await TradeSocket.Value.ConnectAndListen(uri: new Uri(FeedUrl));

                        TradeSocket.Value.Send(message: subMessage);
                        ActiveSubsTrade[id] = sub;
                        break;
                    case EventChannel.ORDERBOOK_L2_INCR:
                        if (!OrderBookL2Socket.Value.IsConnected)
                            await OrderBookL2Socket.Value.ConnectAndListen(uri: new Uri(FeedUrl));

                        OrderBookL2Socket.Value.Send(message: subMessage);
                        ActiveSubsOrdBookL2Incr[id] = sub;
                        break;
                    case EventChannel.TICKER:
                        if (!TickerSocket.Value.IsConnected)
                            await TickerSocket.Value.ConnectAndListen(uri: new Uri(FeedUrl));

                        TickerSocket.Value.Send(message: subMessage);
                        ActiveSubsTicker[id] = sub;
                        break;
                }
            }
        }
    }
}
