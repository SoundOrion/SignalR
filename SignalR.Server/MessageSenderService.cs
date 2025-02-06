using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class MessageSenderService : BackgroundService
{
    private readonly IHubContext<MyHub> _hubContext;
    private readonly ILogger<MessageSenderService> _logger;

    public MessageSenderService(IHubContext<MyHub> hubContext, ILogger<MessageSenderService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("MessageSenderService is starting...");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(5000, stoppingToken); // 5秒ごとにメッセージ送信

            string message = $"Server message at {DateTime.Now}";
            _logger.LogInformation($"[Server] Sending message: {message}");

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message, stoppingToken);
        }
    }
}
