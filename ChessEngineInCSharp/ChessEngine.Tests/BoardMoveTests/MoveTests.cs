using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ChessEngine.Helpers;
using ChessEngine.Pieces;
using Xunit;
using Xunit.Abstractions;

namespace ChessEngine.Tests.BoardMoveTests
{
    public class MoveTests
    {
        private readonly ITestOutputHelper output;

        public MoveTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestBoardMoves1()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "BN", "BB", "BQ", "BK", "BB", "BN", "BR"},
                {"BP", "BP", "BP", "BP", "BP", "BP", "BP", "BP"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"WP", "WP", "WP", "WP", "WP", "WP", "WP", "WP"},
                {"WR", "WN", "WB", "WQ", "WK", "WB", "WN", "WR"}
            };
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            IList<Move> moves = null;

            for (int i = 0; i < 64000; i++)
            {
                Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
                moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion1(board, true);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
            Assert.True(moves.Count == 20);
        }

        [Fact]
        public void TestBoardMoves2()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "BN", "BB", "BQ", "BK", "BB", "BN", "BR"},
                {"BP", "BP", "BP", "BP", "BP", "BP", "BP", "BP"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"WP", "WP", "WP", "WP", "WP", "WP", "WP", "WP"},
                {"WR", "WN", "WB", "WQ", "WK", "WB", "WN", "WR"}
            };

            var watch = System.Diagnostics.Stopwatch.StartNew();
            IList<Move> moves = null;

            for (int i = 0; i < 64000; i++)
            {
                Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
                moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion2(board, true);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
            Assert.True(moves.Count == 20);
        }

        [Fact]
        public void TestBoardMoves3()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "BN", "BB", "BQ", "BK", "BB", "BN", "BR"},
                {"BP", "BP", "BP", "BP", "BP", "BP", "BP", "BP"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"WP", "WP", "WP", "WP", "WP", "WP", "WP", "WP"},
                {"WR", "WN", "WB", "WQ", "WK", "WB", "WN", "WR"}
            };

            var watch = System.Diagnostics.Stopwatch.StartNew();
            IList<Move> moves = null;

            for (int i = 0; i < 64000; i++)
            {
                Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
                moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, true);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());
            Assert.True(moves.Count == 20);
        }
    }
}
