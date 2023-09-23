using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Framework.Protocols.Websocket;

namespace Systmtca.MDPipe.Venues
{
    public abstract partial class VenueWebsocketFeed
    {
        protected Lazy<WebsocketClient> OrderBookL2Socket;
        protected readonly IDictionary<string, dynamic> ActiveSubsOrdBookL2Incr;
        protected Func<string, Task<string>> FormatOrdBookL2IncrHandler;

        private void InitOrdBookL2IncrSocket()
            => OrderBookL2Socket = ChannelSocketFactory(onVenueEventCallback: OnOrdBookL2IncrEvent);

        private void InitOrdBookL2IncrHandler(EventFormat format) 
            => FormatOrdBookL2IncrHandler = format == EventFormat.Native ? FormatOrdBookL2IncrNative : FormatOrdBookL2IncrNormalized;

        private async void OnOrdBookL2IncrEvent(string? eventData)
        {
            if (!string.IsNullOrEmpty(eventData))
                _onEventCallbackHandler.Invoke(FormatOrdBookL2IncrHandler(eventData).Result);
        }

        public virtual async Task<string> FormatOrdBookL2IncrNative(string eventData) => eventData;

        public abstract Task<string> FormatOrdBookL2IncrNormalized(string eventData);
    }
}
