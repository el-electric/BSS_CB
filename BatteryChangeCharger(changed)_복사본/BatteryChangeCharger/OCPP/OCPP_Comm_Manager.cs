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
        CancellationTokenSource cts = new CancellationTokenSource();
        Timer connectionCheckTimer;
        bool isStop = false;
        string url;
        public OCPP_Comm_Manager()
        {
            url = CsUtil.IniReadValue(System.Windows.Forms.Application.StartupPath + @"\web_socet_url.ini", "web_socet_url", "url", "wss://dev.wev-charger.com:12200/ws/NYJ-TEST0001");
            ConnectAsync(url);
            connectionCheckTimer = new Timer(CheckConnectionStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        }
        private async void CheckConnectionStatus(object state)
        {
            if (webSocket.State == WebSocketState.Closed || webSocket.State == WebSocketState.Aborted)
            {
                // 재접속 시도
                Console.WriteLine("재접속");
                await ReconnectAsync(url);
            }
            if (webSocket.State == WebSocketState.Open)
            {
                for (int i = 0; i < MyApplication.getInstance().oCPP_Comm_SendMgr.list_packet.Count; i++)
                {
                    SendMessageAsync(MyApplication.getInstance().oCPP_Comm_SendMgr.list_packet[i].mPacket.ToString());
                }
            }
        }

        public async Task ConnectAsync(string uri)
        {
            webSocket = new ClientWebSocket();

            webSocket.Options.SetRequestHeader("Sec-WebSocket-Protocol", "ocpp1.6");
            webSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(5);
            webSocket.Options.SetBuffer(5000, 5000);

            await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);

            switch (webSocket.State)
            {
                case WebSocketState.Open:
                    _ = ListenForMessagesAsync(cts.Token);
                    break;
                default:
                    Console.WriteLine("연결 에러");
                    break;
            }

        }

        public async Task SendMessageAsync(string message)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                var messageBuffer = System.Text.Encoding.UTF8.GetBytes(message);
                var segment = new ArraySegment<byte>(messageBuffer);

                await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);

                if (webSocket.State == WebSocketState.Open)
                    Logger.d("＠Send Success＠ " + ": " + message);
                else
                    Logger.d("＠Send Failed＠ " + ": " + message);
            }
        }

        public async Task<string> ReceiveMessageAsync()
        {
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            return System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
        }

        public async Task CloseAsync()
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                Console.WriteLine("WebSocket closed!");
            }
        }

        public async Task ListenForMessagesAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {

                    string message = await ReceiveMessageAsync();
                    Console.WriteLine($"Received message: {message}");

                    MyApplication.getInstance().oCPP_Comm_SendMgr.ReceivedPacket(message);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Message listening canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in message listening: {ex.Message}");
            }
        }


        public async Task ReconnectAsync(string uri)
        {
            // 재시도 횟수나 시간 간격은 필요에 따라 조정합니다.
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
