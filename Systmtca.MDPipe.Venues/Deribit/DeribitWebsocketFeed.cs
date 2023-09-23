using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues.Deribit
{
    /// <summary>
    /// Docs: https://docs.deribit.com/v2/#subscriptions
    /// </summary>
    public class DeribitWebsocketFeed : VenueWebsocketFeed
    {
        protected override string FeedUrl => "wss://streams.deribit.com/ws/api/v2";

        // TODO: Add to mapping file
        private static readonly IDictionary<string, string> _channelMapping = new Dictionary<string, string>()
        {
            { EventChannel.ORDERBOOK_L2_INCR, "book" }, // docs: https://docs.deribit.com/#book-instrument_name-interval
            { EventChannel.TRADE, "trades" },             // docs: https://docs.deribit.com/#trades-instrument_name-interval
            { EventChannel.TICKER, "ticker" }              // docs: https://docs.deribit.com/#trades-instrument_name-interval
        };

        public DeribitWebsocketFeed(EventFormat format, Action<string> onEventCallback)
            : base(format, onEventCallback)
        {
        }

        public override Task<string> FormatTradeNormalized(string eventData) => throw new NotImplementedException();

        public override Task<string> FormatOrdBookL2IncrNormalized(string eventData) => throw new NotImplementedException();

        public override Task<string> FormatTickerNormalized(string message) => throw new NotImplementedException();

        protected override IDictionary<string, dynamic> MapSubscriptionRequest(List<SubscriptionRequest> newSubscriptions) 
            => DeribitMapper.MapSubscriptionRequest(newSubscriptions: newSubscriptions, channelMappings: _channelMapping);
    }
}
