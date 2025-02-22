using TicTacToe.GameModel.GameBoard.BoardException;

namespace TicTacToe.GameModel.GameBoard;

public class Board
{
    /*------------| < Private Fields > |------------*/
    private Field[,] _board;
    private int _emptyFields;
    /*----------------------------------------------*/


    /*--------------| < Properties > |--------------*/
    public int Rows { get; init; }
    public int Columns { get; init; }

    public bool HasEmptyFields => _emptyFields > 0;

    private int Size { get; init; }

    /*----------------------------------------------*/


    /*--------------| < Constructors > |------------*/
    public Board(int size = 3)
    {
        if (size <= 0) throw new BoardExceptionNotNatural("Введено не натуральное число.");

        Rows = size;
        Columns = size;
        Size = size;

        _board = new Field[Rows, Columns];
        _emptyFields = Rows * Columns;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                _board[i, j] = new Field() { Row = i, Column = j };
            }
        }
    }

    /*----------------------------------------------*/


    /*------------| < Public Methods > |------------*/
    public bool SetField(int row, int column, FieldItem item)
    {
        if (IsFieldEmpty(row, column))
        {
            _board[row, column].Item = item;
            _emptyFields -= 1;
            return true;
        }

        return false;
    }


    public bool IsFieldEmpty(int row, int column)
    {
        if (row >= Rows || column >= Columns || row < 0 || column < 0) return false;
        return _board[row, column].IsEmpty();
    }


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

    public bool CanItBeWinningField(int row, int column, FieldItem item)
    {
        if (item == Empty) return false;

        int rowWin = 0, colWin = 0, mainDiagWin = 0, antiDiagWin = 0;

        for (int i = 0; i < Size; i++)
        {
            if (_board[row, i].Item == item || i == column) rowWin += 1;
            if (_board[i, column].Item == item || i == row) colWin += 1;

            if (row == column &&
                (_board[i, i].Item == item || i == row)) mainDiagWin += 1;
            if (row + column == Rows - 1 &&
                (_board[i, Size - 1 - i].Item == item || (i == row && Size - 1 - i == column))) antiDiagWin += 1;
        }

        return rowWin == Size || colWin == Size || mainDiagWin == Size || antiDiagWin == Size;
    }

    public bool IsWinningField(int row, int column)
    {
        Field field = new(_board[row, column]);

        if (field.Item == Empty) return false;

        bool rowWin = true, colWin = true, mainDiagWin = true, antiDiagWin = true;

        for (int i = 0; i < Size; i++)
        {
            if (_board[row, i].Item != field.Item) rowWin = false;
            if (_board[i, column].Item != field.Item) colWin = false;
            if (_board[i, i].Item != field.Item) mainDiagWin = false;
            if (_board[i, Size - 1 - i].Item != field.Item) antiDiagWin = false;
        }

        return rowWin || colWin || mainDiagWin || antiDiagWin;
    }


    public List<(int row, int column)> GetFieldsCoordinates(FieldItem item)
    {
        List<(int row, int column)> coordinates = new();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (_board[i, j].Item == item) coordinates.Add((i, j));
            }
        }

        return coordinates;
    }


    public void PrintBoard()
    {
        for (int i = 0; i < Rows; i++) Console.Write("|-----");
        Console.WriteLine("|");

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Field field = new Field(_board[i, j]);
                if (field.IsCross()) Console.Write($"|  X  ");
                else if (field.IsZero()) Console.Write($"|  O  ");
                else Console.Write($"|     ");
            }


            Console.WriteLine("|");
            for (int k = 0; k < Rows; k++) Console.Write("|-----");
            Console.WriteLine("|");
        }
    }
    /*----------------------------------------------*/
}