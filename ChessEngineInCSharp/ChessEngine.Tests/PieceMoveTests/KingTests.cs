using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Helpers;
using ChessEngine.Pieces;
using UI.Services;
using Xunit;
using Xunit.Abstractions;

namespace ChessEngine.Tests.PieceMoveTests
{
    public class KingTests
    {
        private readonly ITestOutputHelper output;

        public KingTests(ITestOutputHelper output)
        {
            this.output = output;
            CacheService cacheService = new CacheService();
            cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
        }

        [Fact]
        public void KingTests1()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WK", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = King.GetMoves(board, cell);
            Assert.True(moves.Count == 8);
        }

        [Fact]
        public void KingTests2()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WK", "WP", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = King.GetMoves(board, cell);
            Assert.True(moves.Count == 7);
        }

        [Fact]
        public void KingTests3()
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
                {"WK", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[0, 0];
            IList<Move> moves = King.GetMoves(board, cell);
            Assert.True(moves.Count == 3);
        }

        [Fact]
        public void KingTests4()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WK"},
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
            IList<Move> moves = King.GetMoves(board, cell);
            Assert.True(moves.Count == 3);
        }

        [Fact]
        public void KingTests5()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WK"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "BP", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[7, 7];
            IList<Move> moves = King.GetMoves(board, cell);
            Assert.True(moves.Count == 3);
        }

        [Fact]
        public void KingTests6()
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
                {"  ", "  ", "  ", "  ", "WK", "  ", "  ", "WR"}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            Cell cell = board[0, 4];
            IList<Move> moves = King.GetMovesFromCache(board, cell);

            bool moveExist = moves.Any(x => x.From.Row == 0 && x.From.Column == 4
                                                            && x.To.Row == 0 && x.To.Column == 6);
            Assert.True(moveExist);

            Assert.True(moves.Count == 6);
        }

        [Fact]
        public void KingTests7()
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
                {"WR", "  ", "  ", "  ", "WK", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            Cell cell = board[0, 4];
            IList<Move> moves = King.GetMovesFromCache(board, cell);

            bool moveExist = moves.Any(x => x.From.Row == 0 && x.From.Column == 4
                                                            && x.To.Row == 0 && x.To.Column == 2);
            Assert.True(moveExist);

            Assert.True(moves.Count == 6);
        }

        [Fact]
        public void KingTests8()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "BK", "  ", "  ", "BR"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            Cell cell = board[7, 4];
            IList<Move> moves = King.GetMovesFromCache(board, cell);

            bool moveExist = moves.Any(x => x.From.Row == 7 && x.From.Column == 4
                                                            && x.To.Row == 7 && x.To.Column == 6);
            Assert.True(moveExist);

            Assert.True(moves.Count == 6);
        }

        [Fact]
        public void KingTests9()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "  ", "  ", "  ", "BK", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            Cell cell = board[7, 4];
            IList<Move> moves = King.GetMovesFromCache(board, cell);

            bool moveExist = moves.Any(x => x.From.Row == 7 && x.From.Column == 4
                                                            && x.To.Row == 7 && x.To.Column == 2);
            Assert.True(moveExist);

            Assert.True(moves.Count == 6);
        }


        [Fact]
        public void KingTests10()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "BK", "BR", "  ", "BB", "  ", "BR"},
                {"BP", "BP", "BP", "  ", "  ", "BP", "BP", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "BP", "  "},
                {"  ", "  ", "BN", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "WN", "  ", "  ", "  "},
                {"WP", "WP", "  ", "WP", "  ", "WP", "WP", "WP"},
                {"WR", "  ", "WB", "  ", "WK", "  ", "  ", "WR"}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            Cell cell = board[0, 4];
            IList<Move> moves = King.GetMovesFromCache(board, cell);

            bool moveExist = moves.Any(x => x.From.Row == 7 && x.From.Column == 4
                                                            && x.To.Row == 7 && x.To.Column == 2);
            Assert.True(moveExist);

            Assert.True(moves.Count == 6);
        }

        [Fact]
        public void KnightTests6()
        {
            KingMovesHelper.KingMovesBinaryToActualMoves = new List<Move>[KingMovesHelper.HashKeyForKingMoves];
            KingMovesHelper.UpdateAllPossibleKingMovesFromAllSquares();
            KingMovesHelper.UpdateAllPossibleKingMovesForAllBlockers();

            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WK", "  ", "  ", "  ", "  "},
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
            ulong kingMask = KingMovesHelper.AllPossibleKingMovesFromAllSquares[row, column];
            ulong occupancy = BoardHelper.GetOccupancy(board);
            ulong ownBlockers = 0;//one << 46;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            //for (ulong i = 1; i < 1000000; i++)
            //{
            //    bool found = true;
            //    HashSet<int> indexes = new HashSet<int>();

            //    foreach (ulong binaryMove in KingMovesHelper.KingAllBinaryMoves)
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
            //    Dictionary<ulong, ulong> blockerMovesToBinaryMoves = KingMovesHelper.KingBlockerMovesToBinaryMovesDictionary[i];
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
                List<Move> moves = King.GetKingMovesFromMagicBitboards(square, kingMask, ownBlockers);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
        }
    }
}
