using TicTacToe.Model.Core.Entity;
using TicTacToe.Model.Core.GameBoard;

namespace TicTacToe.Model.Core.Complete;

public class GameModel
{
    private Board _board;
    private GameMode _gameMode;
    
    private IEntity?[] _entities = null;

    public GameModel(int size, GameMode gameMode)
    {
        _board = new Board(size);
        _gameMode = gameMode;

        if (_gameMode == GameMode.FaceToFace)
        {
            _entities = new[]
            {
                new Player("P2"),
                new Player("p3")
            };
        }
        
        if (_gameMode == GameMode.Online)
        {
            _entities = new[]
            {
                new Player("P2"),
            };
        }
    }
    
}