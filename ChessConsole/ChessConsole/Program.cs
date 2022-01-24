using System;
using ChessConsole.Board;

namespace ChessConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position P = new Position(3, 4);
            Console.WriteLine("Position: "+P);
            Console.ReadLine(); 
        }
    }
}
