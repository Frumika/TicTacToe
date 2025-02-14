using TicTacToe.GameModel.Entity;
using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Model;

public class GameModel
{
    private GameBoard.Board _board;
    private GameMode _gameMode;

    private IEntity?[] _entities = null;

    public GameModel(int size, GameMode gameMode)
    {
        _board = new Board(size);
        _gameMode = gameMode;
    }
    
}