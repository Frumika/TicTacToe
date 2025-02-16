using TicTacToe.GameModel.Entity;
using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Model;

public class GameModel
{
    /*------------| < Private Fields > |------------*/
    private Board _board;

    private FieldItem _currentItem = Cross;
    private FieldItem _winner = Empty;

    private GameMode _gameMode;

    private Bot? _bot = null;
    /*----------------------------------------------*/


    /*--------------| < Properties > |--------------*/
    public FieldItem CurrentItem => _currentItem;
    public bool HasEmptyFields => _board.HasEmptyFields;
    public FieldItem Winner => _winner;
    /*----------------------------------------------*/


    /*--------------| < Constructors > |------------*/
    public GameModel(int size, GameMode gameMode, BotMode botMode)
    {
        _board = new Board(size);
        _gameMode = gameMode;

        if (_gameMode == GameMode.PvE) _bot = new Bot(Zero, botMode);
    }
    /*----------------------------------------------*/


    /*------------| < Public Methods > |------------*/
    public bool MakeMove(int row, int column)
    {
        if (_board.SetField(row, column, _currentItem))
        {
            if (_board.IsWinningField(row, column))
            {
                _winner = _currentItem;
                return true;
            }
            if (_gameMode == GameMode.PvP) SwitchTurn();

            
            else if (_gameMode == GameMode.PvE && HasEmptyFields)
            {
                var position = _bot!.Move(_board);

                _board.SetField(position.row, position.column, _bot.Item);
                if (_board.IsWinningField(position.row, position.column))
                {
                    _winner = _bot.Item;
                    return true;
                }
            }

            return true;
        }

        return false;
    }

    private void SwitchTurn()
    {
        _currentItem = _currentItem == Cross ? Zero : Cross;
    }

    public void PrintBoard() => _board.PrintBoard();
    /*----------------------------------------------*/
}