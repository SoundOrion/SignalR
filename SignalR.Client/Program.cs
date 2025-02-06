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

        // サーバーからのメッセージを受信
        connection.On<string>("ReceiveMessage", async (message) =>
        {
            Console.WriteLine($"Received message from server: {message}");

            // 何らかの処理 (例: メッセージを大文字に変換)
            // 長時間の処理を別スレッドで実行
            string result = await Task.Run(async () =>
            {
                Console.WriteLine("Starting long processing...");

                // 疑似的に1時間かかる処理
                await Task.Delay(TimeSpan.FromHours(1));

                // 処理完了後の結果
                return message.ToUpper();
            });

            // サーバーへ結果を送信
            await connection.InvokeAsync("ReceiveResult", result);
            Console.WriteLine($"Sent result to server: {result}");
        });

        // 接続開始
        await connection.StartAsync();
        Console.WriteLine("Connected to SignalR server.");

        // サーバーからのメッセージを待つ
        Console.ReadLine();
    }
}
