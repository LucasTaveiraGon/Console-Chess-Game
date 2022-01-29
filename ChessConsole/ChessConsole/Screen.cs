using System;
using System.Collections.Generic;
using ChessConsole.Board;
using ChessConsole.Chess;

namespace ChessConsole
{
    class Screen
    {
        public static void printMatch(ChessMatch match)
        {
            printBoard(match.board);
            Console.WriteLine();
            printCapturedPiece(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.turn);
            if (!match.finished)
            {
                Console.WriteLine("Wating for you Move: " + match.currentPlayer);
                if (match.check)
                {
                    Console.WriteLine("Check!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("WINNER: " + match.currentPlayer);
            }
        }

        public static void printCapturedPiece(ChessMatch match)
        {
            Console.WriteLine("Captive:");
            Console.Write("WHITES: ");
            printSet(match.capturedPiece(Collor.White));
            Console.WriteLine();
            Console.Write("BLACKS: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            printSet(match.capturedPiece(Collor.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void printSet(HashSet<Piece> sets)
        {
            Console.Write("[");
            foreach (Piece x in sets)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void printBoard(BoardGame board)
        {

            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("  A B C D E F G H");
        }

        public static void printBoard(BoardGame board, bool[,] posicoePossiveis)
        {

            ConsoleColor originFore = Console.BackgroundColor;
            ConsoleColor changedFore = ConsoleColor.DarkGray;

            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (posicoePossiveis[i, j])
                    {
                        Console.BackgroundColor = changedFore;
                    }
                    else
                    {
                        Console.BackgroundColor = originFore;
                    }
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originFore;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = originFore;
        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void printPiece(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.collor == Collor.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }


    }
}
