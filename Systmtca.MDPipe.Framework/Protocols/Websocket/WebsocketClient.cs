using System.Net.WebSockets;

namespace Systmtca.MDPipe.Framework.Protocols.Websocket
{
    public partial class WebsocketClient
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private ClientWebSocket? _websocket;
        private readonly Action<string?> _onEventCallbackHandler;

        protected Uri FeedUri;

        public bool IsConnected { get; private set; }

        public WebsocketClient(Action<string?> onEventCallback, CancellationTokenSource cancellationTokenSource)
        {
            _cancellationTokenSource = cancellationTokenSource;
            _onEventCallbackHandler = onEventCallback;
        }
    }
}
