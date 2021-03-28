using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace ChessEngine.Tests.ChessEngineTests
{
    public class ChessEngineTests
    {
        private readonly ITestOutputHelper output;

        public ChessEngineTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ChessEngineTests1()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "BN", "BB", "BQ", "BK", "BB", "  ", "BR"},
                {"BP", "BP", "  ", "  ", "BP", "BP", "BP", "BP"},
                {"  ", "  ", "BP", "  ", "  ", "BN", "  ", "  "},
                {"  ", "  ", "  ", "BP", "  ", "WQ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "WP", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"WP", "WP", "WP", "WP", "  ", "WP", "WP", "WP"},
                {"WR", "WN", "WB", "  ", "WK", "WB", "WN", "WR"}
            };

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            Node node = new Node();
            Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 4, true, 0, null, null);
            Move move = node.Moves.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            output.WriteLine(elapsedMs.ToString());

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                move = node.Moves?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();
            }

        }
    }
}
