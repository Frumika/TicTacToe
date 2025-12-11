using System.Text.Json;
using StackExchange.Redis;
using Backend.Application.DTO.Entities.Identity;
using Backend.DataAccess.Redis;


namespace Backend.Application.Managers;

public class UserSessionManager
{
    private readonly IDatabase _database;
    private readonly TimeSpan _expiryTime = TimeSpan.FromMinutes(15);

    public UserSessionManager(IRedisContext redis)
    {
        _database = redis.UserSessions;
    }

    public async Task SetSessionAsync(string sessionId, UserRedisDto userDto)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        if (userDto is null) throw new ArgumentNullException(nameof(userDto));

        try
        {
            string json = JsonSerializer.Serialize(userDto);
            bool isSet = await _database.StringSetAsync($"session:{sessionId}", json, _expiryTime);

            if (!isSet) throw new InvalidOperationException("Failed to set session for sessionId");
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException("An unexpected error occurred while setting the session", exception);
        }
    }

    public async Task<UserRedisDto?> GetSessionAsync<T>(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            string? json = await _database.StringGetAsync($"session:{sessionId}");

            if (string.IsNullOrEmpty(json)) return null;
            return JsonSerializer.Deserialize<UserRedisDto>(json);
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