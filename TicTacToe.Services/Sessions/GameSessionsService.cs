using System.Collections.Concurrent;

namespace TicTacToe.Services;

public class GameSessionsService
{
    private ConcurrentDictionary<string, Session> _gameServices;

    public GameSessionsService()
    {
        _gameServices = new ConcurrentDictionary<string, Session>();
    }

    public Session GetOrCreateSession(string sessionId) => _gameServices.GetOrAdd(sessionId, _ => new Session());

    public Session? GetSession(string sessionId) =>
        _gameServices.TryGetValue(sessionId, out var session) ? session : null;

    public bool RemoveSession(string sessionId) => _gameServices.TryRemove(sessionId, out var session) ? true : false;


    public bool ResetSession(string sessionId)
    {
        Session? session = GetSession(sessionId);

        session?.Reset();

        return session is not null ? true : false;
    }
}