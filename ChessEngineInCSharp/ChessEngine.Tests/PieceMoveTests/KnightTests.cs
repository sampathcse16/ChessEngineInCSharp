using System;
using System.Collections.Generic;
using ChessEngine.Helpers;
using ChessEngine.Pieces;
using UI.Services;
using Xunit;
using Xunit.Abstractions;

namespace ChessEngine.Tests.PieceMoveTests
{
    public class KnightTests
    {
        private readonly ITestOutputHelper output;

        public KnightTests(ITestOutputHelper output)
        {
            this.output = output;
            CacheService cacheService = new CacheService();
            cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
        }

        [Fact]
        public void KnightTests1()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WN", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Knight.GetMoves(board, cell);
            Assert.True(moves.Count == 8);
        }

        [Fact]
        public void KnightTests2()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "BP", "  ", "  "},
                {"  ", "  ", "  ", "WN", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Knight.GetMoves(board, cell);
            Assert.True(moves.Count == 8);
        }

        [Fact]
        public void KnightTests3()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"WN", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[0, 0];
            IList<Move> moves = Knight.GetMoves(board, cell);
            Assert.True(moves.Count == 2);
        }

        [Fact]
        public void KnightTests4()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WN"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[7, 7];
            IList<Move> moves = Knight.GetMoves(board, cell);
            Assert.True(moves.Count == 2);
        }

        [Fact]
        public void KnightTests5()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WN"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "BK", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[7, 7];
            bool isOpponentKingIsInCheck = Knight.IsOpponentKingIsInCheck(board, cell);
            Assert.True(isOpponentKingIsInCheck);
        }

        [Fact]
        public void KnightTests6()
        {
            KnightMovesHelper.KnightMovesBinaryToActualMoves = new List<Move>[KnightMovesHelper.HashKeyForKnightMoves];
            KnightMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            KnightMovesHelper.UpdateAllPossibleMovesForAllBlockers();

            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WN"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "BK", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            ulong one = 1;
            int row = 7;
            int column = 7;
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            int square = row * 8 + column;
            ulong rookMask = KnightMovesHelper.AllPossibleKnightMovesFromAllSquares[row, column];
            ulong occupancy = BoardHelper.GetOccupancy(board);
            ulong ownBlockers = 0;//one << 46;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            //for (ulong i = 1; i < 1000000; i++)
            //{
            //    bool found = true;
            //    HashSet<int> indexes = new HashSet<int>();

            //    foreach (ulong binaryMove in KnightMovesHelper.KnightAllBinaryMoves)
            //    {
            //        int index = (int)(binaryMove % i);

            //        if (indexes.Contains(index))
            //        {
            //            found = false;
            //            break;
            //        }
            //        else
            //        {
            //            indexes.Add(index);
            //        }
            //    }

            //    if (found)
            //    {

            //    }
            //}


            //for (int i = 0; i < 64; i++)
            //{
            //    Dictionary<ulong, ulong> blockerMovesToBinaryMoves = KnightMovesHelper.KnightBlockerMovesToBinaryMovesDictionary[i];
            //    Random random = new Random();
            //    bool found = false;
            //    ulong magicNumber = 0;

            //    while (!found)
            //    {
            //        found = true;
            //        magicNumber = (ulong)(MovesHelper.LongRandom(0, Int64.MaxValue, random)
            //            & MovesHelper.LongRandom(0, Int64.MaxValue, random)
            //            & MovesHelper.LongRandom(0, Int64.MaxValue, random));
            //        ulong[] blockerIndexes = new ulong[1 << 8];

            //        foreach (ulong key in blockerMovesToBinaryMoves.Keys)
            //        {
            //            int index = (int)((key * magicNumber) >> (64 - 8));

            //            if (blockerIndexes[index] != 0)
            //            {
            //                if (blockerIndexes[index] != blockerMovesToBinaryMoves[key])
            //                {
            //                    found = false;
            //                    break;
            //                }
            //            }
            //            else
            //            {
            //                blockerIndexes[index] = blockerMovesToBinaryMoves[key];
            //            }
            //        }
            //    }

            //    output.WriteLine(magicNumber.ToString());
            //}


            for (int i = 0; i < 10000000; i++)
            {
                List<Move> moves = Knight.GetKnightMovesFromMagicBitboards(square, rookMask,  ownBlockers);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
        }

        [Fact]
        public void KnightTests7()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WN"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "BK", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[7, 7];
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (int i = 0; i < 10000000; i++)
            {
                List<Move> moves = Knight.GetMovesFromCache(board, cell);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
        }
    }
}
