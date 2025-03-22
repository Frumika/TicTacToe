using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TicTacToe.Services;
using TicTacToe.Data.Context;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GameSessionsService>();

// Настройка подключения к DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(connectionString));


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