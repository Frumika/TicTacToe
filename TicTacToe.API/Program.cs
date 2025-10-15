using TicTacToe.API;
using TicTacToe.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
AppConfiguration configuration = new(builder.Configuration, builder.Environment);

builder.Services.AddApplicationServices(configuration);

var app = builder.Build();
app.UseApplicationPipeline();
app.WarmupDatabase();

app.Run();