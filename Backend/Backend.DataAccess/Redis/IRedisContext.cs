using StackExchange.Redis;

namespace Backend.DataAccess.Redis;

public interface IRedisContext
{
    IDatabase GameSessions { get; }
    IDatabase UserSessions { get; }    
}