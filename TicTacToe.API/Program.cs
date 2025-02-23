using System.Text.Json.Serialization;
using TicTacToe.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GameSessionsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var app = builder.Build();

app.UseCors("AllowAllOrigins");
app.MapControllers();
app.Run();