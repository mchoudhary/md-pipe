using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Venues.Binance;
using Systmtca.MDPipe.Venues.BinanceUS;
using Systmtca.MDPipe.Venues.Coinbase;
using Systmtca.MDPipe.Venues.Deribit;
using Systmtca.MDPipe.Venues.Huobi;

namespace Systmtca.MDPipe.Venues
{
    public class VenueWebsocketFeedFactory : IVenueWebsocketFeedFactory
    {
        public IVenueWebsocketFeed GetVenueWebsocketFeed(string venue, EventFormat format, Action<string> onEventCallback)
        {
            return venue switch
            {
                EventVenues.COINBASE =>  new CoinbaseWebsocketFeed(format: format, onEventCallback: onEventCallback),
                EventVenues.BINANCE => new BinanceWebsocketFeed(format: format, onEventCallback: onEventCallback),
                EventVenues.BINANCE_FUTURES => new BinanceFutWebsocketFeed(format: format, onEventCallback: onEventCallback),
                EventVenues.DERIBIT => new DeribitWebsocketFeed(format: format, onEventCallback: onEventCallback),
                EventVenues.HUOBI => new HuobiWebsocketFeed(format: format, onEventCallback: onEventCallback),
                _ => throw new Exception($"Venue '{venue}' is not currently available through MD-Pipe"),
            };
        }
    }
}
