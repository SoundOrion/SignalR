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
        await connection.StartAsync();
        Console.WriteLine("Connected to SignalR server.");

        // サーバーからのメッセージを待つ
        Console.ReadLine();
    }
}
