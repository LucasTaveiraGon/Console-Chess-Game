using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.Board
{
    class Piece
    {
        public Position position { get; set; }
        public Collor collor { get; protected set; }
        public int qntMove { get; protected set; }
        public BoardGame board { get; protected set; }

        public Piece(Position position, BoardGame board, Collor collor) 
        { 
            this.position = position;
            this.board = board;
            this.collor = collor;
            this.qntMove = 0;   


        }


    }
}
