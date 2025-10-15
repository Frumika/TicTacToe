namespace TicTacToe.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseApplicationPipeline(this WebApplication app, AppConfiguration config)
    {
        app.UseCors("AllowAllOrigins");
        app.MapControllers();

        return app;
    }
    
    public static void WarmupDatabase(this WebApplication app, AppConfiguration config)
    {
        using var scope = app.Services.CreateScope();
        
        // Todo: Нормально написать слой доступа к базе данных
        //var dbContext = scope.ServiceProvider.GetRequiredService<>();
        //dbContext.Users.Any();
    }
}