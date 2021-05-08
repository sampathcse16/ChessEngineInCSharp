using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine.Pieces;

namespace ChessEngine.Helpers
{
    public class RookMovesHelper
    {
        static ulong one = 1;

        static int maxRowNumber = 7;

        public static ulong[,] AllPossibleRookMovesFromAllSquares { get; set; }

        public static List<Move>[,] BinaryToActualMoves { get; set; }

        public static HashSet<ulong>[] AllBinaryMoves { get; set; }

        public static ulong[,] RookBlockerMovesToBinaryMoves { get; set; }
        public static Dictionary<ulong, ulong>[] RookBlockerMovesToBinaryMovesDictionary { get; set; }

        public static ulong HashKeyForRookMoves = 649579;

        public static ulong[] MagicNumbersForBlockers =
        {
             331014710050949120
            ,2454466332406583296
            ,45038200128801856
            ,2377901188509207296
            ,5332262784716507648
            ,144115540263182600
            ,4828439351270576384
            ,2632354536249114632
            ,140772155490816
            ,2336768394240
            ,1126037346878464
            ,4941856172652626176
            ,2305862800490121280
            ,17592878366912
            ,1450163478345023808
            ,4611730003196248256
            ,9912785569792
            ,4675084763136
            ,2199040569856
            ,10995150359712
            ,1153067739686945024
            ,2405184046080
            ,72937272063885824
            ,3316788527120
            ,140738606148608
            ,585547134112107136
            ,1649334572032
            ,4611686585900007968
            ,38280873992257696
            ,576461035838374408
            ,72137325811081544
            ,2257299250906368
            ,354043022017536
            ,4398082425344
            ,46318034616832
            ,2220514870288
            ,278107521540
            ,1229483257693733384
            ,1170379374628
            ,74310776832163860
            ,140894535811584
            ,4402346263040
            ,17630840885248
            ,7980091183104
            ,1108109967364
            ,576460891889893632
            ,275415072776
            ,144115325527523488
            ,143006842241152
            ,4947949126144
            ,83318795350909440
            ,1153301936168241664
            ,274953535632
            ,162413269913518208
            ,288230451859169312
            ,288230444904941968
            ,360777548693510
            ,582231272794424322
            ,739153358637572106
            ,2199031912458
            ,576751335027376130
            ,4683748053604123809
            ,72084020974880260
            ,1101660162114
        };

        public static ulong[] MagicNumbersForActualMoves =
        {
            2885121732641216
            ,653602920289543170
            ,298645293887652740
            ,1734167340104581154
            ,579275850501915668
            ,151433540221797396
            ,225461484279341058
            ,2379169585707516417
            ,13524549222584448
            ,37304230842730497
            ,576478344791597168
            ,45053607066337539
            ,72627149785273410
            ,1319416172118146
            ,216467452580628494
            ,6922327383896228961
            ,5200531876403494920
            ,2887089593109217344
            ,5769129279644254529
            ,607986396372142216
            ,5926741593567531016
            ,4900479895398121474
            ,2306134388391350433
            ,76843562600366145
            ,148694793609544832
            ,146648876298801600
            ,2251903161384976
            ,2307813343445913616
            ,4506077295542296
            ,290483846708920585
            ,1008824115211075585
            ,220188479852705
            ,144240257810825248
            ,4611743747085961248
            ,3464411622737510422
            ,2260630401712432
            ,4612814134587691025
            ,18085319211483266
            ,149886249753118985
            ,2337403463481164353
            ,36064050113781824
            ,11270028563317472
            ,2882374714402213920
            ,2251868835164164
            ,720611133375057923
            ,567366455133316
            ,216182696242884626
            ,288670945342933505
            ,333267610718179396
            ,81702511177631756
            ,288529859540813832
            ,5665822341595907
            ,9580079390941346
            ,436853836847648796
            ,578714751146918023
            ,167068593901539337
            ,4776209244045738785
            ,145102550062989331
            ,146581462584989713
            ,27596642412957707
            ,216225576402095241
            ,36606040645698121
            ,4756082708357318661
            ,1126179115452426
        };

        public static void UpdateAllPossibleMovesFromAllSquares()
        {
            AllPossibleRookMovesFromAllSquares = new ulong[8, 8];
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

                    boardInStringFormat[row, column] = "WR";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
                    Cell cell = board[i, j];
                    List<Move> moves = Rook.GetMovesFromCache(board, cell);
                    ulong rookMoves = 0;

                    foreach (Move move in moves)
                    {
                        int square = move.To.Row * 8 + move.To.Column;
                        rookMoves = rookMoves | one << square;
                    }

                    AllPossibleRookMovesFromAllSquares[i, j] = rookMoves;
                    boardInStringFormat[row, column] = "  ";
                    MovesHelper.GetBinaryString(rookMoves);
                }
            }
        }

        public static void UpdateAllPossibleMovesForAllBlockers()
        {

            RookBlockerMovesToBinaryMoves = new ulong[64, 1 << 14];
            BinaryToActualMoves = new List<Move>[64, 1 << 12];
            RookBlockerMovesToBinaryMovesDictionary = new Dictionary<ulong, ulong>[64];
            AllBinaryMoves = new HashSet<ulong>[64];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;
                    RookBlockerMovesToBinaryMovesDictionary[square] = new Dictionary<ulong, ulong>();
                    AllBinaryMoves[square] = new HashSet<ulong>();
                    ulong allRookMoves = AllPossibleRookMovesFromAllSquares[i, j];
                    string[,] boardInStringArray = MovesHelper.GetBinaryToBoardInStringArray(allRookMoves);
                    boardInStringArray[7 - i, j] = "WR";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringArray);
                    GenerateAllBlockers(allRookMoves, 0, i, j, board);
                }
            }
        }

        public static void GenerateAllBlockers(ulong allRookMoves, int index, int row, int column, Cell[,] board)
        {
            if (index > 63)
            {
                UpdateActualMoves(allRookMoves, row, column, board);
                return;
            }
            int currentRow = (index / 8);
            int currentColumn = index % 8;

            GenerateAllBlockers(allRookMoves, index + 1, row, column, board);

            if ((allRookMoves & (one << index)) > 0)
            {
                Piece piece = board[currentRow, currentColumn].Piece;
                board[currentRow, currentColumn].Piece = null;
                GenerateAllBlockers(allRookMoves & ~(one << index), index + 1, row, column, board);
                board[currentRow, currentColumn].Piece = piece;
            }
        }

        public static void UpdateActualMoves(ulong rookBlockers, int row, int column, Cell[,] board)
        {
            int square = row * 8 + column;
            Cell cell = board[row, column];
            List<Move> moves = Rook.GetMovesFromCache(board, cell);
            ulong binaryRookMoves = 0;

            foreach (Move move in moves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                binaryRookMoves = binaryRookMoves | one << currentSquare;
            }

            int index = (int)((binaryRookMoves * MagicNumbersForActualMoves[square]) >> (64 - 12));
            BinaryToActualMoves[square, index] = moves;
            AllBinaryMoves[square].Add(binaryRookMoves);

            List<Move> killerMoves = moves.Where(x => board[x.To.Row, x.To.Column].Piece != null).ToList();
            ulong killerBinaryRookMoves = 0;

            foreach (Move move in killerMoves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                killerBinaryRookMoves = killerBinaryRookMoves | one << currentSquare;
            }

            index = (int)((killerBinaryRookMoves * MagicNumbersForActualMoves[square]) >> (64 - 12));
            BinaryToActualMoves[square, index] = killerMoves;
            AllBinaryMoves[square].Add(killerBinaryRookMoves);

            int indexForBlocker = (int)((rookBlockers * MagicNumbersForBlockers[square]) >> (64 - 14));

            RookBlockerMovesToBinaryMoves[square, indexForBlocker] = binaryRookMoves;
            RookBlockerMovesToBinaryMovesDictionary[square][rookBlockers] = binaryRookMoves;
        }

        public static void UpdateAllPossibleMovesForOwnBlockers()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;
                    AllBinaryMoves[square] = new HashSet<ulong>();
                    ulong allRookMoves = AllPossibleRookMovesFromAllSquares[i, j];
                    string[,] boardInStringArray = MovesHelper.GetBinaryToBoardInStringArray(allRookMoves, "WP");
                    boardInStringArray[7 - i, j] = "WR";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringArray);
                    GenerateOwnBlockers(allRookMoves, 0, i, j, board);
                }
            }
        }

        public static void GenerateOwnBlockers(ulong allRookMoves, int index, int row, int column, Cell[,] board)
        {
            if (index > 63)
            {
                UpdateActualMovesForOwnBlockers(allRookMoves, row, column, board);
                return;
            }
            int currentRow = (index / 8);
            int currentColumn = index % 8;

            GenerateOwnBlockers(allRookMoves, index + 1, row, column, board);

            if ((allRookMoves & (one << index)) > 0)
            {
                Piece piece = board[currentRow, currentColumn].Piece;
                board[currentRow, currentColumn].Piece = null;
                GenerateOwnBlockers(allRookMoves & ~(one << index), index + 1, row, column, board);
                board[currentRow, currentColumn].Piece = piece;
            }
        }

        public static void UpdateActualMovesForOwnBlockers(ulong rookBlockers, int row, int column, Cell[,] board)
        {
            int square = row * 8 + column;
            Cell cell = board[row, column];
            List<Move> moves = Rook.GetMovesFromCache(board, cell);
            ulong binaryRookMoves = 0;

            foreach (Move move in moves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                binaryRookMoves = binaryRookMoves | one << currentSquare;
            }

            int index = (int)((binaryRookMoves * MagicNumbersForActualMoves[square]) >> (64 - 12));
            BinaryToActualMoves[square, index] = moves;
            AllBinaryMoves[square].Add(binaryRookMoves);
        }
    }
}
