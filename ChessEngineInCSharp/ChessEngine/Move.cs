using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    public class Move
    {
        public Position From { get; set; }
        public Position To { get; set; }
        public decimal Cost { get; set; }
    }
}
