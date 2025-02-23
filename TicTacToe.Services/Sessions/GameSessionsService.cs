﻿using System.Collections.Concurrent;

namespace TicTacToe.Services;

public class GameSessionsService
{
    private ConcurrentDictionary<string, Session> _gameServices;

    public GameSessionsService()
    {
        _gameServices = new ConcurrentDictionary<string, Session>();
    }
    
    public Session GetOrCreateSession(string sessionId) => _gameServices.GetOrAdd(sessionId, _ => new Session());
}