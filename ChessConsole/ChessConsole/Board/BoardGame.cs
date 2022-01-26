using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.Board
{
    class BoardGame
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public BoardGame(int lines, int columns)
        {
            this.Lines = lines;
            this.Columns = columns;
            Pieces = new Piece[lines, columns];
        }

    }
}
