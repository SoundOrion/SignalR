using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

public class MyHub : Hub<IHubClient>, IHubServer
{
    // クライアントから送信された処理結果を受信
    public async Task ReceiveResult(string result)
    {
        Console.WriteLine($"[Server] Received result from client: {result}");
    }
}
