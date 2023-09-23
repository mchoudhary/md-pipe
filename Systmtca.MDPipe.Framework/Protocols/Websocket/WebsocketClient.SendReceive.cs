using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Text;
using Systmtca.MDPipe.Framework.Core;

namespace Systmtca.MDPipe.Framework.Protocols.Websocket
{
    public partial class WebsocketClient
    {
        private const int CHUNKSIZE = 1024 * 8;

        public async Task Send(dynamic message)
        {
            try
            {
                if (!IsConnected)
                    await ConnectAndListen(uri: FeedUri);

                byte[] jsonRequest = SpanJson.JsonSerializer.Generic.Utf8.Serialize(message);
                Console.WriteLine($"Sent message to venue: {message}");
                await _websocket.SendAsync(jsonRequest, WebSocketMessageType.Text, true, _cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                await Disconnect();
                await Reconnect();
            }
        }

        private async Task Listen()
        {
            Exception? causedException = null;
            // define buffer here and reuse, to avoid more allocation
            ArraySegment<byte> buffer = new(new byte[CHUNKSIZE]);

            try
            {
                while (IsConnected && !_cancellationTokenSource.IsCancellationRequested)
                {
                    WebSocketReceiveResult result;
                    byte[]? resultArrayWithTrailing = null;
                    int resultArraySize = 0;
                    bool isResultArrayCloned = false;
                    MemoryStream? memStream = null;

                    while (true)
                    {
                        result = await _websocket.ReceiveAsync(buffer, _cancellationTokenSource.Token);
                        byte[]? currentChunk = buffer.Array;
                        int currentChunkSize = result.Count;

                        bool isFirstChunk = resultArrayWithTrailing is null;

                        if (isFirstChunk)
                        {
                            // first chunk, use buffer as reference, do not allocate anything
                            resultArraySize += currentChunkSize;
                            resultArrayWithTrailing = currentChunk;
                            isResultArrayCloned = false;
                        }
                        else if (currentChunk is not null)
                        {
                            // received more chunks, lets merge them via memory stream
                            if (memStream is null)
                            {
                                // create memory stream and insert first chunk
                                memStream = new MemoryStream(resultArraySize);

                                if (resultArrayWithTrailing is not null)
                                    await memStream.WriteAsync(resultArrayWithTrailing, 0, resultArraySize);
                            }

                            // insert current chunk
                            await memStream.WriteAsync(currentChunk, buffer.Offset, currentChunkSize);
                        }

                        if (result.EndOfMessage)
                            break;

                        if (isResultArrayCloned)
                            continue;

                        // we got more chunks incoming, need to clone first chunk
                        resultArrayWithTrailing = resultArrayWithTrailing?.ToArray();
                        isResultArrayCloned = true;
                    }

                    memStream?.Seek(0, SeekOrigin.Begin);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string? message = null;

                        if (memStream is not null)
                        {
                            if (memStream.TryGetBuffer(out var messageBuffer))
                            {
                                message = Encoding.UTF8.GetString(messageBuffer.Array, messageBuffer.Offset, messageBuffer.Count);
                                OnMessageReceived(message);
                            }
                        }
                        else if (resultArrayWithTrailing is not null)
                        { 
                            message = Encoding.UTF8.GetString(resultArrayWithTrailing, 0, resultArraySize);
                            OnMessageReceived(message);
                        }
                    }
                    else if(result.MessageType == WebSocketMessageType.Binary)
                    {
                        byte[] decompressed;
                        string? message;

                        using (var compressedStream = new MemoryStream(resultArrayWithTrailing))
                        using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                        using (var resultStream = new MemoryStream())
                        {
                            await zipStream.CopyToAsync(resultStream);
                            decompressed = resultStream.ToArray();
                        }

                        message = Encoding.UTF8.GetString(decompressed);
                        OnMessageReceived(message);
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await Disconnect();
                        return;
                    }

                    memStream?.Dispose();
                };
            }
            catch (TaskCanceledException e)
            {
                // task was canceled, ignore
                causedException = e;
            }
            catch (OperationCanceledException e)
            {
                // operation was canceled, ignore
                causedException = e;
            }
            catch (ObjectDisposedException e)
            {
                // client was disposed, ignore
                causedException = e;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case TaskCanceledException:
                        Console.WriteLine("Task was canceled, ignore");
                        break;
                    case OperationCanceledException:
                        Console.WriteLine("Operation was canceled, ignore");
                        break;
                    case ObjectDisposedException:
                        Console.WriteLine("Client was disposed, ignore");
                        break;
                    case WebSocketException:
                        Console.WriteLine($"WebSocket Error: {ex.Message}");
                        await Disconnect();
                        await Reconnect();
                        break;
                }

                //logger.Error($"Error while listening to websocket stream, error: '{e.Message}'");
                causedException = ex;
            }
        }

        private async Task OnMessageReceived(string? data)
        {
            _ = Task.Run(() => _onEventCallbackHandler.Invoke(data));
        }
    }
}
