using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using StackExchange.Redis;
using TicTacToe.Services.Game;
using TicTacToe.Services.Redis;
using TicTacToe.DataAccess.Context;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GameSessionsService>();

// Настройка подключения к PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(connectionString));

// Настройка подключения к Redis
var redisHost = Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost";
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisHost));
builder.Services.AddScoped<RedisSessionService>();

// Разрешаем принимать запросы с любых портов
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

// Прогреваем базу данных
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

    // Применяем миграции (если есть)
    dbContext.Database.Migrate();

    // Отправляем запрос для прогрева базы данных
    dbContext.Users.Any();
}

app.UseCors("AllowAllOrigins");
app.MapControllers();
app.Run();