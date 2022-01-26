using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.Board
{
    class BoardGameException : Exception
    {
        public BoardGameException(string msg) : base(msg)
        {
        }

    }
}
