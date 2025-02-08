using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main(string[] args)
    {
        // ロガーの設定
        using var serviceProvider = new ServiceCollection()
            .AddLogging(configure => configure.AddConsole()) // コンソールログを有効化
            .BuildServiceProvider();

        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5051/myHub") // サーバーのURLを指定
            .ConfigureLogging(logging =>
            {
                logging.AddConsole(); // SignalR クライアントのログを出力
                logging.SetMinimumLevel(LogLevel.Information);
            })
            .Build();

        // 型安全なメソッド登録
        connection.On<string>(nameof(IHubClient.ReceiveMessage), async (message) =>
        {
            logger.LogInformation("Received message from server: {Message}", message);

            // 疑似的に1時間かかる処理（今回は1秒）
            await Task.Delay(TimeSpan.FromSeconds(1));

            string result = message.ToUpper();

            // サーバーへ結果を送信
            await connection.InvokeAsync(nameof(IHubServer.ReceiveResult), result);
            logger.LogInformation("Sent result to server: {Result}", result);
        });

        // 接続開始
        try
        {
            await connection.StartAsync();
            logger.LogInformation("Connected to SignalR server.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to connect to SignalR server.");
            return;
        }

        // サーバーからのメッセージを待つ
        logger.LogInformation("Waiting for messages. Press Enter to exit.");
        Console.ReadLine();
    }
}
