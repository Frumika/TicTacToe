using TicTacToe.GameModel.Entity;
using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Model;

public class GameModel
{
    /*------------| < Private Fields > |------------*/
    private Board _board;
    private FieldItem _currentItem = Cross;
    private FieldItem _winner = Empty;
    /*----------------------------------------------*/


    /*--------------| < Properties > |--------------*/
    public FieldItem CurrentItem => _currentItem;
    public bool HasEmptyFields => _board.HasEmptyFields;
    public FieldItem Winner => _winner;
    /*----------------------------------------------*/


    /*--------------| < Constructors > |------------*/
    public GameModel(int size)
    {
        _board = new Board(size);
    }
    /*----------------------------------------------*/


    /*------------| < Public Methods > |------------*/
    public bool MakeMove(int row, int column)
    {
        if (_board.SetField(row, column, _currentItem))
        {
            if (_board.IsWinningField(row, column)) _winner = _currentItem;
            SwitchTurn();
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