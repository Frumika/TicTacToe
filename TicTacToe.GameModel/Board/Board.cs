using TicTacToe.GameModel.GameBoard.BoardException;

namespace TicTacToe.GameModel.GameBoard;

public class Board
{
    /*------------| < Private Fields > |------------*/
    private Field[,] _board;
    /*----------------------------------------------*/

    
    /*--------------| < Properties > |--------------*/
    public int Rows { get; init; }
    public int Columns { get; init; }

    private int BoardSize { get; init; }
    /*----------------------------------------------*/


    /*--------------| < Constructors > |------------*/
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
    /*----------------------------------------------*/


    /*------------| < Public Methods > |------------*/
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

    // Используется для проверки - Если поставить этот Item в это поле то мы победим?
    public bool CheckFieldForWin(int row, int column, FieldItem item)
    {
        if (item == Empty) return false;

        bool rowWin = true, colWin = true, mainDiagWin = true, antiDiagWin = true;

        for (int i = 0; i < BoardSize; i++)
        {
            if (_board[row, i].Item != item) rowWin = false;
            if (_board[i, column].Item != item) colWin = false;
            if (_board[i, i].Item != item) mainDiagWin = false;
            if (_board[i, BoardSize - 1 - i].Item != item) antiDiagWin = false;
        }

        return rowWin || colWin || mainDiagWin || antiDiagWin;
    }

    // Используется для проверки - Это поле уже является победным
    public bool IsWinningField(int row, int column)
    {
        Field field = new(_board[row, column]);
        return field.Item == Empty ? false : CheckFieldForWin(row, column, field.Item);
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
    /*----------------------------------------------*/
}