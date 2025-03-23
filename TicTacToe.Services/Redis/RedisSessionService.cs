using StackExchange.Redis;
using System.Text.Json;

namespace TicTacToe.Services.Redis;


public class RedisSessionService
{
    private readonly IDatabase _database;

    public RedisSessionService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task SetSessionAsync(string sessionId, object sessionData, TimeSpan? expiry = null)
    {
        string json = JsonSerializer.Serialize(sessionData);
        await _database.StringSetAsync($"session:{sessionId}", json, expiry ?? TimeSpan.FromHours(1));
    }

    public async Task<T?> GetSessionAsync<T>(string sessionId)
    {
        string json = await _database.StringGetAsync($"session:{sessionId}");
        return json is not null ? JsonSerializer.Deserialize<T>(json) : default;
    }

    public async Task<bool> DeleteSessionAsync(string sessionId)
    {
        return await _database.KeyDeleteAsync($"session:{sessionId}");
    }
}