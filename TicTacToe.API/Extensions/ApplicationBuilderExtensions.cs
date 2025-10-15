﻿using TicTacToe.DataAccess.Context;

namespace TicTacToe.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseApplicationPipeline(this WebApplication app)
    {
        app.UseCors("AllowAllOrigins");
        app.MapControllers();

        return app;
    }

    public static void WarmupDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        var any = dbContext.Users.Any();
    }
}