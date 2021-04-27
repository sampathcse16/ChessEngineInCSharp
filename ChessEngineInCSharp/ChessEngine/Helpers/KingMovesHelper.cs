using System;
using System.Collections.Generic;
using System.Text;
using ChessEngine.Pieces;

namespace ChessEngine.Helpers
{
    public class KingMovesHelper
    {
        static ulong one = 1;

        static int maxRowNumber = 7;

        public static ulong[,] AllPossibleKingMovesFromAllSquares { get; set; }

        public static HashSet<ulong> KingAllBinaryMoves { get; set; }

        public static List<Move>[] KingMovesBinaryToActualMoves { get; set; }

        public static ulong[,] KingBlockerMovesToBinaryMoves { get; set; }

        public static Dictionary<ulong, ulong>[] KingBlockerMovesToBinaryMovesDictionary { get; set; }

        public static ulong HashKeyForKingMoves = 199313;

        public static ulong[] MagicNumbersForKing =
        {
            1738393888573391104
            ,4663565512578433152
            ,3195339205907603456
            ,4701907589655429120
            ,2676272961010278464
            ,27303073017766912
            ,5092946460288258
            ,4630039145992159232
            ,4702338553122652224
            ,2306498318143930368
            ,144449439753457664
            ,576620181489715216
            ,288873040700252160
            ,378344150149384224
            ,45056887296688128
            ,1603563011107197058
            ,4698372099289088
            ,289358887398817792
            ,5048538231711080449
            ,20268813967687681
            ,1127310803666945
            ,41095506589385800
            ,281527053254657
            ,9363834036227
            ,2386978207825134594
            ,74776991252512
            ,111481696418791497
            ,577736203248337032
            ,144339497176270880
            ,36380916255301920
            ,595041399605297296
            ,288582634387300352
            ,1333105347006695424
            ,81075926120661536
            ,4575136739034369
            ,144133226948002817
            ,9008315950989314
            ,144116288663715840
            ,4611686057083281460
            ,6918725297913270272
            ,309506295864
            ,217303080604369922
            ,2377900706599322112
            ,2533868435472928
            ,288795525195542657
            ,144123984441517568
            ,720576661950565408
            ,580966556344828480
            ,848918004961811
            ,4611688364823806532
            ,4325680
            ,2306142076376973457
            ,2378041340874227788
            ,1130298087587878
            ,5765183667329589267
            ,2269393107304457
            ,144150540505319490
            ,655291337968852996
            ,2314850208737399555
            ,211106232533058
            ,594475228225080353
            ,216313865347015329
            ,17596489928721
            ,180144053815353638
        };

        public static void UpdateAllPossibleKingMovesFromAllSquares()
        {
            AllPossibleKingMovesFromAllSquares = new ulong[8, 8];
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

                    boardInStringFormat[row, column] = "WK";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
                    Cell cell = board[i, j];
                    List<Move> moves = King.GetMovesFromCache(board, cell);
                    ulong kingMoves = 0;

                    foreach (Move move in moves)
                    {
                        int square = move.To.Row * 8 + move.To.Column;
                        kingMoves = kingMoves | one << square;
                    }

                    AllPossibleKingMovesFromAllSquares[i, j] = kingMoves;
                    boardInStringFormat[row, column] = "  ";
                    MovesHelper.GetBinaryString(kingMoves);
                }
            }
        }

        public static void UpdateAllPossibleKingMovesForAllBlockers()
        {

            KingBlockerMovesToBinaryMoves = new ulong[64, 1 << 8];
            KingMovesBinaryToActualMoves = new List<Move>[HashKeyForKingMoves];
            KingBlockerMovesToBinaryMovesDictionary = new Dictionary<ulong, ulong>[64];
            KingAllBinaryMoves = new HashSet<ulong>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;
                    KingBlockerMovesToBinaryMovesDictionary[square] = new Dictionary<ulong, ulong>();
                    ulong allKingMoves = AllPossibleKingMovesFromAllSquares[i, j];
                    string[,] boardInStringArray = MovesHelper.GetBinaryToBoardInStringArray(allKingMoves, "WP");
                    boardInStringArray[7 - i, j] = "WK";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringArray);
                    GenerateAllBlockers(allKingMoves, 0, i, j, board);
                }
            }
        }

        public static void GenerateAllBlockers(ulong allKingMoves, int index, int row, int column, Cell[,] board)
        {
            if (index > 63)
            {
                UpdateActualMoves(allKingMoves, row, column, board);
                return;
            }
            int currentRow = (index / 8);
            int currentColumn = index % 8;

            GenerateAllBlockers(allKingMoves, index + 1, row, column, board);

            if ((allKingMoves & (one << index)) > 0)
            {
                Piece piece = board[currentRow, currentColumn].Piece;
                board[currentRow, currentColumn].Piece = null;
                GenerateAllBlockers(allKingMoves & ~(one << index), index + 1, row, column, board);
                board[currentRow, currentColumn].Piece = piece;
            }
        }

        public static void UpdateActualMoves(ulong kingBlockers, int row, int column, Cell[,] board)
        {
            int square = row * 8 + column;
            Cell cell = board[row, column];
            List<Move> moves = King.GetMovesFromCache(board, cell);
            ulong binaryKingMoves = 0;

            foreach (Move move in moves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                binaryKingMoves = binaryKingMoves | one << currentSquare;
            }

            int index = (int)(binaryKingMoves % HashKeyForKingMoves);
            KingMovesBinaryToActualMoves[index] = moves;

            int indexForBlocker = (int)((kingBlockers * MagicNumbersForKing[square]) >> (64 - 8));
            KingAllBinaryMoves.Add(binaryKingMoves);
            KingBlockerMovesToBinaryMoves[square, indexForBlocker] = binaryKingMoves;
            KingBlockerMovesToBinaryMovesDictionary[square][kingBlockers] = binaryKingMoves;
        }
    }
}
