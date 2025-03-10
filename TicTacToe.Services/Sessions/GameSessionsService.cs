using System.Collections.Concurrent;
using TicTacToe.GameModel.Entity;
using TicTacToe.GameModel.Model;

namespace TicTacToe.Services;

public class GameSessionsService
{
    private ConcurrentDictionary<string, Session> _gameServices;

    public GameSessionsService()
    {
        _gameServices = new ConcurrentDictionary<string, Session>();
    }

    public bool CreateSession(string sessionId, string gameMode, string botMode)
    {
        var success = _gameServices.TryAdd(sessionId, new Session(gameMode, botMode));
        return success;
    }

    public Session GetOrCreateSession(string sessionId, string gameMode, string botMode)
    {
        var session = _gameServices.GetOrAdd(sessionId, _ => new Session(gameMode, botMode));
        return session;
    }

    public Session? GetSession(string sessionId) =>
        _gameServices.TryGetValue(sessionId, out var session) ? session : null;

    public bool RemoveSession(string sessionId) => _gameServices.TryRemove(sessionId, out var session) ? true : false;


    public bool ResetSession(string sessionId, string gameMode, string botMode)
    {
        var newSession = new Session(gameMode, botMode);
        _gameServices[sessionId] = newSession;
        
        return true;
    }
}