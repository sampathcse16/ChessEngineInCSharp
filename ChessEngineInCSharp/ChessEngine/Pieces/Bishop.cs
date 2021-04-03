using System.Collections.Generic;
using ChessEngine.Helpers;
using UI.Services;

namespace ChessEngine.Pieces
{
    public class Bishop
    {
        #region Optimization Version#1

        public static List<Move> GetMoves(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("B"))
            {
                return moves;
            }

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
                column++;

                if (column == 8)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
                    }

                    break;
                }

                moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
                column--;

                if (column == -1)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
                    }

                    break;
                }

                moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
                column++;

                if (column == 8)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
                    }

                    break;
                }

                moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
                column--;

                if (column == -1)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
                    }

                    break;
                }

                moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
            }

            return moves;
        }

        #endregion

        #region Optimization Version#2

        public static void AddMoves(Cell[,] board, Cell cell, List<Move> moves)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;

            if (!cell.Piece.Name.EndsWith("B"))
            {
                return;
            }

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
                column++;

                if (column == 8)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
                    }

                    break;
                }

                moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
                column--;

                if (column == -1)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
                    }

                    break;
                }

                moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
                column++;

                if (column == 8)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
                    }

                    break;
                }

                moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
                column--;

                if (column == -1)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
                    }

                    break;
                }

                moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column } });
            }
        }

        #endregion

        #region Optimization Version#3

        public static List<Move> GetMovesFromCache(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            int moveId = 0;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("B"))
            {
                return moves;
            }

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
                
                column++;

                if (column == 8)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }

                    break;
                }

                moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                moves.Add(CacheService.AllPossibleMoves[moveId]);
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
                column--;

                if (column == -1)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }

                    break;
                }

                moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                moves.Add(CacheService.AllPossibleMoves[moveId]);
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
                column++;

                if (column == 8)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }

                    break;
                }

                moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                moves.Add(CacheService.AllPossibleMoves[moveId]);
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
                column--;

                if (column == -1)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }

                    break;
                }

                moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                moves.Add(CacheService.AllPossibleMoves[moveId]);
            }

            return moves;
        }

        public static List<Move> GetKillingMovesFromCache(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            int moveId = 0;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("B"))
            {
                return moves;
            }

            for (row = cell.Position.Row + 1; row < 8; row++)
            {

                column++;

                if (column == 8)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }

                    break;
                }
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
                column--;

                if (column == -1)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }

                    break;
                }
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
                column++;

                if (column == 8)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }

                    break;
                }
            }

            column = cell.Position.Column;

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
                column--;

                if (column == -1)
                {
                    break;
                }

                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + (row * 8 + column);
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }

                    break;
                }
            }

            return moves;
        }
        #endregion
    }
}
