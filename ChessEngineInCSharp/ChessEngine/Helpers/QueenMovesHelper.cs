using System;
using System.Collections.Generic;
using System.Text;
using ChessEngine.Pieces;

namespace ChessEngine.Helpers
{
    public class QueenMovesHelper
    {
        static ulong one = 1;

        static int maxRowNumber = 7;

        public static ulong[,] AllPossibleQueenMovesFromAllSquares { get; set; }

        public static List<Move>[] QueenMovesBinaryToActualMoves { get; set; }

        public static ulong[,] QueenBlockerMovesToBinaryMoves { get; set; }
        public static Dictionary<ulong, ulong>[] QueenBlockerMovesToBinaryMovesDictionary { get; set; }

        public static ulong HashKeyForQueenMoves = 649579;

        public static ulong[] MagicNumbersForQueen =
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

        public static void UpdateAllPossibleQueenMovesFromAllSquares()
        {
            AllPossibleQueenMovesFromAllSquares = new ulong[8, 8];
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

                    boardInStringFormat[row, column] = "WQ";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
                    Cell cell = board[i, j];
                    List<Move> moves = Queen.GetMovesFromCache(board, cell);
                    ulong queenMoves = 0;

                    foreach (Move move in moves)
                    {
                        int square = move.To.Row * 8 + move.To.Column;
                        queenMoves = queenMoves | one << square;
                    }

                    AllPossibleQueenMovesFromAllSquares[i, j] = queenMoves;
                    boardInStringFormat[row, column] = "  ";
                    MovesHelper.GetBinaryString(queenMoves);
                }
            }
        }

        public static void UpdateAllPossibleQueenMovesForAllBlockers()
        {

            QueenBlockerMovesToBinaryMoves = new ulong[64, 1 << 14];
            QueenMovesBinaryToActualMoves = new List<Move>[HashKeyForQueenMoves];
            QueenBlockerMovesToBinaryMovesDictionary = new Dictionary<ulong, ulong>[64];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;
                    QueenBlockerMovesToBinaryMovesDictionary[square] = new Dictionary<ulong, ulong>();
                    ulong allQueenMoves = AllPossibleQueenMovesFromAllSquares[i, j];
                    string[,] boardInStringArray = MovesHelper.GetBinaryToBoardInStringArray(allQueenMoves);
                    boardInStringArray[7 - i, j] = "WQ";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringArray);
                    GenerateAllBlockers(allQueenMoves, 0, i, j, board);
                }
            }
        }

        public static void GenerateAllBlockers(ulong allQueenMoves, int index, int row, int column, Cell[,] board)
        {
            if (index > 63)
            {
                UpdateActualMoves(allQueenMoves, row, column, board);
                return;
            }
            int currentRow = (index / 8);
            int currentColumn = index % 8;

            GenerateAllBlockers(allQueenMoves, index + 1, row, column, board);

            if ((allQueenMoves & (one << index)) > 0)
            {
                Piece piece = board[currentRow, currentColumn].Piece;
                board[currentRow, currentColumn].Piece = null;
                GenerateAllBlockers(allQueenMoves & ~(one << index), index + 1, row, column, board);
                board[currentRow, currentColumn].Piece = piece;
            }
        }

        public static void UpdateActualMoves(ulong queenBlockers, int row, int column, Cell[,] board)
        {
            int square = row * 8 + column;
            Cell cell = board[row, column];
            List<Move> moves = Queen.GetMovesFromCache(board, cell);
            ulong binaryQueenMoves = 0;

            if (queenBlockers == 8 && square == 27)
            {

            }

            foreach (Move move in moves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                binaryQueenMoves = binaryQueenMoves | one << currentSquare;
            }

            int index = (int)(binaryQueenMoves % HashKeyForQueenMoves);
            QueenMovesBinaryToActualMoves[index] = moves;

            int indexForBlocker = (int)((queenBlockers * MagicNumbersForQueen[square]) >> (64 - 14));

            if (square == 27 && indexForBlocker == 256)
            {

            }

            QueenBlockerMovesToBinaryMoves[square, indexForBlocker] = binaryQueenMoves;
            QueenBlockerMovesToBinaryMovesDictionary[square][queenBlockers] = binaryQueenMoves;
        }
    }
}
