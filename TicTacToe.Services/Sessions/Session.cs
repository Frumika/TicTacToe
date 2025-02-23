namespace TicTacToe.Services;

using TicTacToe.GameModel.GameBoard;
using TicTacToe.GameModel.Model;
using TicTacToe.GameModel.Entity;

public class Session
{
    private GameModel _gameModel;
    
    public Session()
    {
        _gameModel = new GameModel(3, GameMode.PvE, BotMode.Medium);
    }

    public bool SendRequest(int row, int column)
    {
        if (_gameModel.Winner == Winner.Undefined)
        {
            
            bool condition = _gameModel.MakeMove(row, column);
            _gameModel.PrintBoard();

            return condition;
        }

        return false;
    }

    public bool AcceptResponse()
    {
        return false;
    }
}