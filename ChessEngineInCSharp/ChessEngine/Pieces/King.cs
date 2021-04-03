using System.Collections.Generic;
using UI.Services;

namespace ChessEngine.Pieces
{
    public class King
    {
        #region Optimization Version#1

        public static List<Move> GetMoves(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("K"))
            {
                return moves;
            }

            if (row + 1 < 8)
            {
                if (board[row + 1, column].Piece != null)
                {
                    if (board[row + 1, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column } });
                }
            }

            if (row + 1 < 8 && column + 1 < 8)
            {
                if (board[row + 1, column + 1].Piece != null)
                {
                    if (board[row + 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column + 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column + 1 } });
                }
            }

            if (row + 1 < 8 && column - 1 >= 0)
            {
                if (board[row + 1, column - 1].Piece != null)
                {
                    if (board[row + 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column - 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column - 1 } });
                }
            }

            if (row < 8 && column + 1 < 8)
            {
                if (board[row, column + 1].Piece != null)
                {
                    if (board[row, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column + 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column + 1 } });
                }
            }

            if (row < 8 && column - 1 >= 0)
            {
                if (board[row, column - 1].Piece != null)
                {
                    if (board[row, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column - 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column - 1 } });
                }
            }

            if (row - 1 >= 0)
            {
                if (board[row - 1, column].Piece != null)
                {
                    if (board[row - 1, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column } });
                }
            }

            if (row - 1 >= 0 && column + 1 < 8)
            {
                if (board[row - 1, column + 1].Piece != null)
                {
                    if (board[row - 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column + 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column + 1 } });
                }
            }

            if (row - 1 >= 0 && column - 1 >= 0)
            {
                if (board[row - 1, column - 1].Piece != null)
                {
                    if (board[row - 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column - 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column - 1 } });
                }
            }

            return moves;
        }

        #endregion

        #region Optimization Version#2

        public static void AddMoves(Cell[,] board, Cell cell, List<Move> moves)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;

            if (!cell.Piece.Name.EndsWith("K"))
            {
                return;
            }

            if (row + 1 < 8)
            {
                if (board[row + 1, column].Piece != null)
                {
                    if (board[row + 1, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column } });
                }
            }

            if (row + 1 < 8 && column + 1 < 8)
            {
                if (board[row + 1, column + 1].Piece != null)
                {
                    if (board[row + 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column + 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column + 1 } });
                }
            }

            if (row + 1 < 8 && column - 1 >= 0)
            {
                if (board[row + 1, column - 1].Piece != null)
                {
                    if (board[row + 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column - 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column - 1 } });
                }
            }

            if (row < 8 && column + 1 < 8)
            {
                if (board[row, column + 1].Piece != null)
                {
                    if (board[row, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column + 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column + 1 } });
                }
            }

            if (row < 8 && column - 1 >= 0)
            {
                if (board[row, column - 1].Piece != null)
                {
                    if (board[row, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column - 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row, Column = column - 1 } });
                }
            }

            if (row - 1 >= 0)
            {
                if (board[row - 1, column].Piece != null)
                {
                    if (board[row - 1, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column } });
                }
            }

            if (row - 1 >= 0 && column + 1 < 8)
            {
                if (board[row - 1, column + 1].Piece != null)
                {
                    if (board[row - 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column + 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column + 1 } });
                }
            }

            if (row - 1 >= 0 && column - 1 >= 0)
            {
                if (board[row - 1, column - 1].Piece != null)
                {
                    if (board[row - 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column - 1 } });
                    }
                }
                else
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column - 1 } });
                }
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

            if (!cell.Piece.Name.EndsWith("K"))
            {
                return moves;
            }

            if (row + 1 < 8)
            {
                if (board[row + 1, column].Piece != null)
                {
                    if (board[row + 1, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
                else
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row + 1 < 8 && column + 1 < 8)
            {
                if (board[row + 1, column + 1].Piece != null)
                {
                    if (board[row + 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column + 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
                else
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column + 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row + 1 < 8 && column - 1 >= 0)
            {
                if (board[row + 1, column - 1].Piece != null)
                {
                    if (board[row + 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column - 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
                else
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column - 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row < 8 && column + 1 < 8)
            {
                if (board[row, column + 1].Piece != null)
                {
                    if (board[row, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column + 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
                else
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column + 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row < 8 && column - 1 >= 0)
            {
                if (board[row, column - 1].Piece != null)
                {
                    if (board[row, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column - 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
                else
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column - 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 1 >= 0)
            {
                if (board[row - 1, column].Piece != null)
                {
                    if (board[row - 1, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
                else
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 1 >= 0 && column + 1 < 8)
            {
                if (board[row - 1, column + 1].Piece != null)
                {
                    if (board[row - 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column + 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
                else
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column + 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 1 >= 0 && column - 1 >= 0)
            {
                if (board[row - 1, column - 1].Piece != null)
                {
                    if (board[row - 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column - 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
                else
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column - 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            return moves;
        }

        public static List<Move> GetKillingMovesFromCache(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            int moveId = 0;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("K"))
            {
                return moves;
            }

            if (row + 1 < 8)
            {
                if (board[row + 1, column].Piece != null)
                {
                    if (board[row + 1, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
            }

            if (row + 1 < 8 && column + 1 < 8)
            {
                if (board[row + 1, column + 1].Piece != null)
                {
                    if (board[row + 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column + 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
            }

            if (row + 1 < 8 && column - 1 >= 0)
            {
                if (board[row + 1, column - 1].Piece != null)
                {
                    if (board[row + 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column - 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
            }

            if (row < 8 && column + 1 < 8)
            {
                if (board[row, column + 1].Piece != null)
                {
                    if (board[row, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column + 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
            }

            if (row < 8 && column - 1 >= 0)
            {
                if (board[row, column - 1].Piece != null)
                {
                    if (board[row, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column - 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
            }

            if (row - 1 >= 0)
            {
                if (board[row - 1, column].Piece != null)
                {
                    if (board[row - 1, column].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
            }

            if (row - 1 >= 0 && column + 1 < 8)
            {
                if (board[row - 1, column + 1].Piece != null)
                {
                    if (board[row - 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column + 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
            }

            if (row - 1 >= 0 && column - 1 >= 0)
            {
                if (board[row - 1, column - 1].Piece != null)
                {
                    if (board[row - 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                    {
                        moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column - 1));
                        moves.Add(CacheService.AllPossibleMoves[moveId]);
                    }
                }
            }

            return moves;
        }

        #endregion
    }
}
