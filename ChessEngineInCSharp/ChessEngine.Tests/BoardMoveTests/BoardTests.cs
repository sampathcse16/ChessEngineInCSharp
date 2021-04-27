using System;
using System.Collections.Generic;
using System.Text;
using ChessEngine.Helpers;
using UI.Services;
using Xunit;
using Xunit.Abstractions;

namespace ChessEngine.Tests.BoardMoveTests
{
    public class BoardTests
    {
        private readonly ITestOutputHelper output;

        public BoardTests(ITestOutputHelper output)
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

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            string[,] bordArrayCopy = BoardHelper.GetBoardAsStringArray(board);
            Cell[,] bordCopy = BoardHelper.GetBoard(bordArrayCopy);
            string boardAsString = BoardHelper.GetBoardAsString(bordCopy);
        }
    }
}
