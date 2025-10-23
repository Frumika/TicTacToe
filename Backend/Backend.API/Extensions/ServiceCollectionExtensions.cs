using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using StackExchange.Redis;
using Backend.Application.Services;
using Backend.Application.Services.Interfaces;
using Backend.DataAccess.Context;
using Backend.Services.Game;
using Backend.Services.Redis;


namespace Backend.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, AppConfiguration config)
    {
        services
            .ConnectPostgres(config)
            .ConnectRedis(config)
            .AddCorsPolicy()
            .AddApplicationServices()
            .AddApplicationControllers();

        return services;
    }

    private static IServiceCollection ConnectPostgres(this IServiceCollection services, AppConfiguration config)
    {
        services.AddDbContext<UsersDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("UsersDatabase")));

        return services;
    }

    private static IServiceCollection ConnectRedis(this IServiceCollection services, AppConfiguration config)
    {
        var redisHost = config.Configuration["Redis:Host"];
        var redisPort = config.Configuration["Redis:Port"];

        var connectionString = $"{redisHost}:{redisPort}";
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionString));

        return services;
    }

    private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                policy => policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<GameSessionsService>();
        services.AddSingleton<RedisSessionService>();
        services.AddScoped<IIdentityService, IdentityService>();
        return services;
    }

    private static IServiceCollection AddApplicationControllers(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        return services;
    }
}