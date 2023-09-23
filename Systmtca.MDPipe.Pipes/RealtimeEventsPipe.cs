using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Venues;
using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Pipes
{
    public class RealtimeEventsPipe: IEventsPipe
    {
        private IVenueWebsocketFeedFactory _venueSocketFeedFactory;
        private Action<string> _onEventCallbackHandler;

        public EventFormat Format { get; private set; }
        
        // venue: websocket feed
        public IDictionary<string, IVenueWebsocketFeed> VenueWebsocketFeeds { get; private set; }

        public RealtimeEventsPipe(IVenueWebsocketFeedFactory socketFactory, Action<string> onEventCallback)
        {
            _venueSocketFeedFactory = socketFactory;
            _onEventCallbackHandler = onEventCallback;

            Format = EventFormat.Native;
            VenueWebsocketFeeds = new Dictionary<string, IVenueWebsocketFeed>();
        }

        public async Task Subscribe(string venue, List<SubscriptionRequest> newSubscriptions)
        {
            IVenueWebsocketFeed socketFeed;

            if (!VenueWebsocketFeeds.ContainsKey(venue))
            {
                socketFeed = _venueSocketFeedFactory.GetVenueWebsocketFeed(venue: venue, format: Format, onEventCallback: OnEvent);
                VenueWebsocketFeeds[venue] = socketFeed;
            }
            else
                socketFeed = VenueWebsocketFeeds[venue];

            await socketFeed.Subscribe(newSubscriptions);
        }

        public async void OnEvent(string eventData)
        {
            _onEventCallbackHandler.Invoke(eventData);
        }
    }
}
