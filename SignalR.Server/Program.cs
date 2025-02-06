using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// SignalRをサービスに追加
builder.Services.AddSignalR();

// メッセージ送信サービスを登録
builder.Services.AddHostedService<MessageSenderService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// エンドポイントを設定
app.MapHub<MyHub>("/myHub");

//var hubContext = app.Services.GetRequiredService<IHubContext<MyHub>>();

//app.Lifetime.ApplicationStarted.Register(async () =>
//{
//    while (true)
//    {
//        await Task.Delay(5000); // 5秒ごとにメッセージ送信
//        string message = $"Server message at {DateTime.Now}";
//        await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
//        //Console.WriteLine($"[Server] Sent message: {message}");
//    }
//});

app.Run();
