using System;
using ChessConsole.Board;

namespace ChessConsole
{
    class Screen
    {
        public static void printBoard(BoardGame board)
        {

            for (int i = 0; i < board.lines; i++)
            {


                for (int j = 0; j < board.columns; j++)
                {
                    if (board.piece(i, j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write(board.piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
