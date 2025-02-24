using TicTacToe.GameModel.Entity;
using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Model;

public class GameModel
{
    /*------------| < Private Fields > |------------*/
    private Board _board;

    private FieldItem _currentItem = Cross;
    private Winner _winner = Winner.Undefined;

    private GameMode _gameMode;

    private Bot? _bot = null;
    /*----------------------------------------------*/


    /*--------------| < Properties > |--------------*/
    public FieldItem CurrentItem => _currentItem;
    
    public Winner Winner => _winner;

    public Field[][] Board => _board.BoardState;
    /*----------------------------------------------*/


    /*--------------| < Constructors > |------------*/
    public GameModel(int size, GameMode gameMode, BotMode botMode)
    {
        _board = new Board(size);
        _gameMode = gameMode;

        if (_gameMode == PvE) _bot = new Bot(_currentItem == Cross ? Zero : Cross, botMode);
    }
    /*----------------------------------------------*/


    /*------------| < Public Methods > |------------*/
    public bool MakeMove(int row, int column)
    {
        bool? playerMove = null;
        bool? botMove = null;

        // ХОД ИГРОКА: Игрок ходит и в PvP и в PvE режиме
        playerMove = IsSuccessfulMove(row, column);

        // ХОД БОТА
        if (_gameMode == PvE && playerMove != false && Winner == Winner.Undefined)
        {
            var position = _bot!.Move(_board);
            botMove = IsSuccessfulMove(position.row, position.column);
        }

        return (playerMove ?? false) && (botMove ?? true);
    }

    public void ResetGame()
    {
        _board.ResetBoard();
        _currentItem = Cross;
        _winner = Winner.Undefined;
    }

    public void PrintBoard() => _board.PrintBoard();
    /*----------------------------------------------*/


    /*------------| < Private Methods > |-----------*/
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
    /*----------------------------------------------*/
}