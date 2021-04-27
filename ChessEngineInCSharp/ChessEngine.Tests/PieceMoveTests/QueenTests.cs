using System.Collections.Generic;
using ChessEngine.Helpers;
using ChessEngine.Pieces;
using UI.Services;
using Xunit;
using Xunit.Abstractions;

namespace ChessEngine.Tests.PieceMoveTests
{
    public class QueenTests
    {
        private readonly ITestOutputHelper output;

        public QueenTests(ITestOutputHelper output)
        {
            this.output = output;
            CacheService cacheService = new CacheService();
            cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
        }

        [Fact]
        public void QueenTests1()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WQ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Queen.GetMoves(board, cell);
            Assert.True(moves.Count == 27);
        }

        [Fact]
        public void QueenTests2()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "BP", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WQ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Queen.GetMoves(board, cell);
            Assert.True(moves.Count == 25);
        }

        [Fact]
        public void QueenTests3()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "WP", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WQ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Queen.GetMoves(board, cell);
            Assert.True(moves.Count == 24);
        }

        [Fact]
        public void QueenTests4()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "WP", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WQ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "WP", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            IList<Move> moves = Queen.GetMoves(board, cell);
            Assert.True(moves.Count == 21);
        }

        [Fact]
        public void QueenTests5()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "BK", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "WP", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WQ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "WP", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[4, 3];
            bool isOpponentKingIsInCheck = Queen.IsOpponentKingIsInCheck(board, cell);
            Assert.True(isOpponentKingIsInCheck);
        }

        [Fact]
        public void QueenTests6()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WQ", "  ", "  ", "  ", "  "},
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
                IList<Move> moves = Queen.GetMovesFromCache(board, cell);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
        }

        [Fact]
        public void QueenTests7()
        {
            RookMovesHelper.RookMovesBinaryToActualMoves = new List<Move>[RookMovesHelper.HashKeyForRookMoves];
            RookMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            RookMovesHelper.UpdateAllPossibleMovesForAllBlockers();

            BishopMovesHelper.BishopMovesBinaryToActualMoves = new List<Move>[BishopMovesHelper.HashKeyForBishopMoves];
            BishopMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            BishopMovesHelper.UpdateAllPossibleMovesForAllBlockers();

            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WQ", "  ", "  ", "  ", "  "},
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
            ulong rookMask = RookMovesHelper.AllPossibleRookMovesFromAllSquares[row, column];
            ulong occupancy = BoardHelper.GetOccupancy(board);
            ulong ownBlockers = 0;
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
                moves.AddRange(Rook.GetRookMovesFromMagicBitboards(square, rookMask, occupancy, ownBlockers));
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
        }
    }
}
