using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    public class BoardState
    {
        public Node Node { get; set; }
        public int Cost { get; set; }
        public bool IsOrdered { get; set; }
        public bool IsToBeOrderedBasedOnCost { get; set; }
        public bool IsKillingMoves { get; set; }
        public bool Maximizer { get; set; }
        public string BoardAsString { get; set; }
        public List<Move> MovesList { get; set; }
        public int Depth { get; set; }
    }
}
