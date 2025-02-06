using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

public class MyHub : Hub<IHubClient>
{
    public async Task ReceiveResult(string result)
    {
        Console.WriteLine($"[Server] Received result from client: {result}");
    }
}
