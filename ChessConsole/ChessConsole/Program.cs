using System;
using ChessConsole.Board;
using ChessConsole.Chess;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BoardGame board = new BoardGame(8, 8);


                board.putPiece(new Tower(board, Collor.Black), new Position(0, 0));
                board.putPiece(new Tower(board, Collor.Black), new Position(1, 3));
                board.putPiece(new King(board, Collor.Black), new Position(2, 4));
                //    board.putPiece(Tower, new Position(0, 0));
                Screen.printBoard(board);





            }
            catch (BoardGameException e) 
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

        }
    }
}
