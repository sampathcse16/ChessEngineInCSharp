using System.Collections.Generic;
using System.Linq;
using ChessEngine.Helpers;
using ChessEngine.Pieces;
using Xunit;

namespace ChessEngine.Tests.PieceMoveTests
{
    public class PawnTests
    {
        [Fact]
        public void PawnTests1()
        {
            string[,] boardInStringFormat =
            {
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "WP", "", "", "", "", ""},
                {"", "", "WP", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[1, 2];
            IList<Move> moves = Pawn.GetMoves(board, cell);
            Assert.True(moves.Count == 0);
        }

        [Fact]
        public void PawnTests2()
        {
            string[,] boardInStringFormat =
            {
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "WP", "", "", "", "", ""}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[0, 2];
            IList<Move> moves = Pawn.GetMoves(board, cell);
            Assert.True(moves.Count == 0);
        }

        [Fact]
        public void PawnTests3()
        {
            string[,] boardInStringFormat =
            {
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "BP", "", "WP", "", "", "", ""},
                {"", "", "WP", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[1, 2];
            IList<Move> moves = Pawn.GetMoves(board, cell);
            Assert.True(moves.Count == 3);

            bool moveExist = moves.Any(x => x.From.Row == 1 && x.From.Column == 2
                                                            && x.To.Row == 2 && x.To.Column == 2);
            Assert.True(moveExist);

            moveExist = moves.Any(x => x.From.Row == 1 && x.From.Column == 2
                                                       && x.To.Row == 3 && x.To.Column == 2);
            Assert.True(moveExist);

            moveExist = moves.Any(x => x.From.Row == 1 && x.From.Column == 2
                                                       && x.To.Row == 2 && x.To.Column == 1);
            Assert.True(moveExist);
        }

        [Fact]
        public void PawnTests4()
        {
            string[,] boardInStringFormat =
            {
                {"", "", "", "", "", "", "", ""},
                {"", "", "BP", "", "", "", "", ""},
                {"", "BP", "", "WP", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[6, 2];
            IList<Move> moves = Pawn.GetMoves(board, cell);
            Assert.True(moves.Count == 3);

            bool moveExist = moves.Any(x => x.From.Row == 6 && x.From.Column == 2
                                                            && x.To.Row == 5 && x.To.Column == 2);
            Assert.True(moveExist);

            moveExist = moves.Any(x => x.From.Row == 6 && x.From.Column == 2
                                                       && x.To.Row == 4 && x.To.Column == 2);
            Assert.True(moveExist);

            moveExist = moves.Any(x => x.From.Row == 6 && x.From.Column == 2
                                                       && x.To.Row == 5 && x.To.Column == 1);
            Assert.True(moveExist);
        }

        [Fact]
        public void PawnTests5()
        {
            string[,] boardInStringFormat =
            {
                {"", "", "", "", "", "", "", ""},
                {"", "", "BP", "", "", "", "", ""},
                {"", "BP", "", "BP", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[6, 2];
            IList<Move> moves = Pawn.GetMoves(board, cell);
            Assert.True(moves.Count == 4);

            bool moveExist = moves.Any(x => x.From.Row == 6 && x.From.Column == 2
                                                            && x.To.Row == 5 && x.To.Column == 2);
            Assert.True(moveExist);

            moveExist = moves.Any(x => x.From.Row == 6 && x.From.Column == 2
                                                       && x.To.Row == 4 && x.To.Column == 2);
            Assert.True(moveExist);

            moveExist = moves.Any(x => x.From.Row == 6 && x.From.Column == 2
                                                       && x.To.Row == 5 && x.To.Column == 1);
            Assert.True(moveExist);

            moveExist = moves.Any(x => x.From.Row == 6 && x.From.Column == 2
                                                       && x.To.Row == 5 && x.To.Column == 3);
            Assert.True(moveExist);
        }

        [Fact]
        public void PawnTests6()
        {
            string[,] boardInStringFormat =
            {
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"WP", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""}
            };

            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);

            Cell cell = board[1, 0];
            IList<Move> moves = Pawn.GetMoves(board, cell);
            Assert.True(moves.Count == 2);
        }
    }
}
