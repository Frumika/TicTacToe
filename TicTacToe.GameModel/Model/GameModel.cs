using TicTacToe.GameModel.Entity;
using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Model;

public class GameModel
{
    private GameBoard.Board _board; // Реализует логику доски (Проверка полей, поиск победителя, и т.д)
    private GameMode _gameMode; // Указывает на текущий режим игры (PvP или PvE)

    private IEntity[] _entities; // Содержит игровые сущности Player или Bot
    private IEntity _currentEntity; // Указывает на текущего игрока


    public GameModel(int size, GameMode gameMode)
    {
        _board = new Board(size);
        _gameMode = gameMode;

        InitializeEntities();
    }


    public bool MakeMove(int row, int column)
    {
        if (_board.IsFieldEmpty(row, column))
        {
            _board.SetField(row, column, _currentEntity.Item);
            SwitchTurn();
            return true;
        }

        return false;
    }


    private void InitializeEntities()
    {
        if (_gameMode == GameMode.FtF || _gameMode == GameMode.PvP)
        {
            _entities = new IEntity[]
            {
                new Player("Player_1", Cross),
                new Player("Player_2", Zero)
            };
        }

        else if (_gameMode == GameMode.PvE)
        {
            _entities = new IEntity[]
            {
                new Player("Player", Cross),
                new Bot("Computer", Zero)
            };
        }

        _currentEntity = _entities[0];
    }

    private void SwitchTurn()
    {
        _currentEntity = _currentEntity == _entities[0] ? _entities[1] : _entities[0];
    }
}