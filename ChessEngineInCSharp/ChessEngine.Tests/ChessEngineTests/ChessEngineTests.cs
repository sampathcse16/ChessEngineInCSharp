using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine.Helpers;
using UI.Services;
using Xunit;
using Xunit.Abstractions;

namespace ChessEngine.Tests.ChessEngineTests
{
    public class ChessEngineTests
    {
        private readonly ITestOutputHelper _output;
        private readonly CacheService _cacheService;

        public ChessEngineTests(ITestOutputHelper output)
        {
            this._output = output;
            this._cacheService = new CacheService();
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
            Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 4, true, 0, 0, null, null);
            Move move = node.Moves.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                move = node.Moves?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();
            }

        }

        [Fact]
        public void ChessEngineTests2()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "BN", "BB", "  ", "BK", "  ", "BR", "  "},
                {"BP", "BP", "  ", "BP", "  ", "  ", "  ", "BP"},
                {"  ", "  ", "  ", "BP", "BP", "WQ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "BP", "  ", "  "},
                {"  ", "  ", "  ", "WP", "  ", "  ", "  ", "  "},
                {"  ", "  ", "WN", "  ", "  ", "  ", "  ", "  "},
                {"WP", "BQ", "WP", "  ", "WP", "WP", "WP", "WP"},
                {"WR", "  ", "  ", "  ", "WK", "WB", "WN", "WR"}
            };

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            _cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            ZorbistData.FillZorbistData(board);
            long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(board);
            ChessEngine.Engine.ChessEngine.TranspositionTable = new Dictionary<long, BoardState>();
            Node node = new Node();
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 2, true, zorbistKey, 0, null, null);

            node = new Node();
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBeta(node, board, 6, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null);

            Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());
            _output.WriteLine(ChessEngine.Engine.ChessEngine.NodesEvaluated.ToString());
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                if (isWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }

                isWhite = !isWhite;
            }
        }

        [Fact]
        public void ChessEngineTests3()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "BN", "BB", "  ", "BK", "  ", "BR", "  "},
                {"BP", "BP", "  ", "BP", "  ", "  ", "  ", "BP"},
                {"  ", "  ", "  ", "BP", "BP", "WQ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "BP", "  ", "  "},
                {"  ", "  ", "  ", "WP", "  ", "  ", "  ", "  "},
                {"  ", "  ", "WN", "  ", "  ", "  ", "  ", "  "},
                {"WP", "BQ", "WP", "  ", "WP", "WP", "WP", "WP"},
                {"WR", "  ", "  ", "  ", "WK", "WB", "WN", "WR"}
            };

            int depth = 6;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            _cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            ZorbistData.FillZorbistData(board);
            long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(board);
            ChessEngine.Engine.ChessEngine.InitializeTranspositionTables(depth);
            //Node node = new Node();
            //ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            //ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 2, true, zorbistKey, 0, null, null);

            Node node = new Node();
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBetaVersion1(node, board, depth, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null, new Stack<Move>(), new HashSet<int>(), new HashSet<int>());

            Node nodeBackup = node;

            Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());
            _output.WriteLine(ChessEngine.Engine.ChessEngine.NodesEvaluated.ToString());
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                if (isWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }

                isWhite = !isWhite;
            }
        }

        [Fact]
        public void ChessEngineTests4()
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

            int depth = 6;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            _cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            ZorbistData.FillZorbistData(board);
            long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(board);
            ChessEngine.Engine.ChessEngine.InitializeTranspositionTables(depth);
            //Node node = new Node();
            //ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            //ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 2, true, zorbistKey, 0, null, null);

            Node node = new Node();
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBetaVersion1(node, board, depth, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null, new Stack<Move>(), new HashSet<int>(), new HashSet<int>());

            Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());
            _output.WriteLine(ChessEngine.Engine.ChessEngine.NodesEvaluated.ToString());
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                if (isWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }

                isWhite = !isWhite;
            }
        }

        [Fact]
        public void ChessEngineTests5()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "  ", "  ", "BQ", "BK", "BB", "  ", "BR"},
                {"BP", "BP", "  ", "BN", "BP", "BP", "BP", "BP"},
                {"  ", "  ", "  ", "  ", "  ", "BN", "  ", "  "},
                {"  ", "WB", "BP", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "BP", "WP", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "WP"},
                {"WP", "WP", "WP", "WP", "WN", "WP", "  ", "WP"},
                {"WR", "  ", "WB", "WQ", "WK", "  ", "  ", "WR"}
            };

            int depth = 6;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            _cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            ZorbistData.FillZorbistData(board);
            long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(board);
            ChessEngine.Engine.ChessEngine.InitializeTranspositionTables(depth);
            //Node node = new Node();
            //ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            //ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 2, true, zorbistKey, 0, null, null);

            Node node = new Node();
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBetaVersion1(node, board, depth, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null, new Stack<Move>(), new HashSet<int>(), new HashSet<int>());

            Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());
            _output.WriteLine(ChessEngine.Engine.ChessEngine.NodesEvaluated.ToString());
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                if (isWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }

                isWhite = !isWhite;
            }
        }

        [Fact]
        public void ChessEngineTests6()
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

            int depth = 6;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            _cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            ZorbistData.FillZorbistData(board);
            long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(board);
            ChessEngine.Engine.ChessEngine.InitializeTranspositionTables(depth);
            //Node node = new Node();
            //ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            //ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 2, true, zorbistKey, 0, null, null);

            Node node = new Node();
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBetaVersion1(node, board, depth, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null, new Stack<Move>(), new HashSet<int>(), new HashSet<int>());

            Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());
            _output.WriteLine(ChessEngine.Engine.ChessEngine.NodesEvaluated.ToString());
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                if (isWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }

                isWhite = !isWhite;
            }
        }

        [Fact]
        public void ChessEngineTests7()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"BP", "BB", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "BP", "  ", "  ", "BR", "  ", "WP", "BK"},
                {"  ", "  ", "  ", "BP", "WN", "BP", "  ", "  "},
                {"  ", "  ", "BP", "WP", "  ", "WP", "  ", "  "},
                {"  ", "  ", "BQ", "  ", "WP", "  ", "WR", "  "},
                {"WP", "  ", "  ", "  ", "WQ", "WK", "WP", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            int depth = 6;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            _cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            ZorbistData.FillZorbistData(board);
            long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(board);
            ChessEngine.Engine.ChessEngine.InitializeTranspositionTables(depth);
            //Node node = new Node();
            //ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            //ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 2, true, zorbistKey, 0, null, null);

            Node node = new Node();
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBetaVersion1(node, board, depth, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null, new Stack<Move>(), new HashSet<int>(), new HashSet<int>());

            Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();
            int firstmoveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
            int cost = node.Costs[firstmoveId];

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());
            _output.WriteLine(ChessEngine.Engine.ChessEngine.NodesEvaluated.ToString());
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                if (isWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }

                isWhite = !isWhite;
            }

            Assert.True(cost > 50000);
        }

        [Fact]
        public void ChessEngineTests8()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "BR", "BK"},
                {"BP", "BP", "BB", "  ", "  ", "WQ", "  ", "  "},
                {"  ", "  ", "BB", "  ", "  ", "BP", "  ", "BP"},
                {"  ", "  ", "WP", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "BP", "WR", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "WP", "BQ"},
                {"WP", "  ", "WB", "  ", "  ", "WP", "  ", "WP"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "WK", "  "}
            };

            int depth = 6;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            _cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            ZorbistData.FillZorbistData(board);
            long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(board);
            ChessEngine.Engine.ChessEngine.InitializeTranspositionTables(depth);
            //Node node = new Node();
            //ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            //ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 2, true, zorbistKey, 0, null, null);

            Node node = new Node();
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBetaVersion1(node, board, depth, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null, new Stack<Move>(), new HashSet<int>(), new HashSet<int>());

            Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();
            int firstmoveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
            int cost = node.Costs[firstmoveId];

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());
            _output.WriteLine(ChessEngine.Engine.ChessEngine.NodesEvaluated.ToString());
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                if (isWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }

                isWhite = !isWhite;
            }

            Assert.True(cost > 50000);
        }

        [Fact]
        public void ChessEngineTests9()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "BN", "BB", "BQ", "BK", "BB", "  ", "BR"},
                {"BP", "BP", "BP", "  ", "BP", "BP", "BP", "BP"},
                {"  ", "  ", "  ", "BP", "  ", "BN", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "WP", "WP", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"WP", "WP", "WP", "  ", "  ", "WP", "WP", "WP"},
                {"WR", "WN", "WB", "WQ", "WK", "WB", "WN", "WR"}
            };

            int depth = 6;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            _cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            ZorbistData.FillZorbistData(board);
            long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(board);
            ChessEngine.Engine.ChessEngine.InitializeTranspositionTables(depth);
            //Node node = new Node();
            //ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            //ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 2, true, zorbistKey, 0, null, null);

            Node node = new Node { IsWhite = true };
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBetaVersion1(node, board, depth, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null, new Stack<Move>(), new HashSet<int>(), new HashSet<int>());
            Node backupNode = node;
            Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();
            int firstmoveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
            int cost = node.Costs[firstmoveId];

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());
            _output.WriteLine(ChessEngine.Engine.ChessEngine.NodesEvaluated.ToString());
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                if (isWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }

                isWhite = !isWhite;
            }

            _output.WriteLine("######################################################");
            _output.WriteLine(ChessEngine.Engine.ChessEngine.GetMoveTree(backupNode));
            Assert.True(cost > 50000);
        }

        [Fact]
        public void ChessEngineTests10()
        {
            string[,] boardInStringFormat =
            {
                {"  ", "  ", "WR", "  ", "BN", "  ", "BK", "  "},
                {"BP", "BP", "  ", "  ", "  ", "BP", "BP", "BP"},
                {"  ", "  ", "BP", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "BP", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "WP", "  ", "WP", "  "},
                {"  ", "  ", "WP", "  ", "  ", "WB", "  ", "  "},
                {"WK", "WP", "  ", "  ", "WP", "WP", "  ", "WP"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            int depth = 6;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
            _cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            ZorbistData.FillZorbistData(board);
            long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(board);
            ChessEngine.Engine.ChessEngine.InitializeTranspositionTables(depth);
            Game.MovesPlayed = new List<Move>();
            //Node node = new Node();
            //ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            //ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, board, 2, true, zorbistKey, 0, null, null);

            Node node = new Node { IsWhite = true };
            ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
            ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBetaVersion1(node, board, depth, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null, new Stack<Move>(), new HashSet<int>(), new HashSet<int>());
            Node backupNode = node;
            Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();
            int firstmoveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
            int cost = node.Costs[firstmoveId];

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _output.WriteLine(elapsedMs.ToString());
            _output.WriteLine(ChessEngine.Engine.ChessEngine.NodesEvaluated.ToString());
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                _output.WriteLine($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
                node = node.ChildNodes.FirstOrDefault(x => x.MoveId == moveId);

                if (node == null)
                {
                    break;
                }

                if (isWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                        .FirstOrDefault();
                }

                isWhite = !isWhite;
            }

            _output.WriteLine("######################################################");
            _output.WriteLine(ChessEngine.Engine.ChessEngine.GetMoveTree(backupNode));
            Assert.True(cost > 50000);
        }
    }
}
