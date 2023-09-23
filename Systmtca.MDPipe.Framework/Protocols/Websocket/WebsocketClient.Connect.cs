using System.Net.WebSockets;


namespace Systmtca.MDPipe.Framework.Protocols.Websocket
{
    public partial class WebsocketClient
    {
        public async Task ConnectAndListen(Uri uri)
        {
            while (!IsConnected)
            {
                try
                {
                    _websocket ??= new ClientWebSocket();

                    await _websocket.ConnectAsync(uri: uri, CancellationToken.None);
                    FeedUri = uri;
                    Console.WriteLine($"Connected to websocket feed at URL={FeedUri}");
                    IsConnected = true;

                    _ = Listen();
                }
                catch (Exception ex)
                {
                    await Reconnect();
                }
            }
        }

        public async Task Disconnect(string reason = "Server closed connection", Exception? exception = null)
        {
            try
            {
                _cancellationTokenSource?.Cancel();

                if (_websocket.State == WebSocketState.Open)
                {
                    WebSocketCloseStatus status = exception is null ? WebSocketCloseStatus.NormalClosure : WebSocketCloseStatus.InternalServerError;
                    await _websocket.CloseAsync(status, reason, CancellationToken.None);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _websocket?.Dispose();
                _cancellationTokenSource?.Dispose();
                IsConnected = false;
            }
        }

        private async Task Reconnect()
        {
            if (IsConnected)
                return;

            await Task.Delay(TimeSpan.FromSeconds(3)); // Wait before reconnecting

            Console.WriteLine("Reconnecting WebSocket...");
            await ConnectAndListen(uri: FeedUri);
        }
    }
}
