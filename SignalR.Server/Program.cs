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

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", builder =>
//    {
//        builder.AllowAnyHeader()
//               .AllowAnyMethod()
//               .AllowCredentials()
//               .WithOrigins("http://localhost:3000"); // 許可するオリジン
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//// ミドルウェアに追加
//app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

// エンドポイントを設定
app.MapHub<MyHub>("/myHub");

app.Run();
