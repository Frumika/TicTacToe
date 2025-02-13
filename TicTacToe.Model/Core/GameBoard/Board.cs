namespace TicTacToe.Model.Core.GameBoard;

public class Board
{
    /*------> Fields <------*/
    private Field[,] _board;

    public int Rows { get; init; }
    public int Columns { get; init; }

    private int BoardSize { get; init; }

    // Todo: Добавить проверку вводимого размера 
    public Board(int size = 3)
    {
        Rows = size;
        Columns = size;
        BoardSize = size;

        _board = new Field[Rows, Columns];

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                _board[i, j] = new Field();
                _board[i, j].Row = i;
                _board[i, j].Column = j;
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

    public bool IsTheFieldEmpty(int row, int column) => _board[row, column].IsEmpty();

    public FieldItem FindWinner()
    {
        FieldItem item = Empty;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Field field = new Field(_board[i, j]);
                if (IsWinningField(field))
                {
                    item = field.Item;
                    break;
                }
            }
        }

        return item;
    }

    private bool IsWinningField(Field field)
    {
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