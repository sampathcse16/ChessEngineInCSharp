using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine.Pieces;

namespace ChessEngine.Helpers
{
    public class KingMovesHelper
    {
        static ulong one = 1;

        static int maxRowNumber = 7;

        public static ulong[,] AllPossibleMovesFromAllSquares { get; set; }

        public static HashSet<ulong>[] AllBinaryMoves { get; set; }

        public static List<Move>[,] BinaryToActualMoves { get; set; }

        public static ulong[,] BlockerMovesToBinaryMoves { get; set; }

        public static Dictionary<ulong, ulong>[] BlockerMovesToBinaryMovesDictionary { get; set; }

        public static ulong HashKeyForKingMoves = 199313;

        public static ulong[] MagicNumbersForBlockers =
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

        public static ulong[] MagicNumbersForActualMoves =
        {
            4758616508509274396
            ,3459116363984045580
            ,725642489961709568
            ,226309197006913600
            ,111464090777681920
            ,306808845769376068
            ,2414596254072834
            ,883338845930717265
            ,4611976332446826504
            ,2310419176608497664
            ,1155492163933470881
            ,576620181489451009
            ,36112360187887620
            ,144186141474555904
            ,2314871099189641728
            ,18024448732995648
            ,576819468040252297
            ,45038487375708672
            ,4697255657038941314
            ,6064097314874606600
            ,5045157690884489216
            ,2306407076403879936
            ,4611967631983906944
            ,4613943524105126057
            ,142938726347776
            ,292769170150392580
            ,288810923156578304
            ,5766938490117825040
            ,216177188905420800
            ,573945677906432
            ,5634999274446848
            ,40537896395014240
            ,5193151786717937680
            ,58640391108558865
            ,68855857280
            ,4612319508991868941
            ,288230393356816384
            ,4901161617268949008
            ,301745852261732352
            ,6755455289336448
            ,4613023025241014320
            ,4415763399684
            ,2305845209579241984
            ,585468090473551104
            ,72339112031504512
            ,144153671016388800
            ,2884573162123567172
            ,10481869882373
            ,597008563042519074
            ,1941427978644
            ,148618822365086000
            ,8796101607576
            ,35736284332108
            ,284928138952742
            ,15462436446227
            ,1213930093445771
            ,87969521254736
            ,4611703610657837089
            ,4503600701636752
            ,4625267186217386116
            ,1158868901548802132
            ,288235323956659238
            ,108086393741250577
            ,288536048974170122
        };

        public static void UpdateAllPossibleKingMovesFromAllSquares()
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

                    AllPossibleMovesFromAllSquares[i, j] = kingMoves;
                    boardInStringFormat[row, column] = "  ";
                    MovesHelper.GetBinaryString(kingMoves);
                }
            }
        }

        public static void UpdateAllPossibleKingMovesForAllBlockers()
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
                    ulong allKingMoves = AllPossibleMovesFromAllSquares[i, j];
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
            ulong binaryMoves = 0;

            foreach (Move move in moves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                binaryMoves = binaryMoves | one << currentSquare;
            }

            int index = (int)((binaryMoves * MagicNumbersForActualMoves[square]) >> (64 - 8));
            BinaryToActualMoves[square, index] = moves;
            AllBinaryMoves[square].Add(binaryMoves);

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

            int indexForBlocker = (int)((kingBlockers * MagicNumbersForBlockers[square]) >> (64 - 8));

            BlockerMovesToBinaryMoves[square, indexForBlocker] = binaryMoves;
            BlockerMovesToBinaryMovesDictionary[square][kingBlockers] = binaryMoves;
        }
    }
}
