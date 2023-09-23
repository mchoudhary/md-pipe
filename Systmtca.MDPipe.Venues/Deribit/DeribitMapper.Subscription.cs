using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues.Deribit
{
    internal static partial class DeribitMapper
    {
        public static IDictionary<string, dynamic> MapSubscriptionRequest(List<SubscriptionRequest> newSubscriptions, IDictionary<string, string> channelMappings, string method = "public/subscribe")
        {
            IDictionary<string, dynamic> mappedSubs = new Dictionary<string, dynamic>();

            foreach (var subscription in newSubscriptions)
            {
                string id = subscription.Id;
                string venChannel = channelMappings[subscription.Channel];

                mappedSubs[id] = new
                {
                    Id = id,
                    subscription.Channel,
                    SubscriptionMessage = new
                    {
                        jsonrpc = "2.0",
                        method,
                        @params = new[] { $"{venChannel}.{subscription.Symbol}.raw" }, // format: book.BTC-PERPETUAL.raw
                        id = DateTime.UtcNow.Millisecond
                    }
                };
            }

            return mappedSubs;
        }
    }
}
