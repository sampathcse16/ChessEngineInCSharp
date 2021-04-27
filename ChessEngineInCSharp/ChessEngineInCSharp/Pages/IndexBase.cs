using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChessEngine;
using ChessEngine.Helpers;
using Microsoft.AspNetCore.Components;
using UI.Models;
using UI.Services;

namespace UI.Pages
{
    public class IndexBase : ComponentBase
    {
        public int MaxRowNumber = 7;
        public string[,] UiChessBoard { get; set; } = new string[8, 8];
        public DragInfo DragInfo { get; set; }
        public bool CanDrop { get; set; } = true;
        public string DropCss { get; set; } = string.Empty;
        public Cell[,] Board { get; set; }
        public long TotalTimeTaken { get; set; }
        public IList<string> ProjectedMoves { get; set; } = new List<string>();
        [Inject]
        public CacheService CacheService { get; set; }

        protected override void OnInitialized()
        {
            Board = BoardHelper.GetInitialPositionOfBoard();
            ZorbistData.FillZorbistData(Board);
            CacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;
            Game.MovesPlayed = new List<Move>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j].Piece != null)
                    {
                        string pieceName = Board[i, j].Piece.Name;

                        switch (pieceName)
                        {
                            case "WP":
                                UiChessBoard[i, j] = "&#9817;";
                                break;
                            case "WB":
                                UiChessBoard[i, j] = "&#9815;";
                                break;
                            case "WN":
                                UiChessBoard[i, j] = "&#9816;";
                                break;
                            case "WR":
                                UiChessBoard[i, j] = "&#9814;";
                                break;
                            case "WQ":
                                UiChessBoard[i, j] = "&#9813;";
                                break;
                            case "WK":
                                UiChessBoard[i, j] = "&#9812;";
                                break;

                            case "BP":
                                UiChessBoard[i, j] = "&#9823;";
                                break;
                            case "BB":
                                UiChessBoard[i, j] = "&#9821;";
                                break;
                            case "BN":
                                UiChessBoard[i, j] = "&#9822;";
                                break;
                            case "BR":
                                UiChessBoard[i, j] = "&#9820;";
                                break;
                            case "BQ":
                                UiChessBoard[i, j] = "&#9819;";
                                break;
                            case "BK":
                                UiChessBoard[i, j] = "&#9818;";
                                break;
                        }
                    }
                }
            }

            BishopMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            BishopMovesHelper.UpdateAllPossibleMovesForAllBlockers();
            RookMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            RookMovesHelper.UpdateAllPossibleMovesForAllBlockers();
            KnightMovesHelper.UpdateAllPossibleMovesFromAllSquares();
            KnightMovesHelper.UpdateAllPossibleMovesForAllBlockers();
        }

        public async Task HandleDrop(int row, int column)
        {
            Debug.WriteLine($"HandleDrop-->From:{row}->To:{column}");

            if (!CanDrop)
            {
                return;
            }

            if (DragInfo != null)
            {
                int moveId = (DragInfo.Row * 8 + DragInfo.Column) * 100 + (row * 8 + column);
                Move move = CacheService.AllPossibleMoves[moveId];
                Game.MovesPlayed.Add(move);
                Game.LastMoveForBlack = move;
                Game.TotalMovesPlayed++;

                if (DragInfo.Row == 7
                    && DragInfo.Column == 4
                    && Board[DragInfo.Row, DragInfo.Column].Piece?.Name == "BK")
                {
                    if (row == 7 && column == 6
                                && Board[DragInfo.Row, DragInfo.Column + 1].Piece == null
                                && Board[DragInfo.Row, DragInfo.Column + 2].Piece == null
                        && Board[DragInfo.Row, DragInfo.Column + 3].Piece?.Name == "BR")
                    {
                        UiChessBoard[DragInfo.Row, DragInfo.Column + 1] = UiChessBoard[DragInfo.Row, DragInfo.Column + 3];
                        UiChessBoard[DragInfo.Row, DragInfo.Column + 3] = null;
                        Board[DragInfo.Row, DragInfo.Column + 1].Piece = Board[DragInfo.Row, DragInfo.Column + 3].Piece;
                        Board[DragInfo.Row, DragInfo.Column + 3].Piece = null;
                    }
                    else if (row == 7 && column == 2
                                     && Board[DragInfo.Row, DragInfo.Column - 1].Piece == null
                                     && Board[DragInfo.Row, DragInfo.Column - 2].Piece == null
                                     && Board[DragInfo.Row, DragInfo.Column - 3].Piece == null
                                     && Board[DragInfo.Row, DragInfo.Column - 4].Piece?.Name == "BR")
                    {
                        UiChessBoard[DragInfo.Row, DragInfo.Column - 1] = UiChessBoard[DragInfo.Row, DragInfo.Column - 4];
                        UiChessBoard[DragInfo.Row, DragInfo.Column - 4] = null;
                        Board[DragInfo.Row, DragInfo.Column - 1].Piece = Board[DragInfo.Row, DragInfo.Column - 4].Piece;
                        Board[DragInfo.Row, DragInfo.Column - 4].Piece = null;
                    }
                }

                UiChessBoard[row, column] = UiChessBoard[DragInfo.Row, DragInfo.Column];
                UiChessBoard[DragInfo.Row, DragInfo.Column] = null;
                Board[row, column].Piece = Board[DragInfo.Row, DragInfo.Column].Piece;
                Board[DragInfo.Row, DragInfo.Column].Piece = null;
                DragInfo = null;
                await MakeComputerMove();
            }
        }

        public void HandleDragStart(int row, int column)
        {
            if (Board[row, column].Piece == null || Board[row, column].Piece.IsWhite)
            {
                return;
            }

            DragInfo = new DragInfo { Row = row, Column = column };
        }

        public void HandleDragEnter(int row, int column)
        {
            Debug.WriteLine($"HandleDragEnter-->From:{row}->To:{column}");
            CanDrop = true;

            if (DragInfo == null || Board[DragInfo.Row, DragInfo.Column].Piece == null)
            {
                CanDrop = false;
                return;
            }

            if (Board[row, column].Piece != null)
            {
                if (Board[DragInfo.Row, DragInfo.Column].Piece.IsWhite == Board[row, column].Piece.IsWhite)
                {
                    CanDrop = false;
                }
            }
        }

        public void HandleDragLeave(int row, int column)
        {
            Debug.WriteLine($"HandleDragLeave-->From:{row}->To:{column}");
        }

        public async Task MakeComputerMove()
        {
            TotalTimeTaken = 0;
            ProjectedMoves.Clear();

            if (Game.TotalMovesPlayed == 0)
            {
                Move move = new Move
                {
                    From = new Position{Row = 1, Column = 3},
                    To = new Position { Row = 3, Column = 3 },
                };
                ChessEngine.Engine.ChessEngine.MakeMove(Board, move);
                MakeMoveOnUI(UiChessBoard, move);
                UpdateGameInfo(move);
                return;
            }

            await Task.Run(() =>
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                int depth = 6;
                CacheService cacheService = new CacheService();
                cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
                long zorbistKey = ZorbistData.GetZorbistKeyForCurrentBoardPosition(Board);
                ChessEngine.Engine.ChessEngine.InitializeTranspositionTables(depth);

                //Node node = new Node();
                //ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
                //ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, Board, 2, true, zorbistKey, 0, null, null);

                Node node = new Node();
                ChessEngine.Engine.ChessEngine.NodesEvaluated = 0;
                ChessEngine.Engine.ChessEngine.UpdateOccupancies(Board);
                ChessEngine.Engine.ChessEngine.GetBestMoveUsingAlphaBetaVersion1(node, Board, depth, Int32.MinValue, Int32.MaxValue, true, zorbistKey, 0, null, null, new Stack<Move>(), new HashSet<int>(), new HashSet<int>());

                Move move = node.Moves.Where(x => node.Costs.ContainsKey(ChessEngine.Engine.ChessEngine.GetMoveId(x)))
                    .OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();

                UpdateGameInfo(move);

                ChessEngine.Engine.ChessEngine.MakeMove(Board, move);
                MakeMoveOnUI(UiChessBoard, move);
                watch.Stop();

                DisplayProjectedMovesOnUI(node, move);

                TotalTimeTaken = watch.ElapsedMilliseconds;
            });
        }

        public void MakeMoveOnUI(string[,] uiboard, Move move)
        {
            string piece = uiboard[move.From.Row, move.From.Column];
            uiboard[move.From.Row, move.From.Column] = null;
            uiboard[move.To.Row, move.To.Column] = piece;

            if (move.From.Row == 0 && move.From.Column == 4
                && move.To.Row == 0 && move.To.Column == 6)
            {
                if (Board[move.To.Row, move.To.Column].Piece?.Name == "WK")
                {
                    uiboard[move.From.Row, move.From.Column + 1] = uiboard[move.From.Row, move.From.Column + 3];
                    uiboard[move.From.Row, move.From.Column + 3] = null;
                }
            }

            if (move.From.Row == 0 && move.From.Column == 4
                                   && move.To.Row == 0 && move.To.Column == 2)
            {
                if (Board[move.To.Row, move.To.Column].Piece?.Name == "WK")
                {
                    uiboard[move.From.Row, move.From.Column - 1] = uiboard[move.From.Row, move.From.Column - 4];
                    uiboard[move.From.Row, move.From.Column - 4] = null;
                }
            }
        }

        public void DisplayProjectedMovesOnUI(Node node, Move move)
        {
            bool isWhite = true;

            while (node != null && move != null)
            {
                int moveId = ChessEngine.Engine.ChessEngine.GetMoveId(move);
                ProjectedMoves.Add($"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[moveId]}");
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

        public void UpdateGameInfo(Move move)
        {
            Game.MovesPlayed.Add(move);
            Game.LastMoveForWhite = move;
            Game.LastMovedPieceForWhite = Board[move.From.Row, move.From.Column].Piece;
            Game.TotalMovesPlayed++;
        }

        public Move GetBestMove(Node node)
        {
            List<Move> moves = ChessEngine.Engine.ChessEngine.GetMoveTreeList(node);
            
            return null;
        }

        public Cell[,] GetBoardCopy()
        {
            string[,] boardAsStringArray = BoardHelper.GetBoardAsStringArray(Board);
            Cell[,] board = BoardHelper.GetBoard(boardAsStringArray);

            return null;
        }
    }
}
