using Backend.Domain.Models.Game;

namespace Backend.Application.Managers.Interfaces;

public interface IGameSessionsManager
{
    bool CreateSession(string sessionId, string gameMode, string botMode);
    bool ResetSession(string sessionId, string gameMode, string botMode);
    Session GetOrCreateSession(string sessionId, string gameMode, string botMode);
    Session? GetSession(string sessionId);
    bool RemoveSession(string sessionId);
}