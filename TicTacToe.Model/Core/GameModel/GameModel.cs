using TicTacToe.Model.Core.GameBoard;

namespace TicTacToe.Model.Core.Complete;

public class GameModel
{
    private Board _board;
    private GameMode _gameMode;

    public GameModel(int size, GameMode gameMode)
    {
        _board = new Board(size);
        _gameMode = gameMode;
    }
    
    
}