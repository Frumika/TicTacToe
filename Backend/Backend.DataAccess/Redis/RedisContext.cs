using StackExchange.Redis;

namespace Backend.DataAccess.Redis;

public class RedisContext : IRedisContext
{
    private readonly ConnectionMultiplexer _gameConnection;
    private readonly ConnectionMultiplexer _userConnection;

    public IDatabase GameSessions => _gameConnection.GetDatabase();
    public IDatabase UserSessions => _userConnection.GetDatabase();

    public RedisContext(ConnectionMultiplexer gameConnection, ConnectionMultiplexer userConnection)
    {
        _gameConnection = gameConnection;
        _userConnection = userConnection;
    }
}