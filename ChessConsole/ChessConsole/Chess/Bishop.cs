using System;
using System.Collections.Generic;
using System.Text;
using ChessConsole.Board;

namespace ChessConsole.Chess
{
    class Bishop : Piece
    {
        public Bishop(BoardGame board, Collor collor) : base(board, collor)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.collor != collor;
        }
        
        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            // NO
            pos.setValue(position.line - 1, position.column - 1);
            while(board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if(board.piece(pos) != null && board.piece(pos).collor != collor)
                {
                    break;
                }
                pos.setValue(pos.line - 1, pos.column - 1);
            }

            // NE
            pos.setValue(position.line - 1, position.column + 1);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).collor != collor)
                {
                    break;
                }
                pos.setValue(pos.line - 1, pos.column + 1);
            }

            // SE
            pos.setValue(position.line + 1, position.column + 1);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).collor != collor)
                {
                    break;
                }
                pos.setValue(pos.line + 1, pos.column + 1);
            }

            // SO
            pos.setValue(position.line + 1, position.column - 1);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).collor != collor)
                {
                    break;
                }
                pos.setValue(pos.line + 1, pos.column - 1);
            }
            return mat;
        } 
    }
}
