using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues.Coinbase
{
    /// <summary>
    /// Docs: https://docs.cloud.coinbase.com/exchange/docs/websocket-overview
    /// </summary>
    public class CoinbaseWebsocketFeed : VenueWebsocketFeed
    {
        protected override string FeedUrl => "wss://ws-feed.exchange.coinbase.com";

        // TODO: Add to mapping file
        private static readonly IDictionary<string, string> _channelMapping = new Dictionary<string, string>()
        {
            { EventChannel.ORDERBOOK_L2_INCR, "level2" },
            { EventChannel.TRADE, "matches" },
            { EventChannel.TICKER, "ticker" }
        };

        public CoinbaseWebsocketFeed(EventFormat format, Action<string> onEventCallback)
            : base(format, onEventCallback)
        {
        }

        public override Task<string> FormatTradeNormalized(string eventData) => CoinbaseMapper.NormalizeTrade(eventData);

        public override Task<string> FormatOrdBookL2IncrNormalized(string eventData) => throw new NotImplementedException();

        public override Task<string> FormatTickerNormalized(string message) => throw new NotImplementedException();

        protected override IDictionary<string, dynamic> MapSubscriptionRequest(List<SubscriptionRequest> newSubscriptions) 
            => CoinbaseMapper.MapSubscriptionRequest(newSubscriptions: newSubscriptions, channelMappings: _channelMapping);
    }
}
