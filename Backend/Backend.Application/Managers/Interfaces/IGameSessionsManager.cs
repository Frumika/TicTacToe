using Backend.Domain.Models.Game;

namespace Backend.Application.Managers.Interfaces;

public interface IGameSessionsManager
{
    Task<Session?> CreateSessionAsync(int? userId, string sessionId, string gameMode, string botMode);
    Task<Session?> GetSessionAsync(string sessionId);
    Task<bool> SetSessionAsync(string sessionId, Session session);
    Task<bool> ResetSessionAsync(int? userId, string sessionId, string gameMode, string botMode);
    Task<Session?> GetOrCreateSessionAsync(int? userId, string sessionId, string gameMode, string botMode);
    Task<bool> RemoveSessionAsync(string sessionId);
}