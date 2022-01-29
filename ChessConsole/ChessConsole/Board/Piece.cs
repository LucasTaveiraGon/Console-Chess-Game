using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.Board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Collor collor { get; protected set; }
        public int qntMove { get; protected set; }
        public BoardGame board { get; protected set; }

        public Piece(BoardGame board, Collor collor) 
        { 
            this.position = null;
            this.board = board;
            this.collor = collor;
            this.qntMove = 0;   
        }

        public void moveIncrement()
        {
            qntMove++;
        }

        public void moveDecrement()
        {
            qntMove--;
        }

        public bool possMoveExist()
        {
            bool[,] mat = possibleMoves();
            for (int i = 0; i< board.lines; i++)
            {
                for(int j = 0; j< board.columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool possibleMove(Position pos)
        {
            return possibleMoves()[pos.line, pos.column];
        }
        public abstract bool[,] possibleMoves(); 

    }
}
