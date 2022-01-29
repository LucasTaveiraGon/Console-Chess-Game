using System;
using System.Collections.Generic;
using System.Text;
using ChessConsole.Board;

namespace ChessConsole.Chess
{
    class Tower : Piece
    {

        public Tower(BoardGame board, Collor collor) : base(board, collor)
        {
        }

        public override string ToString()
        {
            return "T";
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

            // acima
            pos.setValue(position.line - 1, position.column);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).collor != collor)
                {
                    break;
                }
                pos.line = pos.line - 1;
            }

            // abaixo
            pos.setValue(position.line + 1, position.column);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).collor != collor)
                {
                    break;
                }
                pos.line = pos.line + 1;
            }

            // direita
            pos.setValue(position.line, position.column + 1);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).collor != collor)
                {
                    break;
                }
                pos.column = pos.column + 1;
            }

            // esquerda
            pos.setValue(position.line, position.column - 1);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).collor != collor)
                {
                    break;
                }
                pos.column = pos.column - 1;
            }

            return mat;
        }
    }
}
