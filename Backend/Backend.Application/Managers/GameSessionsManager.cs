using System.Collections.Concurrent;
using Backend.Application.Managers.Interfaces;
using Backend.Domain.Models.Game;

namespace Backend.Application.Managers;

public class GameSessionsManager : IGameSessionsManager
{
    private ConcurrentDictionary<string, Session> _sessions = new();

    public bool CreateSession(string sessionId, string gameMode, string botMode)
    {
        var success = _sessions.TryAdd(sessionId, new Session(gameMode, botMode));
        return success;
    }

    public Session GetOrCreateSession(string sessionId, string gameMode, string botMode)
    {
        var session = _sessions.GetOrAdd(sessionId, _ => new Session(gameMode, botMode));
        return session;
    }

    public Session? GetSession(string sessionId) => _sessions.GetValueOrDefault(sessionId);

    public bool RemoveSession(string sessionId) => _sessions.TryRemove(sessionId, out _);

    public bool ResetSession(string sessionId, string gameMode, string botMode)
    {
        var session = GetSession(sessionId);
        if (session is null) return false;

        session.Reset(gameMode, botMode);
        return true;
    }
}