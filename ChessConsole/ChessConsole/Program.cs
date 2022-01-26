using System;
using ChessConsole.Board;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BoardGame board = new BoardGame(8, 8);

            Screen.printBoard(board);


            Console.ReadLine();
        }
    }
}
