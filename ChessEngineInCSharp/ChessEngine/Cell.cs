using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    public class Cell
    {
        public Position Position { get; set; }
        
        public Piece Piece { get; set; }
        public Dictionary<string, long> ZorbistKeys { get; set; }
        
        public HashSet<int> MaximizingPlayerAttacks { get; set; }
        public HashSet<int> MinimizingPlayerAttacks { get; set; }
    }
}
