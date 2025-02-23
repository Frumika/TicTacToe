﻿namespace TicTacToe.Services;

using TicTacToe.GameModel.GameBoard;
using TicTacToe.GameModel.Model;
using TicTacToe.GameModel.Entity;

public class Session
{
    private GameModel _gameModel;

    private FieldItem _winner;
    private FieldItem _currentItem;

    private bool _hasEmptyFields;
    
    
    
    public Session()
    {
        _gameModel = new GameModel(3, GameMode.PvE, BotMode.Medium);
    }

    public bool SendRequest(int row, int column)
    {
        bool condition = _gameModel.MakeMove(row, column);
        _gameModel.PrintBoard();

        return condition;
    }

    public bool AcceptResponse()
    {
        return false;
    }
}