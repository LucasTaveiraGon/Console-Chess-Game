using System;
using System.Collections.Generic;
using System.Text;
using ChessConsole.Board;

namespace ChessConsole.Chess
{
    class Horse : Piece
    {
        public Horse(BoardGame board , Collor collor) : base(board, collor)
        {
        }

        public override string ToString()
        {
            return "H";
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

            pos.setValue(position.line - 1, position.column - 2);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValue(position.line - 2, position.column - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValue(position.line - 2, position.column + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValue(position.line - 1, position.column + 2);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValue(position.line + 1, position.column + 2);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValue(position.line + 2, position.column + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValue(position.line + 2, position.column - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValue(position.line + 1, position.column - 2);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            return mat;
        } 
    }
}
