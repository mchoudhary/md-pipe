using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Venues.Coinbase
{
    internal static partial class CoinbaseMapper
    {
        public static IDictionary<string, dynamic> MapSubscriptionRequest(List<SubscriptionRequest> newSubscriptions, IDictionary<string, string> channelMappings, string type = "subscribe")
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
                        type,
                        product_ids = new[] { subscription.Symbol.ToUpper() }, // format: BTC-USD
                        channels = new[] { venChannel } // format: level2,
                    }
                };
            }

            return mappedSubs;
        }
    }
}
