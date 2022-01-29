using System;
using System.Collections.Generic;
using ChessConsole.Board;
using ChessConsole.Chess;

namespace ChessConsole
{
    class ChessMatch
    {

        public BoardGame board { get; private set; }
        public int turn { get; private set; }
        public Collor currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }
        public Piece vulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            board = new BoardGame(8, 8);
            turn = 1;
            currentPlayer = Collor.White;
            finished = false;
            check = false;
            vulnerableEnPassant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            putPiece();
        }

        public Piece executeMove(Position origin, Position destiny)
        {
            Piece p = board.removePiece(origin);
            p.moveIncrement();
            Piece capturedPiece = board.removePiece(destiny);
            board.putPiece(p, destiny);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // #jogadaespecial roque pequeno
            if (p is King && destiny.column == origin.column + 2)
            {
                Position originT = new Position(origin.line, origin.column + 3);
                Position destinyT = new Position(origin.line, origin.column + 1);
                Piece T = board.removePiece(originT);
                T.moveIncrement();
                board.putPiece(T, destinyT);
            }

            // #jogadaespecial roque grande
            if (p is King && destiny.column == origin.column - 2)
            {
                Position originT = new Position(origin.line, origin.column - 4);
                Position destinyT = new Position(origin.line, origin.column - 1);
                Piece T = board.removePiece(originT);
                T.moveIncrement();
                board.putPiece(T, destinyT);
            }

            // #jogadaespecial en passant
            if (p is Pawn)
            {
                if (origin.column != destiny.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.collor == Collor.White)
                    {
                        posP = new Position(destiny.line + 1, destiny.column);
                    }
                    else
                    {
                        posP = new Position(destiny.line - 1, destiny.column);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        /*internal HashSet<Piece> capturedPiece(Collor white)
        {
            throw new NotImplementedException();
        }*/

        public void unmakeMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = board.removePiece(destiny);
            p.moveDecrement();
            if (capturedPiece != null)
            {
                board.putPiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            board.putPiece(p, origin);

            // #jogadaespecial roque pequeno
            if (p is King && destiny.column == origin.column + 2)
            {
                Position originT = new Position(origin.line, origin.column + 3);
                Position destinityT = new Position(origin.line, origin.column + 1);
                Piece T = board.removePiece(destinityT);
                T.moveDecrement();
                board.putPiece(T, originT);
            }

            // #jogadaespecial roque grande
            if (p is King && destiny.column == origin.column - 2)
            {
                Position originT = new Position(origin.line, origin.column - 4);
                Position destinityT = new Position(origin.line, origin.column - 1);
                Piece T = board.removePiece(destinityT);
                T.moveDecrement();
                board.putPiece(T, originT);
            }

            // #jogadaespecial en passant
            if (p is Pawn)
            {
                if (origin.column != destiny.column && capturedPiece == vulnerableEnPassant)
                {
                    Piece pawn = board.removePiece(destiny);
                    Position posP;
                    if (p.collor == Collor.White)
                    {
                        posP = new Position(3, destiny.column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.column);
                    }
                    board.putPiece(pawn, posP);
                }
            }
        }

        public void makeMove(Position origin, Position destiny)
        {
            Piece capturedPiece = executeMove(origin, destiny);

            if (isInCheck(currentPlayer))
            {
                unmakeMove(origin, destiny, capturedPiece);
                throw new BoardGameException("You can't put you self in check!");
            }

            Piece p = board.piece(destiny);

            // #jogadaespecial promocao
            if (p is Pawn)
            {
                if ((p.collor == Collor.White && destiny.line == 0) || (p.collor == Collor.Black && destiny.line == 7))
                {
                    p = board.removePiece(destiny);
                    pieces.Remove(p);
                    Piece queen = new Queen(board, p.collor);
                    board.putPiece(queen, destiny);
                    pieces.Add(queen);
                }
            }

            if (isInCheck(opponent(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (checkmateTest(opponent(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                changePlayer();
            }

            // #jogadaespecial en passant
            if (p is Pawn && (destiny.line == origin.line - 2 || destiny.line == origin.line + 2))
            {
                vulnerableEnPassant = p;
            }
            else
            {
                vulnerableEnPassant = null;
            }

        }

        public void validateOriginPosition(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new BoardGameException("Don't exist piece in this position");
            }
            if (currentPlayer != board.piece(pos).collor)
            {
                throw new BoardGameException("The chose piece is not yours");
            }
            if (!board.piece(pos).possMoveExist())
            {
                throw new BoardGameException("Not movements possibles with the piece!");
            }
        }

        public void validateDestinyPosition(Position origin, Position destiny)
        {
            if (!board.piece(origin).possibleMove(destiny))
            {
                throw new BoardGameException("Invalid Position!");
            }
        }

        private void changePlayer()
        {
            if (currentPlayer == Collor.White)
            {
                currentPlayer = Collor.Black;
            }
            else
            {
                currentPlayer = Collor.White;
            }
        }

        public HashSet<Piece> capturedPiece(Collor collor)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.collor == collor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInGame(Collor collor)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.collor == collor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPiece(collor));
            return aux;
        }

        private Collor opponent(Collor collor)
        {
            if (collor == Collor.White)
            {
                return Collor.Black;
            }
            else
            {
                return Collor.White;
            }
        }

        private Piece king(Collor collor)
        {
            foreach (Piece x in piecesInGame(collor))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool isInCheck(Collor collor)
        {
            Piece K = king(collor);
            if (K == null)
            {
                throw new BoardGameException("Theres not a King with " + collor + " in the BoardGame!");
            }
            foreach (Piece x in piecesInGame(opponent(collor)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[K.position.line, K.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkmateTest(Collor collor)
        {
            if (!isInCheck(collor))
            {
                return false;
            }
            foreach (Piece x in piecesInGame(collor))
            {
                bool[,] mat = x.possibleMoves();
                for (int i = 0; i < board.lines; i++)
                {
                    for (int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = executeMove(origin, destiny);
                            bool checkTest = isInCheck(collor);
                            unmakeMove(origin, destiny, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPiece(char column, int line, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }

        private void putPiece()
        {
            putNewPiece('a', 1, new Tower(board, Collor.White));
            putNewPiece('b', 1, new Horse(board, Collor.White));
            putNewPiece('c', 1, new Bishop(board, Collor.White));
            putNewPiece('d', 1, new Queen(board, Collor.White));
            putNewPiece('e', 1, new King(board, Collor.White, this));
            putNewPiece('f', 1, new Bishop(board, Collor.White));
            putNewPiece('g', 1, new Horse(board, Collor.White));
            putNewPiece('h', 1, new Tower(board, Collor.White));
            putNewPiece('a', 2, new Pawn(board, Collor.White, this));
            putNewPiece('b', 2, new Pawn(board, Collor.White, this));
            putNewPiece('c', 2, new Pawn(board, Collor.White, this));
            putNewPiece('d', 2, new Pawn(board, Collor.White, this));
            putNewPiece('e', 2, new Pawn(board, Collor.White, this));
            putNewPiece('f', 2, new Pawn(board, Collor.White, this));
            putNewPiece('g', 2, new Pawn(board, Collor.White, this));
            putNewPiece('h', 2, new Pawn(board, Collor.White, this));

            putNewPiece('a', 8, new Tower(board, Collor.Black));
            putNewPiece('b', 8, new Horse(board, Collor.Black));
            putNewPiece('c', 8, new Bishop(board, Collor.Black));
            putNewPiece('d', 8, new Queen(board, Collor.Black));
            putNewPiece('e', 8, new King(board, Collor.Black, this));
            putNewPiece('f', 8, new Bishop(board, Collor.Black));
            putNewPiece('g', 8, new Horse(board, Collor.Black));
            putNewPiece('h', 8, new Tower(board, Collor.Black));
            putNewPiece('a', 7, new Pawn(board, Collor.Black, this));
            putNewPiece('b', 7, new Pawn(board, Collor.Black, this));
            putNewPiece('c', 7, new Pawn(board, Collor.Black, this));
            putNewPiece('d', 7, new Pawn(board, Collor.Black, this));
            putNewPiece('e', 7, new Pawn(board, Collor.Black, this));
            putNewPiece('f', 7, new Pawn(board, Collor.Black, this));
            putNewPiece('g', 7, new Pawn(board, Collor.Black, this));
            putNewPiece('h', 7, new Pawn(board, Collor.Black, this));
        }
    }
}

