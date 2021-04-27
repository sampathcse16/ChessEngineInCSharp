using System.Collections.Generic;
using ChessEngine.Helpers;
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

            if (cell.Piece.IsWhite && board[0, 4].Piece?.Name == "WK"
                                   && board[0, 5].Piece == null
                                   && board[0, 6].Piece == null
                                   && board[0, 7].Piece?.Name == "WR"
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 0, Column = 5 }, Piece = new Piece { Name = "WK", IsWhite = true } })
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 0, Column = 6 }, Piece = new Piece { Name = "WK", IsWhite = true } }))
            {
                moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column + 2));
                moves.Add(CacheService.AllPossibleMoves[moveId]);
            }

            if (cell.Piece.IsWhite && board[0, 4].Piece?.Name == "WK"
                                   && board[0, 3].Piece == null
                                   && board[0, 2].Piece == null
                                   && board[0, 1].Piece == null
                                   && board[0, 0].Piece?.Name == "WR"
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 0, Column = 3 }, Piece = new Piece { Name = "WK", IsWhite = true } })
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 0, Column = 2 }, Piece = new Piece { Name = "WK", IsWhite = true } })
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 0, Column = 1 }, Piece = new Piece { Name = "WK", IsWhite = true } }))
            {
                moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column - 2));
                moves.Add(CacheService.AllPossibleMoves[moveId]);
            }


            if (!cell.Piece.IsWhite && board[7, 4].Piece?.Name == "BK"
                                   && board[7, 5].Piece == null
                                   && board[7, 6].Piece == null
                                   && board[7, 7].Piece?.Name == "BR"
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 7, Column = 5 }, Piece = new Piece { Name = "BK" } })
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 7, Column = 6 }, Piece = new Piece { Name = "BK" } }))
            {
                moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column + 2));
                moves.Add(CacheService.AllPossibleMoves[moveId]);
            }

            if (!cell.Piece.IsWhite && board[7, 4].Piece?.Name == "BK"
                                   && board[7, 3].Piece == null
                                   && board[7, 2].Piece == null
                                   && board[7, 1].Piece == null
                                   && board[7, 0].Piece?.Name == "BR"
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 7, Column = 3 }, Piece = new Piece { Name = "BK" } })
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 7, Column = 2 }, Piece = new Piece { Name = "BK" } })
                                   && !IsKingIsInCheck(board, new Cell { Position = new Position { Row = 7, Column = 1 }, Piece = new Piece { Name = "BK" } }))
            {
                moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row) * 8 + (column - 2));
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

        public static bool IsOpponentKingIsInCheck(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            int moveId = 0;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("K"))
            {
                return false;
            }

            if (row + 1 < 8)
            {
                if (board[row + 1, column].Piece != null)
                {
                    if (board[row + 1, column].Piece.IsWhite != cell.Piece.IsWhite && board[row + 1, column].Piece.Name[1] == 'K')
                    {
                        return true;
                    }
                }
            }

            if (row + 1 < 8 && column + 1 < 8)
            {
                if (board[row + 1, column + 1].Piece != null)
                {
                    if (board[row + 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite && board[row + 1, column + 1].Piece.Name[1] == 'K')
                    {
                        return true;
                    }
                }
            }

            if (row + 1 < 8 && column - 1 >= 0)
            {
                if (board[row + 1, column - 1].Piece != null)
                {
                    if (board[row + 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite && board[row + 1, column - 1].Piece.Name[1] == 'K')
                    {
                        return true;
                    }
                }
            }

            if (row < 8 && column + 1 < 8)
            {
                if (board[row, column + 1].Piece != null)
                {
                    if (board[row, column + 1].Piece.IsWhite != cell.Piece.IsWhite && board[row, column + 1].Piece.Name[1] == 'K')
                    {
                        return true;
                    }
                }
            }

            if (row < 8 && column - 1 >= 0)
            {
                if (board[row, column - 1].Piece != null)
                {
                    if (board[row, column - 1].Piece.IsWhite != cell.Piece.IsWhite && board[row, column - 1].Piece.Name[1] == 'K')
                    {
                        return true;
                    }
                }
            }

            if (row - 1 >= 0)
            {
                if (board[row - 1, column].Piece != null)
                {
                    if (board[row - 1, column].Piece.IsWhite != cell.Piece.IsWhite && board[row - 1, column].Piece.Name[1] == 'K')
                    {
                        return true;
                    }
                }
            }

            if (row - 1 >= 0 && column + 1 < 8)
            {
                if (board[row - 1, column + 1].Piece != null)
                {
                    if (board[row - 1, column + 1].Piece.IsWhite != cell.Piece.IsWhite && board[row - 1, column + 1].Piece.Name[1] == 'K')
                    {
                        return true;
                    }
                }
            }

            if (row - 1 >= 0 && column - 1 >= 0)
            {
                if (board[row - 1, column - 1].Piece != null)
                {
                    if (board[row - 1, column - 1].Piece.IsWhite != cell.Piece.IsWhite && board[row - 1, column - 1].Piece.Name[1] == 'K')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsKingIsInCheck(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;

            if (!cell.Piece.Name.EndsWith("K"))
            {
                return false;
            }

            for (int c = column + 1; c < 8; c++)
            {
                if (board[row, c].Piece != null)
                {
                    Piece piece = board[row, c].Piece;

                    if (board[row, c].Piece.IsWhite == cell.Piece.IsWhite)
                    {
                        break;
                    }
                    else
                    {
                        if (piece.Name[1] == 'R' || piece.Name[1] == 'Q')
                        {
                            return true;
                        }
                    }
                }
            }

            for (int c = column - 1; c >= 0 && row < 8; c--)
            {
                if (board[row, c].Piece != null)
                {
                    Piece piece = board[row, c].Piece;

                    if (board[row, c].Piece.IsWhite == cell.Piece.IsWhite)
                    {
                        break;
                    }
                    else
                    {
                        if (piece.Name[1] == 'R' || piece.Name[1] == 'Q')
                        {
                            return true;
                        }
                    }
                }
            }

            for (int r = row + 1; r < 8; r++)
            {
                if (board[r, column].Piece != null)
                {
                    Piece piece = board[r, column].Piece;

                    if (board[r, column].Piece.IsWhite == cell.Piece.IsWhite)
                    {
                        break;
                    }
                    else
                    {
                        if (piece.Name[1] == 'R' || piece.Name[1] == 'Q')
                        {
                            return true;
                        }
                    }
                }
            }

            for (int r = row - 1; r >= 0; r--)
            {
                if (board[r, column].Piece != null)
                {
                    Piece piece = board[r, column].Piece;

                    if (board[r, column].Piece.IsWhite == cell.Piece.IsWhite)
                    {
                        break;
                    }
                    else
                    {
                        if (piece.Name[1] == 'R' || piece.Name[1] == 'Q')
                        {
                            return true;
                        }
                    }
                }
            }

            for (int r = row + 1, c = column + 1; r < 8 && c < 8; c++, r++)
            {
                if (board[r, c].Piece != null)
                {
                    Piece piece = board[r, c].Piece;

                    if (board[r, c].Piece.IsWhite == cell.Piece.IsWhite)
                    {
                        break;
                    }
                    else
                    {
                        if (piece.Name[1] == 'B'
                            || piece.Name[1] == 'Q')
                        {
                            return true;
                        }

                        if (cell.Piece.IsWhite)
                        {
                            if (piece.Name[1] == 'P')
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            for (int r = row + 1, c = column - 1; r < 8 && c >= 0; c--, r++)
            {
                if (board[r, c].Piece != null)
                {
                    Piece piece = board[r, c].Piece;

                    if (board[r, c].Piece.IsWhite == cell.Piece.IsWhite)
                    {
                        break;
                    }
                    else
                    {
                        if (piece.Name[1] == 'B'
                            || piece.Name[1] == 'Q')
                        {
                            return true;
                        }

                        if (cell.Piece.IsWhite)
                        {
                            if (piece.Name[1] == 'P')
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            for (int r = row - 1, c = column + 1; r >= 0 && c < 8; c++, r--)
            {
                if (board[r, c].Piece != null)
                {
                    Piece piece = board[r, c].Piece;

                    if (board[r, c].Piece.IsWhite == cell.Piece.IsWhite)
                    {
                        break;
                    }
                    else
                    {
                        if (piece.Name[1] == 'B'
                            || piece.Name[1] == 'Q')
                        {
                            return true;
                        }

                        if (!cell.Piece.IsWhite)
                        {
                            if (piece.Name[1] == 'P')
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            for (int r = row - 1, c = column - 1; r >= 0 && c >= 0; c--, r--)
            {
                if (board[r, c].Piece != null)
                {
                    Piece piece = board[r, c].Piece;

                    if (board[r, c].Piece.IsWhite == cell.Piece.IsWhite)
                    {
                        break;
                    }
                    else
                    {
                        if (piece.Name[1] == 'B'
                            || piece.Name[1] == 'Q')
                        {
                            return true;
                        }

                        if (!cell.Piece.IsWhite)
                        {
                            if (piece.Name[1] == 'P')
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static List<Move> GetKingMovesFromMagicBitboards(int square, ulong kingMask, ulong ownBlockers)
        {
            ulong kingBlockers = kingMask & ownBlockers;
            ulong movesInBinary = KingMovesHelper.KingBlockerMovesToBinaryMoves[square, (int)((kingBlockers * KingMovesHelper.MagicNumbersForKing[square]) >> (56))];
            return KingMovesHelper.KingMovesBinaryToActualMoves[(int)(movesInBinary % KnightMovesHelper.HashKeyForKnightMoves)];
        }

        #endregion
    }
}
