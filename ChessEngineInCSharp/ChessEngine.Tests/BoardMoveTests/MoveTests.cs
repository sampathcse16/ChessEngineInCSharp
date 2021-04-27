using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text;
using ChessEngine.Helpers;
using ChessEngine.Pieces;
using UI.Services;
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
            CacheService cacheService = new CacheService();
            cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
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

        [Fact]
        public void TestBoardMove4()
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
                {"WR", "WN", "WB", "WQ", "WK", "  ", "  ", "WR"}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Move castleMove = new Move{From = new Position{Row = 0, Column = 4}, To=new Position{Row = 0, Column = 6}};
            ChessEngine.Engine.ChessEngine.MakeMove(board, castleMove);
            string afterMove = BoardHelper.GetBoardAsString(board);
            ChessEngine.Engine.ChessEngine.RevertMove(board, castleMove, null);
            string afterRevertMove = BoardHelper.GetBoardAsString(board);
        }

        [Fact]
        public void TestBoardMove5()
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
                {"WR", "  ", "  ", "  ", "WK", "WB", "WN", "WR"}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Move castleMove = new Move { From = new Position { Row = 0, Column = 4 }, To = new Position { Row = 0, Column = 2 } };
            ChessEngine.Engine.ChessEngine.MakeMove(board, castleMove);
            string afterMove = BoardHelper.GetBoardAsString(board);
            ChessEngine.Engine.ChessEngine.RevertMove(board, castleMove, null);
            string afterRevertMove = BoardHelper.GetBoardAsString(board);
        }
    }
}
