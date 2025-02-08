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

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", builder =>
//    {
//        builder.AllowAnyHeader()
//               .AllowAnyMethod()
//               .AllowCredentials()
//               .WithOrigins("http://localhost:3000"); // ������I���W��
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//// �~�h���E�F�A�ɒǉ�
//app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

// �G���h�|�C���g��ݒ�
app.MapHub<MyHub>("/myHub");

app.Run();
