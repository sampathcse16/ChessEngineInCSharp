using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine.Pieces;

namespace ChessEngine.Helpers
{
    public class KnightMovesHelper
    {
        static ulong one = 1;

        static int maxRowNumber = 7;

        public static ulong[,] AllPossibleMovesFromAllSquares { get; set; }

        public static HashSet<ulong>[] AllBinaryMoves { get; set; }

        public static List<Move>[,] BinaryToActualMoves { get; set; }

        public static ulong[,] BlockerMovesToBinaryMoves { get; set; }

        public static Dictionary<ulong, ulong>[] BlockerMovesToBinaryMovesDictionary { get; set; }

        public static ulong HashKeyForKnightMoves = 159531;

        public static ulong[] MagicNumbersForBlockers =
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

        public static ulong[] MagicNumbersForActualMoves =
        {
            1759218612830272
            ,20306607658434592
            ,334251534843904
            ,18156304766337024
            ,281788515757122
            ,4622964258951397636
            ,145242462372235280
            ,365140956446784
            ,144485254538723328
            ,150872100495704196
            ,76596588562098304
            ,2310423746529329248
            ,18015516006285378
            ,18014542395212288
            ,2017620639983080448
            ,9295288682742336
            ,145346778554695966
            ,432767811723461640
            ,1154333569645052162
            ,576601498390104064
            ,2451224843276812352
            ,2450098935316873220
            ,6919780854333964288
            ,1196855996219394
            ,1153066924924993536
            ,81069398579872000
            ,2260595940294699
            ,6928788576566020097
            ,1153560322003697792
            ,171349008898457600
            ,2323928054163965312
            ,576465700114141376
            ,162131391815876880
            ,3026473993927656449
            ,4611694823144423552
            ,291049541149458628
            ,4629711420652027938
            ,1297037796489314372
            ,360674513522704
            ,72148029945811328
            ,9037994589686336
            ,2336596426820
            ,303533995855921
            ,72059999489100803
            ,594546226221810178
            ,2252074964222209
            ,2378228333476319305
            ,9012696900993542
            ,76649429659090948
            ,1297036693517664324
            ,5927018722110767108
            ,36591749421891594
            ,18014404962455618
            ,81069191490453761
            ,4035225283306987652
            ,1769949839073755137
            ,4512413173420032
            ,216181715696227328
            ,4621819394079918080
            ,74314994759766178
            ,4611830192030159104
            ,4611686191836823816
            ,34399731745
            ,2305843146786930720
        };

        public static void UpdateAllPossibleMovesFromAllSquares()
        {
            AllPossibleMovesFromAllSquares = new ulong[8, 8];
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

                    AllPossibleMovesFromAllSquares[i, j] = knightMoves;
                    boardInStringFormat[row, column] = "  ";
                    MovesHelper.GetBinaryString(knightMoves);
                }
            }
        }

        public static void UpdateAllPossibleMovesForAllBlockers()
        {

            BlockerMovesToBinaryMoves = new ulong[64, 1 << 8];
            BinaryToActualMoves = new List<Move>[64, 1 << 8];
            BlockerMovesToBinaryMovesDictionary = new Dictionary<ulong, ulong>[64];
            AllBinaryMoves = new HashSet<ulong>[64];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;
                    BlockerMovesToBinaryMovesDictionary[square] = new Dictionary<ulong, ulong>();
                    AllBinaryMoves[square] = new HashSet<ulong>();
                    ulong allKnightMoves = AllPossibleMovesFromAllSquares[i, j];
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

            int index = (int)((binaryKnightMoves * MagicNumbersForActualMoves[square]) >> (64 - 8));
            BinaryToActualMoves[square, index] = moves;
            AllBinaryMoves[square].Add(binaryKnightMoves);

            List<Move> killerMoves = moves.Where(x => board[x.To.Row, x.To.Column].Piece != null).ToList();
            ulong killerBinaryRookMoves = 0;

            foreach (Move move in killerMoves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                killerBinaryRookMoves = killerBinaryRookMoves | one << currentSquare;
            }

            index = (int)((killerBinaryRookMoves * MagicNumbersForActualMoves[square]) >> (64 - 8));
            BinaryToActualMoves[square, index] = killerMoves;
            AllBinaryMoves[square].Add(killerBinaryRookMoves);

            int indexForBlocker = (int)((knightBlockers * MagicNumbersForBlockers[square]) >> (64 - 8));
            BlockerMovesToBinaryMoves[square, indexForBlocker] = binaryKnightMoves;
            BlockerMovesToBinaryMovesDictionary[square][knightBlockers] = binaryKnightMoves;
        }
    }
}
