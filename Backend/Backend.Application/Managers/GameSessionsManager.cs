using System.Text.Json;
using StackExchange.Redis;
using Backend.Application.Managers.Interfaces;
using Backend.DataAccess.Redis;
using Backend.Domain.Models.Game;

namespace Backend.Application.Managers;

public class GameSessionsManager : IGameSessionsManager
{
    private readonly IDatabase _database;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public GameSessionsManager(IRedisContext redis)
    {
        _database = redis.GameSessions;
    }

    public async Task<Session?> CreateSessionAsync(string sessionId, string gameMode, string botMode)
    {
        var session = new Session(gameMode, botMode);
        string json = JsonSerializer.Serialize(session, JsonOptions);

        bool isCreated = await _database.StringSetAsync(sessionId, json, when: When.NotExists);
        return isCreated ? session : null;
    }

    public async Task<bool> SetSessionAsync(string sessionId, Session session)
    {
        string json = JsonSerializer.Serialize(session, JsonOptions);
        return await _database.StringSetAsync(sessionId, json, when: When.Exists);
    }

    public async Task<Session?> GetSessionAsync(string sessionId)
    {
        string? json = await _database.StringGetAsync(sessionId);
        
        return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<Session>(json, JsonOptions);
    }

    public async Task<bool> ResetSessionAsync(string sessionId, string gameMode, string botMode)
    {
        string? json = await _database.StringGetAsync(sessionId);
        if (string.IsNullOrEmpty(json)) return false;

        var session = JsonSerializer.Deserialize<Session>(json, JsonOptions);
        if (session is null) return false;
        session.Reset(gameMode, botMode);
        
        return await SetSessionAsync(sessionId, session);
    }

    public async Task<Session?> GetOrCreateSessionAsync(string sessionId, string gameMode, string botMode)
    {
        var createdSession = await CreateSessionAsync(sessionId, gameMode, botMode);
        if (createdSession is not null) return createdSession;

        return await GetSessionAsync(sessionId);
    }

    public async Task<bool> RemoveSessionAsync(string sessionId)
    {
        bool isSessionExists = await _database.KeyExistsAsync(sessionId);
        return isSessionExists && await _database.KeyDeleteAsync(sessionId);
    }
}