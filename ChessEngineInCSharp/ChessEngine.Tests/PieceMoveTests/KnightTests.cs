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
            KnightMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            KnightMovesHelper.UpdateAllPossibleMovesForAllBlockers();

            string[,] boardInStringFormat =
            {
                {"BR", "BN", "BB", "BQ", "BK", "BB", "BN", "BR"},
                {"BP", "BP", "BP", "BP", "BP", "BP", "BP", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WP", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "BP", "  ", "BP"},
                {"WP", "WP", "WP", "  ", "WP", "WP", "WP", "WP"},
                {"WR", "WN", "WB", "WQ", "WK", "WB", "WN", "WR"}
            };

            ulong one = 1;
            int row = 0;
            int column = 6;
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            int square = row * 8 + column;
            ulong rookMask = KnightMovesHelper.AllPossibleMovesFromAllSquares[row, column];
            ulong occupancy = BoardHelper.GetOccupancy(board);
            ulong ownBlockers = one << 0;
             ownBlockers = ownBlockers | one << 1;
            ownBlockers = ownBlockers | one << 2;
            ownBlockers = ownBlockers | one << 3;
            ownBlockers = ownBlockers | one << 4;
            ownBlockers = ownBlockers | one << 5;
            ownBlockers = ownBlockers | one << 6;
            ownBlockers = ownBlockers | one << 7;
            ownBlockers = ownBlockers | one << 8;
            ownBlockers = ownBlockers | one << 9;
            ownBlockers = ownBlockers | one << 10;
            ownBlockers = ownBlockers | one << 12;
            ownBlockers = ownBlockers | one << 13;
            ownBlockers = ownBlockers | one << 14;
            ownBlockers = ownBlockers | one << 15;
            ownBlockers = ownBlockers | one << 27;

            ulong opponentBlockers = 0;

            opponentBlockers = opponentBlockers | one << 21;
            opponentBlockers = opponentBlockers | one << 23;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            //for (int i = 0; i < 64; i++)
            //{
            //    Random random = new Random();
            //    bool found = false;
            //    ulong magicNumber = 0;

            //    while (!found)
            //    {
            //        found = true;
            //        HashSet<int> indexes = new HashSet<int>();
            //        magicNumber = (ulong)(MovesHelper.LongRandom(0, Int64.MaxValue, random)
            //                              & MovesHelper.LongRandom(0, Int64.MaxValue, random)
            //                              & MovesHelper.LongRandom(0, Int64.MaxValue, random));

            //        foreach (ulong binaryMove in KnightMovesHelper.AllBinaryMoves[i])
            //        {
            //            int index = (int)((binaryMove * magicNumber) >> (64 - 8));

            //            if (indexes.Contains(index))
            //            {
            //                found = false;
            //                break;
            //            }
            //            else
            //            {
            //                indexes.Add(index);
            //            }
            //        }

            //        if (found)
            //        {
            //            output.WriteLine(magicNumber.ToString());
            //            break;
            //        }
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
                List<Move> moves = Knight.GetKillingMovesFromMagicBitboards(square, rookMask,  ownBlockers, opponentBlockers);
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
