using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues.Huobi
{
    internal static partial class HuobiMapper
    {
        public static IDictionary<string, dynamic> MapSubscriptionRequest(List<SubscriptionRequest> newSubscriptions, IDictionary<string, string> channelMappings)
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
                        sub = $"market.{subscription.Symbol}.{venChannel}".ToLower(), // format: "sub": "market.btcusdt.depth.step0"
                        id = $"id{DateTime.UtcNow.Ticks}"
                    }
                };
            }

            return mappedSubs;
        }
    }
}
