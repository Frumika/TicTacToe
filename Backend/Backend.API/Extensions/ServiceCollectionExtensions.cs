using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Backend.Application.Managers;
using StackExchange.Redis;
using Backend.Application.Services;
using Backend.Application.Services.Interfaces;
using Backend.DataAccess.Postgres.Context;
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
        var redisHost = config.Configuration["RedisUsers:Host"];
        var redisPort = config.Configuration["RedisUsers:Port"];

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
        services.AddSingleton<GameSessionsManager>();
        services.AddSingleton<RedisSessionService>();
        
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IGameService, GameService>();
        
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