namespace TicTacToe.Services;

using TicTacToe.GameModel.GameBoard;
using TicTacToe.GameModel.Model;
using TicTacToe.GameModel.Entity;

public class GameStateDto
{
    public Field[][] Board { get; set; }
    public Winner Winner { get; set; }
}

public class Session
{
    private GameModel _gameModel = null;

    public Session()
    {
        _gameModel = new GameModel(3, GameMode.PvE, BotMode.Medium);
    }

    public Session(GameMode gameMode)
    {
        _gameModel = new GameModel(3, gameMode, BotMode.Medium);
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

    public GameStateDto AcceptResponse()
    {
        return new GameStateDto()
        {
            Board = _gameModel.Board,
            Winner = _gameModel.Winner
        };
    }

    public void Reset() => _gameModel?.ResetGame();

    public void ApplySetting(string gameMode)
    {
        GameMode mode = gameMode switch
        {
            "PvP" => GameMode.PvP,
            _ => GameMode.PvE
        };

        _gameModel = new GameModel(3, mode, BotMode.Medium);
    }
}