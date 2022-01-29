using System;
using System.Collections.Generic;
using System.Text;
using ChessConsole.Board;

namespace ChessConsole.Chess
{
    class King : Piece
    {


        private ChessMatch match;

        public King(BoardGame board, Collor collor, ChessMatch match) : base(board, collor)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.collor != collor;
        }

        private bool roqueTest(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Tower && p.collor == collor && p.qntMove == 0;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            // acima
            pos.setValue(position.line - 1, position.column);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // ne
            pos.setValue(position.line - 1, position.column + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // direita
            pos.setValue(position.line, position.column + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // se
            pos.setValue(position.line + 1, position.column + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // abaixo
            pos.setValue(position.line + 1, position.column);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // so
            pos.setValue(position.line + 1, position.column - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // esquerda
            pos.setValue(position.line, position.column - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // no
            pos.setValue(position.line - 1, position.column - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // #jogadaespecial roque
            if (qntMove == 0 && !match.check)
            {
                // #jogadaespecial roque pequeno
                Position posT1 = new Position(position.line, position.column + 3);
                if (roqueTest(posT1))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null)
                    {
                        mat[position.line, position.column + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Position posT2 = new Position(position.line, position.column - 4);
                if (roqueTest(posT2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.line, position.column - 2] = true;
                    }
                }
            }


            return mat;
        }
    }
}