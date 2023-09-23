using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues
{
    public interface IVenueWebsocketFeed
    {
        public Task Subscribe(List<SubscriptionRequest> newSubscriptions);

        // Trade Formatters
        public Task<string> FormatTradeNative(string eventData);
        public Task<string> FormatTradeNormalized(string message);

        // Order Book L2 Formatters
        public Task<string> FormatOrdBookL2IncrNative(string eventData);
        public Task<string> FormatOrdBookL2IncrNormalized(string eventData);

        // Ticker Formatters
        public Task<string> FormatTickerNative(string eventData);
        public Task<string> FormatTickerNormalized(string message);
    }
}
