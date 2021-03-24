using System;
using System.Collections.Generic;
using System.Text;
using ChessEngine.Helpers;
using ChessEngine.Moves;
using Xunit;

namespace ChessEngine.Tests
{
    public class BishopTests
    {
        [Fact]
        public void BishopTests1()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "BB","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "}
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
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "WP", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "BB","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "}
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
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "BP", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "BB","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "}
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
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "WP", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "WB","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "},
                {"  ", "  ", "  ", "  ","  ", "  ", "  "}
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
    }
}
