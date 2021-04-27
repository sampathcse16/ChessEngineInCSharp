using System;
using System.Collections.Generic;
using ChessEngine.Helpers;
using ChessEngine.Pieces;
using UI.Services;
using Xunit;
using Xunit.Abstractions;

namespace ChessEngine.Tests.PieceMoveTests
{
    public class BishopTests
    {
        private readonly ITestOutputHelper output;

        public BishopTests(ITestOutputHelper output)
        {
            this.output = output;
            CacheService cacheService = new CacheService();
            cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
        }

        [Fact]
        public void BishopTests1()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "BB","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Bishop.GetMoves(board, cell);
            Assert.True(moves.Count == 13);
        }

        [Fact]
        public void BishopTests2()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "WP", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "BB","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Bishop.GetMoves(board, cell);
            Assert.True(moves.Count == 11);
        }

        [Fact]
        public void BishopTests3()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "BP", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "BB","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Bishop.GetMoves(board, cell);
            Assert.True(moves.Count == 10);
        }

        [Fact]
        public void BishopTests4()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "WP", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WB","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Bishop.GetMoves(board, cell);
            Assert.True(moves.Count == 10);
        }

        [Fact]
        public void BishopTests5()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "WP", "  ", "BB", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WB", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Bishop.GetMoves(board, cell);
            Assert.True(moves.Count == 8);
        }

        [Fact]
        public void BishopTests6()
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
                {"WB", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[0, 0];
            IList<Move> moves = Bishop.GetMoves(board, cell);
            Assert.True(moves.Count == 7);
        }

        [Fact]
        public void BishopTests7()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WB"},
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
            IList<Move> moves = Bishop.GetMoves(board, cell);
            Assert.True(moves.Count == 7);
        }

        [Fact]
        public void BishopTests8()
        {
            string[,] boardInStringFormat =
            {
                {"WB", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[7, 0];
            IList<Move> moves = Bishop.GetMoves(board, cell);
            Assert.True(moves.Count == 7);
        }

        [Fact]
        public void BishopTests9()
        {
            string[,] boardInStringFormat =
            {
                {"WB", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WK"}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[7, 0];
            bool isOpponentKingIsInCheck = Bishop.IsOpponentKingIsInCheck(board, cell);
            Assert.True(!isOpponentKingIsInCheck);
        }

        [Fact]
        public void BishopTests10()
        {
            string[,] boardInStringFormat =
            {
                {"BB", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WK"}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[7, 0];
            bool isOpponentKingIsInCheck = Bishop.IsOpponentKingIsInCheck(board, cell);
            Assert.True(isOpponentKingIsInCheck);
        }

        [Fact]
        public void BishopTests11()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "BK"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"WB", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[0, 0];
            bool isOpponentKingIsInCheck = Bishop.IsOpponentKingIsInCheck(board, cell);
            Assert.True(isOpponentKingIsInCheck);
        }

        [Fact]
        public void BishopTests12()
        {
            //649579
            BishopMovesHelper.BishopMovesBinaryToActualMoves = new List<Move>[BishopMovesHelper.HashKeyForBishopMoves];
            BishopMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            BishopMovesHelper.UpdateAllPossibleMovesForAllBlockers();

            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WB", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            ulong one = 1;
            int row = 4;
            int column = 3;
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            int square = row * 8 + column;
            ulong bishopMask = BishopMovesHelper.AllPossibleBishopMovesFromAllSquares[row, column];
            ulong occupancy = BoardHelper.GetOccupancy(board);
            ulong ownBlockers = one << 3;
            ownBlockers = ownBlockers | one << 59;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            //for (ulong i = 1; i < 1000000; i++)
            //{
            //    bool found = true;
            //    HashSet<int> indexes = new HashSet<int>();

            //    foreach (ulong binaryMove in BishopMovesHelper.BishopAllBinaryMoves)
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
            //    Dictionary<ulong, ulong> blockerMovesToBinaryMoves = BishopMovesHelper.BishopBlockerMovesToBinaryMovesDictionary[i];
            //    Random random = new Random();
            //    bool found = false;
            //    ulong magicNumber = 0;

            //    while (!found)
            //    {
            //        found = true;
            //        magicNumber = (ulong)(MovesHelper.LongRandom(0, Int64.MaxValue, random)
            //            & MovesHelper.LongRandom(0, Int64.MaxValue, random)
            //            & MovesHelper.LongRandom(0, Int64.MaxValue, random));
            //        ulong[] blockerIndexes = new ulong[1 << 14];

            //        foreach (ulong key in blockerMovesToBinaryMoves.Keys)
            //        {
            //            int index = (int)((key * magicNumber) >> (64 - 14));

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
                List<Move> moves = Bishop.GetBishopMovesFromMagicBitboards(square, bishopMask, occupancy, ownBlockers);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
        }

        [Fact]
        public void BishopTests13()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WB", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];

            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < 10000000; i++)
            {
                IList<Move> moves = Bishop.GetMovesFromCache(board, cell);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
        }
    }
}
