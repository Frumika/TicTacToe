using Backend.Domain.DTO;
using Backend.Domain.Enums;
using static Backend.Domain.Enums.FieldItem;
using static Backend.Domain.Enums.GameMode;

namespace Backend.Domain.Models.Game;

public class GameModel
{
    private Board _board;
    private FieldItem _currentItem = Cross;
    private Winner _winner = Winner.Undefined;

    private Bot? _bot = null;


    public FieldItem CurrentItem => _currentItem;

    public Winner Winner => _winner;
    public Field[][] Board => _board.BoardState;
    public GameMode GameMode { get; }
    public BotMode BotMode { get; }


    public GameModel(int size, GameMode gameMode, BotMode botMode)
    {
        _board = new Board(size);

        GameMode = gameMode;
        BotMode = botMode;

        if (GameMode == PvE) _bot = new Bot(_currentItem == Cross ? Zero : Cross, BotMode);
    }

    public bool MakeMove(int row, int column)
    {
        bool? playerMove = null;
        bool? botMove = null;

        // ХОД ИГРОКА: Игрок ходит и в PvP и в PvE режиме
        playerMove = IsSuccessfulMove(row, column);

        // ХОД БОТА
        if (GameMode == PvE && playerMove != false && Winner == Winner.Undefined)
        {
            var position = _bot!.Move(_board);
            botMove = IsSuccessfulMove(position.row, position.column);
        }

        return (playerMove ?? false) && (botMove ?? true);
    }

    public void LoadState(SessionState state)
    {
        _winner = state.Winner;
        _currentItem = state.CurrentItem;
        _board.LoadState(state);
    }

    public void ResetGame()
    {
        _board.ResetBoard();
        _currentItem = Cross;
        _winner = Winner.Undefined;
    }

    public void PrintBoard() => _board.PrintBoard();

    private bool IsSuccessfulMove(int row, int column)
    {
        if (_board.SetField(row, column, _currentItem))
        {
            if (_board.IsWinningField(row, column))
            {
                _winner = _currentItem switch
                {
                    Cross => Winner.Cross,
                    _ => Winner.Zero,
                };
            }
            else if (!_board.HasEmptyFields) _winner = Winner.Draw;

            SwitchTurn();

            return true;
        }

        return false;
    }

    private void SwitchTurn() => _currentItem = _currentItem == Cross ? Zero : Cross;
}