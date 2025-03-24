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

    public async Task SetSessionAsync(string sessionId, UserDto userDto, TimeSpan expiry)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");
        
        if (userDto is null) throw new ArgumentNullException(nameof(userDto));

        try
        {
            string json = JsonSerializer.Serialize(userDto);
            bool isSet = await _database.StringSetAsync($"session:{sessionId}", json, expiry);

            if (!isSet) throw new InvalidOperationException("Failed to set session for sessionId");
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException("An unexpected error occurred while setting the session", exception);
        }
    }

    public async Task<UserDto?> GetSessionAsync<T>(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            string? json = await _database.StringGetAsync($"session:{sessionId}");

            if (string.IsNullOrEmpty(json)) return null;
            return JsonSerializer.Deserialize<UserDto>(json);
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException("An unexpected error occurred while retrieving the session", exception);
        }
    }

    public async Task<bool> DeleteSessionAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            return await _database.KeyDeleteAsync($"session:{sessionId}");
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException("An unexpected error occurred when deleting the session", exception);
        }
    }
}