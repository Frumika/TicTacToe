namespace TicTacToe.Model.Core.Board;

public class Board
{
    /*------> Fields <------*/
    private Field[,] _gameBoard;

    public int Rows { get; init; }
    public int Columns { get; init; }

    public Board(int rows = 3, int columns = 3)
    {
        Rows = rows;
        Columns = columns;

        _gameBoard = new Field[Rows, Columns];
    }


    public int SetField(int row, int column, FieldStatus status)
    {
        if (_gameBoard[row, column].IsEmpty())
        {
            _gameBoard[row, column].Status = status;
            return 1;
        }

        return 0;
    }

    public bool IsTheFieldEmpty(int row, int column) => _gameBoard[row, column].IsEmpty();

    public bool FindWinner()
    {
        return true;
    }
}