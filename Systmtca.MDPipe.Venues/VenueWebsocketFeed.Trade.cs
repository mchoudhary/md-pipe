using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Framework.Protocols.Websocket;

namespace Systmtca.MDPipe.Venues
{
    public abstract partial class VenueWebsocketFeed
    {
        protected Lazy<WebsocketClient> TradeSocket;
        protected readonly IDictionary<string, dynamic> ActiveSubsTrade;
        protected Func<string, Task<string>> FormatTradeHandler;

        private void InitTradeSocket()  
            => TradeSocket = ChannelSocketFactory(onVenueEventCallback: OnTradeEvent);

        private void InitTradeHandler(EventFormat format) 
            => FormatTradeHandler = format == EventFormat.Native ? FormatTradeNative : FormatTradeNormalized;

        private async void OnTradeEvent(string? eventData)
        {
            if (!string.IsNullOrEmpty(eventData))
                _onEventCallbackHandler.Invoke(FormatTradeHandler(eventData).Result);
        }

        public virtual async Task<string> FormatTradeNative(string eventData) => eventData;

        public abstract Task<string> FormatTradeNormalized(string message);
    }
}
