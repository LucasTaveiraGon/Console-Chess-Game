using System;
using System.Collections.Generic;
using System.Text;
using ChessConsole.Board;

namespace ChessConsole.Chess
{
    class King : Piece
    {
        public King(BoardGame board, Collor collor) : base(board, collor)
        {
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
