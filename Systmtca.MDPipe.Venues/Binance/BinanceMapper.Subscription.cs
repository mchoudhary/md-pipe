using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues.BinanceUS
{
    internal static partial class BinanceMapper
    {
        public static IDictionary<string, dynamic> MapSubscriptionRequest(List<SubscriptionRequest> newSubscriptions, IDictionary<string, string> channelMappings, string method = "SUBSCRIBE")
        {
            IDictionary<string, dynamic> mappedSubs = new Dictionary<string, dynamic>();

            foreach (var subscription in newSubscriptions)
            {
                string id = subscription.Id;
                string venChannel = channelMappings[subscription.Channel];

                if (subscription.Channel == EventChannel.TICKER)
                    venChannel = $"{venChannel}_{(string.IsNullOrWhiteSpace(subscription.Interval) ? "1s" : subscription.Interval)}";

                mappedSubs[id] = new
                {
                    Id = id,
                    subscription.Channel,
                    SubscriptionMessage = new
                    {
                        method,
                        id = DateTime.UtcNow.Ticks,
                        @params = new[] { $"{subscription.Symbol}@{venChannel}".ToLower() }, // has to be btcusd@trade due to case-sensitivity
                    }
                };
            }

            return mappedSubs;
        }
    }
}
