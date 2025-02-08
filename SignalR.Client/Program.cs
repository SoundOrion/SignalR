using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5051/myHub") // サーバーのURLを指定
            .Build();

        // 型安全なメソッド登録
        connection.On<string>(nameof(IHubClient.ReceiveMessage), async (message) =>
        {
            Console.WriteLine($"Received message from server: {message}");

            // 疑似的に1時間かかる処理（今回は1秒）
            await Task.Delay(TimeSpan.FromSeconds(1));

            string result = message.ToUpper();

            // サーバーへ結果を送信
            await connection.InvokeAsync(nameof(IHubServer.ReceiveResult), result);
            Console.WriteLine($"Sent result to server: {result}");
        });

        // 接続開始
        connection.Closed += async (error) =>
        {
            Console.WriteLine("Connection closed. Retrying in 5 seconds...");
            await Task.Delay(5000);
            try
            {
                await connection.StartAsync();
                Console.WriteLine("Reconnected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reconnection failed: {ex.Message}");
            }
        };

        // サーバーからのメッセージを待つ
        Console.ReadLine();
    }
}
