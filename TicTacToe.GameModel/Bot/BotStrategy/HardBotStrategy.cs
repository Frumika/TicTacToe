using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;

public class HardBotStrategy : BotStrategy
{
    public HardBotStrategy(FieldItem item) : base(item)
    {
    }

    public override (int row, int column) FindField(Board board)
    {
        (int row, int column) bestMove = (-1, -1);
        int bestScore = int.MinValue; // Для бота ищем максимум

        foreach (var field in board.GetFieldsCoordinates(Empty))
        {
            Board newBoard = new Board(board);
            newBoard.SetField(field.row, field.column, Item);

            Console.WriteLine("NEW BOARD =>");
            newBoard.PrintBoard();
            Console.WriteLine("NEW BOARD =>");

            int score = Minimax(newBoard, false, 1); // Передаём false, так как теперь ходит человек

            if (score > bestScore)
            {
                bestScore = score;
                bestMove = field;
            }
        }

        return bestMove;
    }

    private int Minimax(Board board, bool isMaximizing, int depth)
    {
        FieldItem opponentItem = Item == Zero ? Cross : Zero;

        if (IsWinningState(board, Item)) return 10 - depth; // Если бот победил
        if (IsWinningState(board, opponentItem)) return depth - 10; // Если победил игрок
        if (!board.HasEmptyFields) return 0; // Ничья

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        FieldItem current = isMaximizing ? Item : opponentItem;

        foreach (var field in board.GetFieldsCoordinates(Empty))
        {
            Board newBoard = new Board(board);
            newBoard.SetField(field.row, field.column, current);
            int score = Minimax(newBoard, !isMaximizing, depth + 1);

            Console.WriteLine($"Minimax Score: {score}");
            
            if (isMaximizing)
            {
                bestScore = Math.Max(bestScore, score);
            }
            else
            {
                bestScore = Math.Min(bestScore, score);
            }
        }

        return bestScore;
    }

    private bool IsWinningState(Board board, FieldItem player)
    {
        foreach (var field in board.GetFieldsCoordinates(player))
        {
            if (board.IsWinningField(field.row, field.column))
            {
                Console.WriteLine("|>-----------------------");
                board.PrintBoard();
                Console.WriteLine("|>-----------------------");
                return true;
            }
        }

        return false;
    }
}