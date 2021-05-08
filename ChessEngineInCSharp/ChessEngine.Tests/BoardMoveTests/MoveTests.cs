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
            BishopMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            BishopMovesHelper.UpdateAllPossibleMovesForAllBlockers();
            BishopMovesHelper.UpdateAllPossibleMovesForOwnBlockers();
            RookMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            RookMovesHelper.UpdateAllPossibleMovesForAllBlockers();
            RookMovesHelper.UpdateAllPossibleMovesForOwnBlockers();
            KnightMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            KnightMovesHelper.UpdateAllPossibleMovesForAllBlockers();
            KingMovesHelper.UpdateAllPossibleKingMovesFromAllSquares();
            KingMovesHelper.UpdateAllPossibleKingMovesForAllBlockers();

            string[,] boardInStringFormat =
            {
                {"BR", "  ", "BB", "BQ", "BK", "BB", "BN", "BR"},
                {"BP", "BP", "BP", "  ", "  ", "BP", "BP", "BP"},
                {"  ", "BN", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "BP", "BP", "  ", "  ", "  "},
                {"  ", "  ", "WN", "WP", "WP", "  ", "WN", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "WP", "WP", "  ", "  ", "WP", "WP", "  "},
                {"WR", "  ", "WB", "WQ", "WK", "WB", "  ", "WR"}
            };

            var watch = System.Diagnostics.Stopwatch.StartNew();
            IList<Move> moves = null;
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            for (int i = 0; i < 100000; i++)
            {
                moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, true);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());

            watch = System.Diagnostics.Stopwatch.StartNew();
            moves = null;
            ChessEngine.Engine.ChessEngine.UpdateOccupancies(board);

            ulong occupancyForWhite = ChessEngine.Engine.ChessEngine.OccupancyForWhite;
            ulong occupancyForBlack = ChessEngine.Engine.ChessEngine.OccupancyForBlack;

            for (int i = 0; i < 100000; i++)
            {
                ChessEngine.Engine.ChessEngine.OccupancyForWhite = occupancyForWhite;
                ChessEngine.Engine.ChessEngine.OccupancyForBlack = occupancyForBlack;
                moves = MovesHelper.GetAllMovesForCurrentTurnUsingBitboards(board, true);
            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
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
