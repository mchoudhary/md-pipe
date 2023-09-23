using Systmtca.MDPipe.Framework.Core.Constants;
using Systmtca.MDPipe.Framework.Protocols.Websocket;

namespace Systmtca.MDPipe.Venues
{
    /// <summary>
    /// wss://localhost:9000/md-pipe/realtime/native
    /// wss://localhost:9000/md-pipe/realtime/normalized
    /// wss://localhost:9000/md-pipe/historical/native
    /// wss://localhost:9000/md-pipe/historical/normalized
    /// </summary>
    public abstract partial class VenueWebsocketFeed: IVenueWebsocketFeed
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly Action<string> _onEventCallbackHandler;

        protected abstract string FeedUrl { get; }

        public VenueWebsocketFeed(EventFormat eventFormat, Action<string> onEventCallback)
        {
            _onEventCallbackHandler = onEventCallback;

            ActiveSubsTrade = new Dictionary<string, dynamic>();
            ActiveSubsOrdBookL2Incr = new Dictionary<string, dynamic>();
            ActiveSubsTicker = new Dictionary<string, dynamic>();

            InitFormatters(eventFormat);
            InitSockets();
        }

        private void InitFormatters(EventFormat format)
        {
            InitTickerHandler(format);
            InitTradeHandler(format);
            InitOrdBookL2IncrHandler(format);
        }

        private void InitSockets()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            InitTickerSocket();
            InitTradeSocket();
            InitOrdBookL2IncrSocket();
        }

        private Lazy<WebsocketClient> ChannelSocketFactory(Action<string?> onVenueEventCallback)
        {
            return new Lazy<WebsocketClient>(() => new WebsocketClient(onEventCallback: onVenueEventCallback,
                                                                       cancellationTokenSource: _cancellationTokenSource));
        }

    }
}
