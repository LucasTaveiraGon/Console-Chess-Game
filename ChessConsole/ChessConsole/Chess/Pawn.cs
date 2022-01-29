using System;
using System.Collections.Generic;
using System.Text;
using ChessConsole.Board;

namespace ChessConsole.Chess
{
    class Pawn : Piece
    {

        private ChessMatch match;

        public Pawn(BoardGame board, Collor collor, ChessMatch match) : base(board, collor)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool enemyExist(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.collor != collor;
        }

        private bool free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            if (collor == Collor.White)
            {
                pos.setValue(position.line - 1, position.column);
                if (board.positionValid(pos) && free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValue(position.line - 2, position.column);
                Position p2 = new Position(position.line - 1, position.column);
                if (board.positionValid(p2) && free(p2) && board.positionValid(pos) && free(pos) && qntMove == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValue(position.line - 1, position.column - 1);
                if (board.positionValid(pos) && enemyExist(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValue(position.line - 1, position.column + 1);
                if (board.positionValid(pos) && enemyExist(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // #jogadaespecial en passant
                if (position.line == 3)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.positionValid(left) && enemyExist(left) && board.piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.line - 1, left.column] = true;
                    }
                    Position direita = new Position(position.line, position.column + 1);
                    if (board.positionValid(direita) && enemyExist(direita) && board.piece(direita) == match.vulnerableEnPassant)
                    {
                        mat[direita.line - 1, direita.column] = true;
                    }
                }
            }
            else
            {
                pos.setValue(position.line + 1, position.column);
                if (board.positionValid(pos) && free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValue(position.line + 2, position.column);
                Position p2 = new Position(position.line + 1, position.column);
                if (board.positionValid(p2) && free(p2) && board.positionValid(pos) && free(pos) && qntMove == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValue(position.line + 1, position.column - 1);
                if (board.positionValid(pos) && enemyExist(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValue(position.line + 1, position.column + 1);
                if (board.positionValid(pos) && enemyExist(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // #jogadaespecial en passant
                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.positionValid(left) && enemyExist(left) && board.piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.line + 1, left.column] = true;
                    }
                    Position direita = new Position(position.line, position.column + 1);
                    if (board.positionValid(direita) && enemyExist(direita) && board.piece(direita) == match.vulnerableEnPassant)
                    {
                        mat[direita.line + 1, direita.column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
