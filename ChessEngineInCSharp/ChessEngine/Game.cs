using System.Collections.Generic;

namespace ChessEngine
{
    public class Game
    {
        public static List<Move> MovesPlayed { get; set; }
        public static Move LastMoveForWhite { get; set; }
        public static Move LastMoveForBlack { get; set; }
        public static int TotalMovesPlayed { get; set; }
        public static Piece LastMovedPieceForWhite { get; set; }
        public static Piece LastMovedPieceForBlack { get; set; }
    }
}
