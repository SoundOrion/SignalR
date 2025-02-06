using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

public class MyHub : Hub
{
    // クライアントにメッセージを送信
    //public async Task SendMessageToClient(string message)
    //{
    //    Console.WriteLine($"Sending message to client: {message}");
    //    await Clients.All.SendAsync("ReceiveMessage", message);
    //}

    // クライアントからの結果を受け取る
    public async Task ReceiveResult(string result)
    {
        Console.WriteLine($"Received result from client: {result}");
    }
}
