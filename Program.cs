using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
//  using Microsoft.AspNetCore.WebSockets.Client;

namespace SocketTest1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string[] strbtc = new string[1];
            strbtc[0] = "BTC-USD";
            //  strbtc[1] = "BTC-EUR";

            channel ch1 = new channel { name = "ticker", product_ids = strbtc };
            channel[] chArray = new channel[1];
            chArray[0] = ch1;
            Wshello wshello = new Wshello { type = "subscribe", channels = chArray };

            string message = JsonConvert.SerializeObject(wshello);


            ClientWebSocket websocket = new ClientWebSocket();
            string url = "wss://ws-feed.pro.coinbase.com";
            Console.WriteLine("Connecting to: " + url);
            await websocket.ConnectAsync(new Uri(url), CancellationToken.None);

            Console.WriteLine("Sending message: " + message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await websocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

            Console.WriteLine();
            Console.WriteLine("wait for a response");
            Console.WriteLine();


            while (true)
            {
                byte[] incomingData = new byte[1024];
                WebSocketReceiveResult result = await websocket.ReceiveAsync(new ArraySegment<byte>(incomingData), CancellationToken.None);

                if (result.CloseStatus.HasValue)
                {
                    Console.WriteLine("Closed; Status: " + result.CloseStatus + ", " + result.CloseStatusDescription);
                }
                else
                {
                    Console.WriteLine("Received message: " + Encoding.UTF8.GetString(incomingData, 0, result.Count));
                }

                Console.WriteLine("==========================================================");
                Thread.Sleep(1000);
            }


        }

    }
}
