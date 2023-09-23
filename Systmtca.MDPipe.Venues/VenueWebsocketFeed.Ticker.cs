using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Framework.Protocols.Websocket;

namespace Systmtca.MDPipe.Venues
{
    public abstract partial class VenueWebsocketFeed
    {
        protected Lazy<WebsocketClient> TickerSocket;
        protected readonly IDictionary<string, dynamic> ActiveSubsTicker;
        protected Func<string, Task<string>> FormatTickerHandler;

        private void InitTickerSocket()
            => TickerSocket = ChannelSocketFactory(onVenueEventCallback: OnTickerEvent);

        private void InitTickerHandler(EventFormat format) 
            => FormatTickerHandler = format == EventFormat.Native ? FormatTickerNative : FormatTickerNormalized;

        private async void OnTickerEvent(string? eventData)
        {
            if (!string.IsNullOrEmpty(eventData))
                _onEventCallbackHandler.Invoke(FormatTickerHandler(eventData).Result);
        }

        public virtual async Task<string> FormatTickerNative(string eventData) => eventData;

        public abstract Task<string> FormatTickerNormalized(string message);
    }
}
