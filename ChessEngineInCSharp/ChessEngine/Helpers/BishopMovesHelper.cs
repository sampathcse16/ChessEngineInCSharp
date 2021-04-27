using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine.Pieces;
using UI.Services;

namespace ChessEngine.Helpers
{
    public class BishopMovesHelper
    {
        static ulong one = 1;

        static int maxRowNumber = 7;

        public static ulong[,] AllPossibleBishopMovesFromAllSquares { get; set; }

        public static List<Move>[] BishopMovesBinaryToActualMoves { get; set; }

        public static ulong[,] BishopBlockerMovesToBinaryMoves { get; set; }

        public static HashSet<ulong> BishopAllBinaryMoves { get; set; }

        public static Dictionary<ulong, ulong>[] BishopBlockerMovesToBinaryMovesDictionary { get; set; }

        public static ulong HashKeyForBishopMoves = 59351;

        public static ulong[] MagicNumbersForBishop =
        {
            9007233631813760
            ,5478774082501640244
            ,4620706412362768384
            ,4573969463115776
            ,1317450500576186528
            ,6765570007500804
            ,287006911373824
            ,2377900878397976576
            ,578163075493037124
            ,1254577261830272
            ,78830586772335616
            ,54711701282625664
            ,306527357773021472
            ,72093328174236177
            ,24822580952191488
            ,2306045456861435010
            ,2308104979516269328
            ,562950494519952
            ,2260632481115408
            ,1450194273017270784
            ,1153273351817658448
            ,562984339660800
            ,2395916103454298432
            ,5514805117440
            ,4504183743971585
            ,4785635097592064
            ,19997636692128
            ,3315848972354
            ,5774179879891502080
            ,844429258949120
            ,2450029674136478276
            ,7494344459686003233
            ,4755942496434536512
            ,18023207559495944
            ,586593868663755776
            ,240518503684
            ,35753723707648
            ,36802861794984064
            ,18036663623107584
            ,216243176629862720
            ,1154047997789620512
            ,576461304208819350
            ,721555176816672
            ,4620838387845695488
            ,184647602439242112
            ,26405593235700
            ,3659742707190112
            ,3332899883346
            ,2882307062216278024
            ,2456150681170494464
            ,72374597529600000
            ,2305851811757556016
            ,580973148174821376
            ,2882866739119439936
            ,27022700801228800
            ,1443579671169221312
            ,328775966971429378
            ,2253998841144368
            ,18017835575937024
            ,9072581615624
            ,576460825854763072
            ,4506213115250952
            ,2305985398121244936
            ,10445969305744
        };

        public static void UpdateAllPossibleMovesFromAllSquares()
        {
            AllPossibleBishopMovesFromAllSquares = new ulong[8, 8];
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

                    boardInStringFormat[row, column] = "WB";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
                    Cell cell = board[i, j];
                    List<Move> moves = Bishop.GetMovesFromCache(board, cell);
                    ulong bishopMoves = 0;

                    foreach (Move move in moves)
                    {
                        int square = move.To.Row * 8 + move.To.Column;
                        bishopMoves = bishopMoves | one << square;
                    }

                    AllPossibleBishopMovesFromAllSquares[i, j] = bishopMoves;
                    boardInStringFormat[row, column] = "  ";
                    MovesHelper.GetBinaryString(bishopMoves);
                }
            }
        }

        public static void UpdateAllPossibleMovesForAllBlockers()
        {

            BishopBlockerMovesToBinaryMoves = new ulong[64, 1 << 14];
            BishopMovesBinaryToActualMoves = new List<Move>[HashKeyForBishopMoves];
            BishopBlockerMovesToBinaryMovesDictionary = new Dictionary<ulong, ulong>[64];
            BishopAllBinaryMoves = new HashSet<ulong>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;
                    BishopBlockerMovesToBinaryMovesDictionary[square] = new Dictionary<ulong, ulong>();
                    ulong allBishopMoves = AllPossibleBishopMovesFromAllSquares[i, j];
                    string[,] boardInStringArray = MovesHelper.GetBinaryToBoardInStringArray(allBishopMoves);
                    boardInStringArray[7 - i, j] = "WB";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringArray);
                    GenerateAllBlockers(allBishopMoves, 0, i, j, board);
                }
            }
        }

        public static void GenerateAllBlockers(ulong allBishopMoves, int index, int row, int column, Cell[,] board)
        {
            if (index > 63)
            {
                UpdateActualMoves(allBishopMoves, row, column, board);
                return;
            }
            int currentRow = (index / 8);
            int currentColumn = index % 8;

            GenerateAllBlockers(allBishopMoves, index + 1, row, column, board);

            if ((allBishopMoves & (one << index)) > 0)
            {
                Piece piece = board[currentRow, currentColumn].Piece;
                board[currentRow, currentColumn].Piece = null;
                GenerateAllBlockers(allBishopMoves & ~(one << index), index + 1, row, column, board);
                board[currentRow, currentColumn].Piece = piece;
            }
        }

        public static void UpdateActualMoves(ulong bishopBlockers, int row, int column, Cell[,] board)
        {
            int square = row * 8 + column;
            Cell cell = board[row, column];
            List<Move> moves = Bishop.GetMovesFromCache(board, cell);
            ulong binaryBishopMoves = 0;

            foreach (Move move in moves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                binaryBishopMoves = binaryBishopMoves | one << currentSquare;
            }

            int index = (int)(binaryBishopMoves % HashKeyForBishopMoves);
            BishopMovesBinaryToActualMoves[index] = moves;

            int indexForBlocker = (int)((bishopBlockers * MagicNumbersForBishop[square]) >> (64 - 14));

            BishopAllBinaryMoves.Add(binaryBishopMoves);
            BishopBlockerMovesToBinaryMoves[square, indexForBlocker] = binaryBishopMoves;
            BishopBlockerMovesToBinaryMovesDictionary[square][bishopBlockers] = binaryBishopMoves;
        }
    }
}
