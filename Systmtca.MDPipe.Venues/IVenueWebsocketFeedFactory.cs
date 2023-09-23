using Systmtca.MDPipe.Framework.Core.Constants;

namespace Systmtca.MDPipe.Venues
{
    public interface IVenueWebsocketFeedFactory
    {
        IVenueWebsocketFeed GetVenueWebsocketFeed(string venue, EventFormat format, Action<string> onEventCallback);
    }
}
