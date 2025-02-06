using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// SignalR���T�[�r�X�ɒǉ�
builder.Services.AddSignalR();

// ���b�Z�[�W���M�T�[�r�X��o�^
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

// �G���h�|�C���g��ݒ�
app.MapHub<MyHub>("/myHub");

//var hubContext = app.Services.GetRequiredService<IHubContext<MyHub>>();

//app.Lifetime.ApplicationStarted.Register(async () =>
//{
//    while (true)
//    {
//        await Task.Delay(5000); // 5�b���ƂɃ��b�Z�[�W���M
//        string message = $"Server message at {DateTime.Now}";
//        await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
//        //Console.WriteLine($"[Server] Sent message: {message}");
//    }
//});

app.Run();
