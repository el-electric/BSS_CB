using BatteryChangeCharger.BatteryChange_Charger.Controller;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EL_DC_Charger.ocpp.ver16.comm;
using BatteryChangeCharger.Applications;
using System.Diagnostics;
using RestSharp.Extensions;

namespace BatteryChangeCharger.OCPP
{
    public class OCPP_Comm_Manager
    {
        private ClientWebSocket webSocket = null;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Timer connectionCheckTimer;
        private string url;

        public OCPP_Comm_Manager()
        {
            // URL 설정 및 초기 연결 시도
            url = CsUtil.IniReadValue(System.Windows.Forms.Application.StartupPath + @"\web_socet_url.ini", "web_socet_url", "url", "wss://dev.wev-charger.com:12200/ws/NYJ-TEST0001");
            ConnectAsync(url);

            // 연결 상태 확인 타이머 설정
            connectionCheckTimer = new Timer(CheckConnectionStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        private async void CheckConnectionStatus(object state)
        {
            if (webSocket.State == WebSocketState.Closed || webSocket.State == WebSocketState.Aborted)
            {
                // 재접속 시도
                Console.WriteLine("Attempting to reconnect...");
                await ReconnectAsync(url);
            }
        }

        public async Task ConnectAsync(string uri)
        {
            webSocket = new ClientWebSocket();
            webSocket.Options.SetRequestHeader("Sec-WebSocket-Protocol", "ocpp1.6");
            webSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(5);
            webSocket.Options.SetBuffer(5000, 5000);

            try
            {
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                if (webSocket.State == WebSocketState.Open)
                {
                    _ = ListenForMessagesAsync(cts.Token);
                }
                else
                {
                    Console.WriteLine("Connection error.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
            }
        }

        public async Task SendMessageAsync(string message)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                var messageBuffer = Encoding.UTF8.GetBytes(message);
                var segment = new ArraySegment<byte>(messageBuffer);

                try
                {
                    await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                    Console.WriteLine("Message sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Send message failed: {ex.Message}");
                }
            }
        }

        public async Task<string> ReceiveMessageAsync()
        {
            try
            {
                var buffer = new byte[1024];
                WebSocketReceiveResult result;
                do
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await CloseAsync();
                        return null;
                    }
                } while (!result.EndOfMessage);

                return Encoding.UTF8.GetString(buffer, 0, result.Count);
            }
            catch (WebSocketException)
            {
                Console.WriteLine("WebSocket connection problem. Attempting to reconnect...");
                await ReconnectAsync(url);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReceiveMessageAsync exception: {ex.Message}");
                return null;
            }
        }

        public async Task CloseAsync()
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                Console.WriteLine("WebSocket closed.");
            }
        }

        public async Task ListenForMessagesAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested && webSocket.State == WebSocketState.Open)
                {
                    string message = await ReceiveMessageAsync();
                    if (message != null)
                    {
                        Console.WriteLine($"Received message: {message}");
                        // 메시지 처리 로직
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Message listening canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ListenForMessagesAsync exception: {ex.Message}");
            }
        }

        public async Task ReconnectAsync(string uri)
        {
            int retryIntervalSeconds = 5;
            int maxRetryCount = 5;
            int retryCount = 0;

            while (retryCount < maxRetryCount && (webSocket == null || webSocket.State != WebSocketState.Open))
            {
                retryCount++;
                try
                {
                    await ConnectAsync(uri);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Reconnect attempt {retryCount} failed: {ex.Message}");
                    await Task.Delay(retryIntervalSeconds * 1000);
                }
            }

            if (webSocket.State == WebSocketState.Open)
            {
                Console.WriteLine("Reconnected to WebSocket server.");
            }
            else
            {
                Console.WriteLine("Failed to reconnect to WebSocket server.");
            }
        }
    }
}

