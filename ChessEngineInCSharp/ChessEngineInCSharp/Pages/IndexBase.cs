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
            CacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            ChessEngine.Engine.ChessEngine.AllPossibleMoves = CacheService.AllPossibleMoves;

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

            await Task.Run(() =>
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Node node = new Node();
                ChessEngine.Engine.ChessEngine.GetBestMoveUsingMinMax(node, Board, 4, true, 0, null, null);
                Move move = node.Moves.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)]).FirstOrDefault();
                ChessEngine.Engine.ChessEngine.MakeMove(Board, move);
                MakeMoveOnUI(UiChessBoard, move);
                watch.Stop();
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
                        move = node.Moves
                            ?.OrderBy(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                            .FirstOrDefault();
                    }
                    else
                    {
                        move = node.Moves
                            ?.OrderByDescending(x => node.Costs[ChessEngine.Engine.ChessEngine.GetMoveId(x)])
                            .FirstOrDefault();
                    }

                    isWhite = !isWhite;
                }
                TotalTimeTaken = watch.ElapsedMilliseconds;
            });
        }

        public void MakeMoveOnUI(string[,] uiboard, Move move)
        {
            string piece = uiboard[move.From.Row, move.From.Column];
            uiboard[move.From.Row, move.From.Column] = null;
            uiboard[move.To.Row, move.To.Column] = piece;
        }
    }
}
