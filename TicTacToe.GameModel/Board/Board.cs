using TicTacToe.GameModel.GameBoard.BoardException;

namespace TicTacToe.GameModel.GameBoard;


public class Board
{
    /*------> Fields <------*/
    private Field[,] _board;

    public int Rows { get; init; }
    public int Columns { get; init; }

    private int BoardSize { get; init; }

    public Board(int size = 3)
    {
        if (size <= 0) throw new BoardExceptionNotNatural("Введено не натуральное число.");

        Rows = size;
        Columns = size;
        BoardSize = size;

        _board = new Field[Rows, Columns];

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                _board[i, j] = new Field() { Row = i, Column = j };
            }
        }
    }


    public int SetField(int row, int column, FieldItem item)
    {
        if (_board[row, column].IsEmpty())
        {
            _board[row, column].Item = item;
            return 1;
        }

        return 0;
    }

    public bool IsFieldEmpty(int row, int column) => _board[row, column].IsEmpty();


    public FieldItem FindWinner()
    {
        FieldItem item = Empty;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (IsWinningField(i, j))
                {
                    item = _board[i, j].Item;
                    break;
                }
            }
        }

        return item;
    }

    private bool IsWinningField(int row, int column)
    {
        Field field = new(_board[row, column]);

        if (field.Item == Empty) return false;

        bool rowWin = true, colWin = true, mainDiagWin = true, antiDiagWin = true;
        for (int i = 0; i < BoardSize; i++)
        {
            if (_board[field.Row, i].Item != field.Item) rowWin = false;
            if (_board[i, field.Column].Item != field.Item) colWin = false;
            if (_board[i, i].Item != field.Item) mainDiagWin = false;
            if (_board[i, BoardSize - 1 - i].Item != field.Item) antiDiagWin = false;
        }

        return rowWin || colWin || mainDiagWin || antiDiagWin;
    }
}