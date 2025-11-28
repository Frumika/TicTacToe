using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Backend.Domain.Enums;

namespace Backend.Domain.Models.Game;

public class Session
{
    [JsonProperty("GameModel")]
    private GameModel _gameModel;

    public Field[][] Board => _gameModel.Board;
    public Winner Winner => _gameModel.Winner;

    public Session()
    {
        _gameModel = new GameModel(3, GameMode.PvE, BotMode.Medium);
    }

    public Session(string gameMode, string botMode)
    {
        var (gMode, bMode) = SettingsToObjects(gameMode, botMode);
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

    public void Reset(string gameMode, string botMode)
    {
        var (gMode, bMode) = SettingsToObjects(gameMode, botMode);
        _gameModel = new GameModel(3, gMode, bMode);
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