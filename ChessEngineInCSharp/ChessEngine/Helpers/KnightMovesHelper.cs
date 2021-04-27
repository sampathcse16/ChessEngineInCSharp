using System;
using System.Collections.Generic;
using System.Text;
using ChessEngine.Pieces;

namespace ChessEngine.Helpers
{
    public class KnightMovesHelper
    {
        static ulong one = 1;

        static int maxRowNumber = 7;

        public static ulong[,] AllPossibleKnightMovesFromAllSquares { get; set; }

        public static HashSet<ulong> KnightAllBinaryMoves { get; set; }

        public static List<Move>[] KnightMovesBinaryToActualMoves { get; set; }

        public static ulong[,] KnightBlockerMovesToBinaryMoves { get; set; }

        public static Dictionary<ulong, ulong>[] KnightBlockerMovesToBinaryMovesDictionary { get; set; }

        public static ulong HashKeyForKnightMoves = 159531;

        public static ulong[] MagicNumbersForKnight =
        {
            2342034606968016994
            ,1161089685389320
            ,599015034324095251
            ,46188285544661576
            ,19144730823360896
            ,288336075302633472
            ,5067099347107842
            ,563500011261952
            ,585468368170262816
            ,72202901405045762
            ,2594224028123153409
            ,76702506748477506
            ,1297045594038339776
            ,292742875018690688
            ,36039105099862084
            ,4661225895648821248
            ,1153001090485584404
            ,288529581290293304
            ,577023736641750016
            ,577024254169055266
            ,721138892482283520
            ,1157706580301450240
            ,725097278557388808
            ,4578400928931840
            ,37155848514461700
            ,144418670532231720
            ,2504573139403415560
            ,288373329977933825
            ,2738756474451796008
            ,36592296761892872
            ,2305851806447571232
            ,1152993024406720576
            ,1297142556146535937
            ,76565677611434048
            ,14638932190830625
            ,144137182607835200
            ,293860154878989313
            ,1162211283722895377
            ,144115273975562274
            ,3381557742043142
            ,1211644917443494977
            ,18726334238736
            ,153687815815745552
            ,40684111333448
            ,1125939131973762
            ,281887864522754
            ,281509940691985
            ,72057662832902178
            ,286457222693904
            ,1128170099574786
            ,2305913790285746241
            ,4618513992145571844
            ,1688849928438282
            ,288318337367808013
            ,6808178160370180
            ,4612249041953359106
            ,4719913696762202112
            ,1193459398822936832
            ,110338199460806676
            ,139281
            ,1884756445145080456
            ,4616356743824425008
            ,1163054603810390528
            ,18122150653263880
        };

        public static void UpdateAllPossibleMovesFromAllSquares()
        {
            AllPossibleKnightMovesFromAllSquares = new ulong[8, 8];
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int row = maxRowNumber - i;
                    int column = j;

                    boardInStringFormat[row, column] = "WN";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
                    Cell cell = board[i, j];
                    List<Move> moves = Knight.GetMovesFromCache(board, cell);
                    ulong knightMoves = 0;

                    foreach (Move move in moves)
                    {
                        int square = move.To.Row * 8 + move.To.Column;
                        knightMoves = knightMoves | one << square;
                    }

                    AllPossibleKnightMovesFromAllSquares[i, j] = knightMoves;
                    boardInStringFormat[row, column] = "  ";
                    MovesHelper.GetBinaryString(knightMoves);
                }
            }
        }

        public static void UpdateAllPossibleMovesForAllBlockers()
        {

            KnightBlockerMovesToBinaryMoves = new ulong[64, 1 << 8];
            KnightMovesBinaryToActualMoves = new List<Move>[HashKeyForKnightMoves];
            KnightBlockerMovesToBinaryMovesDictionary = new Dictionary<ulong, ulong>[64];
            KnightAllBinaryMoves = new HashSet<ulong>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;
                    KnightBlockerMovesToBinaryMovesDictionary[square] = new Dictionary<ulong, ulong>();
                    ulong allKnightMoves = AllPossibleKnightMovesFromAllSquares[i, j];
                    string[,] boardInStringArray = MovesHelper.GetBinaryToBoardInStringArray(allKnightMoves, "WP");
                    boardInStringArray[7 - i, j] = "WN";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringArray);
                    GenerateAllBlockers(allKnightMoves, 0, i, j, board);
                }
            }
        }

        public static void GenerateAllBlockers(ulong allKnightMoves, int index, int row, int column, Cell[,] board)
        {
            if (index > 63)
            {
                UpdateActualMoves(allKnightMoves, row, column, board);
                return;
            }
            int currentRow = (index / 8);
            int currentColumn = index % 8;

            GenerateAllBlockers(allKnightMoves, index + 1, row, column, board);

            if ((allKnightMoves & (one << index)) > 0)
            {
                Piece piece = board[currentRow, currentColumn].Piece;
                board[currentRow, currentColumn].Piece = null;
                GenerateAllBlockers(allKnightMoves & ~(one << index), index + 1, row, column, board);
                board[currentRow, currentColumn].Piece = piece;
            }
        }

        public static void UpdateActualMoves(ulong knightBlockers, int row, int column, Cell[,] board)
        {
            int square = row * 8 + column;
            Cell cell = board[row, column];
            List<Move> moves = Knight.GetMovesFromCache(board, cell);
            ulong binaryKnightMoves = 0;
            
            foreach (Move move in moves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                binaryKnightMoves = binaryKnightMoves | one << currentSquare;
            }

            int index = (int)(binaryKnightMoves % HashKeyForKnightMoves);
            KnightMovesBinaryToActualMoves[index] = moves;

            int indexForBlocker = (int)((knightBlockers * MagicNumbersForKnight[square]) >> (64 - 8));
            KnightAllBinaryMoves.Add(binaryKnightMoves);
            KnightBlockerMovesToBinaryMoves[square, indexForBlocker] = binaryKnightMoves;
            KnightBlockerMovesToBinaryMovesDictionary[square][knightBlockers] = binaryKnightMoves;
        }
    }
}
