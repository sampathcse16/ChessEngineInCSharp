using System;
using System.Collections.Generic;
using System.Text;
using ChessEngine.Pieces;

namespace ChessEngine.Helpers
{
    public class MovesHelper
    {
        public static List<Move> GetAllMovesForCurrentTurnWithOptimizationVersion1(Cell[,] board, bool isWhiteTurn)
        {
            List<Move> moves = new List<Move>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Piece == null)
                    {
                        continue;
                    }

                    Piece piece = board[i, j].Piece;

                    if (piece.IsWhite != isWhiteTurn)
                    {
                        continue;
                    }

                    switch (piece.Name)
                    {
                        case "WP":
                        case "BP":
                            moves.AddRange(Pawn.GetMoves(board, board[i, j]));
                            break;
                        case "WB":
                        case "BB":
                            moves.AddRange(Bishop.GetMoves(board, board[i, j]));
                            break;
                        case "WN":
                        case "BN":
                            moves.AddRange(Knight.GetMoves(board, board[i, j]));
                            break;
                        case "WR":
                        case "BR":
                            moves.AddRange(Rook.GetMoves(board, board[i, j]));
                            break;
                        case "WQ":
                        case "BQ":
                            moves.AddRange(Queen.GetMoves(board, board[i, j]));
                            break;
                        case "WK":
                        case "BK":
                            moves.AddRange(King.GetMoves(board, board[i, j]));
                            break;
                    }
                }
            }

            return moves;
        }

        public static List<Move> GetAllMovesForCurrentTurnWithOptimizationVersion2(Cell[,] board, bool isWhiteTurn)
        {
            List<Move> moves = new List<Move>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Piece == null)
                    {
                        continue;
                    }

                    Piece piece = board[i, j].Piece;

                    if (piece.IsWhite != isWhiteTurn)
                    {
                        continue;
                    }

                    switch (piece.Name)
                    {
                        case "WP":
                        case "BP":
                            Pawn.AddMoves(board, board[i, j], moves);
                            break;
                        case "WB":
                        case "BB":
                            Bishop.AddMoves(board, board[i, j], moves);
                            break;
                        case "WN":
                        case "BN":
                            Knight.AddMoves(board, board[i, j], moves);
                            break;
                        case "WR":
                        case "BR":
                            Rook.AddMoves(board, board[i, j], moves);
                            break;
                        case "WQ":
                        case "BQ":
                            Queen.AddMoves(board, board[i, j], moves);
                            break;
                        case "WK":
                        case "BK":
                            King.AddMoves(board, board[i, j], moves);
                            break;
                    }
                }
            }

            return moves;
        }
    }
}
