using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    public class Node
    {
        public int MoveId { get; set; }
        public List<Move> Moves { get; set; }
        public Dictionary<int, int> Costs { get; set; }
        public List<Node> ChildNodes { get; set; }
        public bool IsWhite { get; set; }
    }
}
