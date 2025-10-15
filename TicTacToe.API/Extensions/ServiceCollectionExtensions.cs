﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TicTacToe.DataAccess.Context;

namespace TicTacToe.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, AppConfiguration config)
    {
        services
            .ConnectPostgres(config)
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
        // services.AddScoped<IIdentityService, IdentityService>();
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