using Backend.Domain.DTO;
using Backend.Domain.Enums;


namespace Backend.Domain.Models.Game;

public class Session
{
    private GameModel _gameModel;

    public Field[][] Board => _gameModel.Board;
    public Winner Winner => _gameModel.Winner;
    public FieldItem CurrentItem => _gameModel.CurrentItem;

    public int? UserId { get; set; }

    public Session()
    {
        _gameModel = new GameModel(3, GameMode.PvE, BotMode.Medium);
    }

    public Session(string gameMode, string botMode, int? userId = null)
    {
        var (gMode, bMode) = SettingsToObjects(gameMode, botMode);
        _gameModel = new GameModel(3, gMode, bMode);
        UserId = userId;
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

    public void Reset(string gameMode, string botMode)
    {
        var (gMode, bMode) = SettingsToObjects(gameMode, botMode);
        _gameModel = new GameModel(3, gMode, bMode);
    }

    public SessionState ToState()
    {
        return new SessionState
        {
            UserId = UserId,
            Board = Board,
            CurrentItem = _gameModel.CurrentItem,
            Winner = Winner,
            GameMode = _gameModel.GameMode,
            BotMode = _gameModel.BotMode
        };
    }

    public static SessionState ToState(Session session) => session.ToState();

    public static Session FromState(SessionState state)
    {
        var session = new Session(state.GameMode.ToString(), state.BotMode.ToString());
        session.UserId = state.UserId;
        session._gameModel.LoadState(state);

        return session;
    }

    private (GameMode, BotMode) SettingsToObjects(string gameMode, string botMode)
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

        return (gMode, bMode);
    }
}