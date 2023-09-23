using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Venues.BinanceUS;
using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues.Binance
{
    /// <summary>
    /// Binance Global: https://github.com/binance/binance-spot-api-docs/blob/master/web-socket-streams.md#general-wss-information
    /// Binance US: https://github.com/binance-us/binance-us-api-docs/blob/master/web-socket-streams.md#general-wss-information
    /// </summary>
    public class BinanceWebsocketFeed : VenueWebsocketFeed
    {
        protected override string FeedUrl => "wss://stream.binance.com:9443/ws";

        // TODO: Add to mapping file
        private static readonly IDictionary<string, string> _channelMapping = new Dictionary<string, string>()
        {
            { EventChannel.ORDERBOOK_L2_INCR, "depth" },
            { EventChannel.TRADE, "trade" },
            { EventChannel.TICKER, "kline" }
        };

        public BinanceWebsocketFeed(EventFormat format, Action<string> onEventCallback)
            : base(format, onEventCallback)
        {
        }

        public override Task<string> FormatTradeNormalized(string eventData) => BinanceMapper.NormalizeTrade(eventData);

        public override Task<string> FormatOrdBookL2IncrNormalized(string eventData) => throw new NotImplementedException();

        public override Task<string> FormatTickerNormalized(string message) => throw new NotImplementedException();

        protected override IDictionary<string, dynamic> MapSubscriptionRequest(List<SubscriptionRequest> newSubscriptions)
            => BinanceMapper.MapSubscriptionRequest(newSubscriptions: newSubscriptions, channelMappings: _channelMapping);
    }
}
