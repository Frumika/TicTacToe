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
    private GameModel _gameModel;

    public Session()
    {
        _gameModel = new GameModel(3, GameMode.PvE, BotMode.Medium);
    }

    public Session(string gameMode, string botMode)
    {
        GameMode gMode = gameMode switch
        {
            "PvP" => GameMode.PvP,
            _ => GameMode.PvE
        };

        BotMode bMode = botMode switch
        {
            "Easy" => BotMode.Easy,
            "Medium" => BotMode.Medium,
            "Hard" => BotMode.Hard,
            _ => BotMode.Medium
        };
        
        _gameModel = new GameModel(3, gMode, bMode);
    }

    public bool MakeMove(int row, int column)
    {
        if (_gameModel.Winner == Winner.Undefined)
        {
            bool condition = _gameModel.MakeMove(row, column);
            return condition;
        }
        return false;
    }
    
    public GameStateDto GameState()
    {
        return new GameStateDto()
        {
            Board = _gameModel.Board,
            Winner = _gameModel.Winner
        };
    }
}