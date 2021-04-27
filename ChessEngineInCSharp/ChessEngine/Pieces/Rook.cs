using System.Collections.Generic;
using ChessEngine.Helpers;
using UI.Services;

namespace ChessEngine.Pieces
{
    public class Rook
    {
        #region Optimization Version#1

        public static List<Move> GetMoves(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("R"))
            {
                return moves;
            }

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
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

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
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

            row = cell.Position.Row;

            for (column = cell.Position.Column + 1; column < 8; column++)
            {
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

            for (column = cell.Position.Column - 1; column >= 0; column--)
            {
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

            if (!cell.Piece.Name.EndsWith("R"))
            {
                return;
            }

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
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

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
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

            row = cell.Position.Row;

            for (column = cell.Position.Column + 1; column < 8; column++)
            {
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

            for (column = cell.Position.Column - 1; column >= 0; column--)
            {
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

            if (!cell.Piece.Name.EndsWith("R"))
            {
                return moves;
            }

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
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

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
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

            row = cell.Position.Row;

            for (column = cell.Position.Column + 1; column < 8; column++)
            {
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

            for (column = cell.Position.Column - 1; column >= 0; column--)
            {
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

            if (!cell.Piece.Name.EndsWith("R"))
            {
                return moves;
            }

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
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

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
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

            row = cell.Position.Row;

            for (column = cell.Position.Column + 1; column < 8; column++)
            {
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

            for (column = cell.Position.Column - 1; column >= 0; column--)
            {
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

        public static bool IsOpponentKingIsInCheck(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            int moveId = 0;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("R"))
            {
                return false;
            }

            for (row = cell.Position.Row + 1; row < 8; row++)
            {
                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite && board[row, column].Piece.Name[1] == 'K')
                    {
                        return true;
                    }

                    break;
                }
            }

            for (row = cell.Position.Row - 1; row >= 0; row--)
            {
                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite && board[row, column].Piece.Name[1] == 'K')
                    {
                        return true;
                    }

                    break;
                }
            }

            row = cell.Position.Row;

            for (column = cell.Position.Column + 1; column < 8; column++)
            {
                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite && board[row, column].Piece.Name[1] == 'K')
                    {
                        return true;
                    }

                    break;
                }
            }

            for (column = cell.Position.Column - 1; column >= 0; column--)
            {
                if (board[row, column].Piece != null)
                {
                    if (board[row, column].Piece.IsWhite != cell.Piece.IsWhite && board[row, column].Piece.Name[1] == 'K')
                    {
                        return true;
                    }

                    break;
                }
            }

            return false;
        }

        public static List<Move> GetRookMovesFromMagicBitboards(int square, ulong rookMask, ulong occupancy, ulong ownBlockers)
        {
            ulong rookBlockers = rookMask & occupancy;
            ulong movesInBinary = RookMovesHelper.RookBlockerMovesToBinaryMoves[square, (int)((rookBlockers * RookMovesHelper.MagicNumbersForRook[square]) >> (50))] & ~ownBlockers;
            return RookMovesHelper.RookMovesBinaryToActualMoves[(int)(movesInBinary % RookMovesHelper.HashKeyForRookMoves)] ?? new List<Move>();
        }

        #endregion
    }
}
