using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues.Huobi
{
    /// <summary>
    /// Docs: https://huobiapi.github.io/docs/spot/v1/en/#introduction-10
    /// </summary>
    public class HuobiWebsocketFeed : VenueWebsocketFeed
    {
        protected override string FeedUrl => "wss://api-aws.huobi.pro/ws";

        // TODO: Add to mapping file
        private static readonly IDictionary<string, string> _channelMapping = new Dictionary<string, string>()
        {
            { EventChannel.ORDERBOOK_L2_INCR, "depth.step0" }, // docs: https://huobiapi.github.io/docs/spot/v1/en/#market-depth
            { EventChannel.TRADE, "trade.detail" },             // docs: https://huobiapi.github.io/docs/spot/v1/en/#trade-detail
            { EventChannel.TICKER, "ticker" }              // docs: https://huobiapi.github.io/docs/spot/v1/en/#market-ticker
        };

        public HuobiWebsocketFeed(EventFormat format, Action<string> onEventCallback)
            : base(format, onEventCallback)
        {
        }

        public override Task<string> FormatTradeNormalized(string eventData) => throw new NotImplementedException();

        public override Task<string> FormatOrdBookL2IncrNormalized(string eventData) => throw new NotImplementedException();

        public override Task<string> FormatTickerNormalized(string message) => throw new NotImplementedException();

        protected override IDictionary<string, dynamic> MapSubscriptionRequest(List<SubscriptionRequest> newSubscriptions) 
            => HuobiMapper.MapSubscriptionRequest(newSubscriptions: newSubscriptions, channelMappings: _channelMapping);
    }
}
