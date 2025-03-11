﻿using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;

public class HardBotStrategy : BotStrategy
{
    public HardBotStrategy(FieldItem item) : base(item)
    {
    }

    public override (int row, int column) FindField(Board board)
    {
        var (bestMove, _) = FindBestMove(board, Item);
        return bestMove;
    }

    private ((int row, int column) move, int score) FindBestMove(Board board, FieldItem current, int depth = 1)
    {
        (int row, int column) bestMove = (-1, -1);
        int bestScore = current == Item ? int.MinValue : int.MaxValue;

        var emptyFields = board.GetFieldsCoordinates(Empty);

        if (!board.HasEmptyFields) return (bestMove, 0);

        foreach (var field in emptyFields)
        {
            Board newBoard = new Board(board);
            newBoard.SetField(field.row, field.column, current);

            int score = DetermineCostMove(newBoard, current, field.row, field.column, depth);

            if (current == Item)
            {
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = (field.row, field.column);
                }
            }
            else
            {
                if (score < bestScore)
                {
                    bestScore = score;
                    bestMove = (field.row, field.column);
                }
            }
        }

        return (bestMove, bestScore);
    }

    private int DetermineCostMove(Board board, FieldItem current, int row, int column, int depth)
    {
        int cost = board.Rows + board.Columns + 1;

        if (board.IsWinningField(row, column))
        {
            return current == Item ? cost - depth : depth - cost;
        }
        else if (!board.HasEmptyFields)
        {
            return 0;
        }
        else
        {
            FieldItem opponent = current == Zero ? Cross : Zero;
            var (_, score) = FindBestMove(board, opponent, depth + 1);
            return score;
        }
    }
}