using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    public class Piece
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsWhite { get; set; }
        public int Value { get; set; }
        public int MinValue { get; set; }
    }
}
