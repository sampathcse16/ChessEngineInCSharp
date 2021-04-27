using System.Collections.Generic;
using ChessEngine.Helpers;
using UI.Services;

namespace ChessEngine.Pieces
{
    public class Knight
    {
        #region Optimization Version#1

        public static List<Move> GetMoves(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("N"))
            {
                return moves;
            }

            if (row + 1 < 8 && column + 2 < 8)
            {
                if (board[row + 1, column + 2].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column + 2 } });
                }
                else if (board[row + 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column + 2 } });
                }
            }

            if (row + 1 < 8 && column - 2 >= 0)
            {
                if (board[row + 1, column - 2].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column - 2 } });
                }
                else if (board[row + 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column - 2 } });
                }
            }

            if (row + 2 < 8 && column + 1 < 8)
            {
                if (board[row + 2, column + 1].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 2, Column = column + 1 } });
                }
                else if (board[row + 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 2, Column = column + 1 } });
                }
            }

            if (row + 2 < 8 && column - 1 >= 0)
            {
                if (board[row + 2, column - 1].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 2, Column = column - 1 } });
                }
                else if (board[row + 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 2, Column = column - 1 } });
                }
            }

            if (row - 1 >= 0 && column + 2 < 8)
            {
                if (board[row - 1, column + 2].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column + 2 } });
                }
                else if (board[row - 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column + 2 } });
                }
            }

            if (row - 1 >= 0 && column - 2 >= 0)
            {
                if (board[row - 1, column - 2].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column - 2 } });
                }
                else if (board[row - 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column - 2 } });
                }
            }

            if (row - 2 >= 0 && column + 1 < 8)
            {
                if (board[row - 2, column + 1].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 2, Column = column + 1 } });
                }
                else if (board[row - 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 2, Column = column + 1 } });
                }
            }

            if (row - 2 >= 0 && column - 1 >= 0)
            {
                if (board[row - 2, column - 1].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 2, Column = column - 1 } });
                }
                else if (board[row - 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 2, Column = column - 1 } });
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

            if (!cell.Piece.Name.EndsWith("N"))
            {
                return;
            }

            if (row + 1 < 8 && column + 2 < 8)
            {
                if (board[row + 1, column + 2].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column + 2 } });
                }
                else if (board[row + 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column + 2 } });
                }
            }

            if (row + 1 < 8 && column - 2 >= 0)
            {
                if (board[row + 1, column - 2].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column - 2 } });
                }
                else if (board[row + 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 1, Column = column - 2 } });
                }
            }

            if (row + 2 < 8 && column + 1 < 8)
            {
                if (board[row + 2, column + 1].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 2, Column = column + 1 } });
                }
                else if (board[row + 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 2, Column = column + 1 } });
                }
            }

            if (row + 2 < 8 && column - 1 >= 0)
            {
                if (board[row + 2, column - 1].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 2, Column = column - 1 } });
                }
                else if (board[row + 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row + 2, Column = column - 1 } });
                }
            }

            if (row - 1 >= 0 && column + 2 < 8)
            {
                if (board[row - 1, column + 2].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column + 2 } });
                }
                else if (board[row - 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column + 2 } });
                }
            }

            if (row - 1 >= 0 && column - 2 >= 0)
            {
                if (board[row - 1, column - 2].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column - 2 } });
                }
                else if (board[row - 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 1, Column = column - 2 } });
                }
            }

            if (row - 2 >= 0 && column + 1 < 8)
            {
                if (board[row - 2, column + 1].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 2, Column = column + 1 } });
                }
                else if (board[row - 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 2, Column = column + 1 } });
                }
            }

            if (row - 2 >= 0 && column - 1 >= 0)
            {
                if (board[row - 2, column - 1].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 2, Column = column - 1 } });
                }
                else if (board[row - 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moves.Add(new Move { From = cell.Position, To = new Position { Row = row - 2, Column = column - 1 } });
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

            if (!cell.Piece.Name.EndsWith("N"))
            {
                return moves;
            }

            if (row + 1 < 8 && column + 2 < 8)
            {
                if (board[row + 1, column + 2].Piece == null)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column + 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
                else if (board[row + 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column + 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row + 1 < 8 && column - 2 >= 0)
            {
                if (board[row + 1, column - 2].Piece == null)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column - 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
                else if (board[row + 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column - 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row + 2 < 8 && column + 1 < 8)
            {
                if (board[row + 2, column + 1].Piece == null)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 2) * 8 + (column + 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
                else if (board[row + 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 2) * 8 + (column + 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row + 2 < 8 && column - 1 >= 0)
            {
                if (board[row + 2, column - 1].Piece == null)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 2) * 8 + (column - 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
                else if (board[row + 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 2) * 8 + (column - 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 1 >= 0 && column + 2 < 8)
            {
                if (board[row - 1, column + 2].Piece == null)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column + 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
                else if (board[row - 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column + 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 1 >= 0 && column - 2 >= 0)
            {
                if (board[row - 1, column - 2].Piece == null)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column - 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
                else if (board[row - 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column - 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 2 >= 0 && column + 1 < 8)
            {
                if (board[row - 2, column + 1].Piece == null)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 2) * 8 + (column + 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
                else if (board[row - 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 2) * 8 + (column + 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 2 >= 0 && column - 1 >= 0)
            {
                if (board[row - 2, column - 1].Piece == null)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 2) * 8 + (column - 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
                else if (board[row - 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 2) * 8 + (column - 1));
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

            if (!cell.Piece.Name.EndsWith("N"))
            {
                return moves;
            }

            if (row + 1 < 8 && column + 2 < 8)
            {
                if (board[row + 1, column + 2].Piece == null)
                {

                }
                else if (board[row + 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column + 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row + 1 < 8 && column - 2 >= 0)
            {
                if (board[row + 1, column - 2].Piece == null)
                {

                }
                else if (board[row + 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 1) * 8 + (column - 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row + 2 < 8 && column + 1 < 8)
            {
                if (board[row + 2, column + 1].Piece == null)
                {

                }
                else if (board[row + 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 2) * 8 + (column + 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row + 2 < 8 && column - 1 >= 0)
            {
                if (board[row + 2, column - 1].Piece == null)
                {

                }
                else if (board[row + 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row + 2) * 8 + (column - 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 1 >= 0 && column + 2 < 8)
            {
                if (board[row - 1, column + 2].Piece == null)
                {

                }
                else if (board[row - 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column + 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 1 >= 0 && column - 2 >= 0)
            {
                if (board[row - 1, column - 2].Piece == null)
                {

                }
                else if (board[row - 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 1) * 8 + (column - 2));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 2 >= 0 && column + 1 < 8)
            {
                if (board[row - 2, column + 1].Piece == null)
                {

                }
                else if (board[row - 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 2) * 8 + (column + 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
                }
            }

            if (row - 2 >= 0 && column - 1 >= 0)
            {
                if (board[row - 2, column - 1].Piece == null)
                {

                }
                else if (board[row - 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite)
                {
                    moveId = (cell.Position.Row * 8 + cell.Position.Column) * 100 + ((row - 2) * 8 + (column - 1));
                    moves.Add(CacheService.AllPossibleMoves[moveId]);
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

            if (!cell.Piece.Name.EndsWith("N"))
            {
                return false;
            }

            if (row + 1 < 8 && column + 2 < 8)
            {
                if (board[row + 1, column + 2].Piece == null)
                {

                }
                else if (board[row + 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite && board[row + 1, column + 2].Piece.Name[1] == 'K')
                {
                    return true;
                }
            }

            if (row + 1 < 8 && column - 2 >= 0)
            {
                if (board[row + 1, column - 2].Piece == null)
                {

                }
                else if (board[row + 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite && board[row + 1, column - 2].Piece.Name[1] == 'K')
                {
                    return true;
                }
            }

            if (row + 2 < 8 && column + 1 < 8)
            {
                if (board[row + 2, column + 1].Piece == null)
                {

                }
                else if (board[row + 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite && board[row + 2, column + 1].Piece.Name[1] == 'K')
                {
                    return true;
                }
            }

            if (row + 2 < 8 && column - 1 >= 0)
            {
                if (board[row + 2, column - 1].Piece == null)
                {

                }
                else if (board[row + 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite && board[row + 2, column - 1].Piece.Name[1] == 'K')
                {
                    return true;
                }
            }

            if (row - 1 >= 0 && column + 2 < 8)
            {
                if (board[row - 1, column + 2].Piece == null)
                {

                }
                else if (board[row - 1, column + 2].Piece.IsWhite != cell.Piece.IsWhite && board[row - 1, column + 2].Piece.Name[1] == 'K')
                {
                    return true;
                }
            }

            if (row - 1 >= 0 && column - 2 >= 0)
            {
                if (board[row - 1, column - 2].Piece == null)
                {

                }
                else if (board[row - 1, column - 2].Piece.IsWhite != cell.Piece.IsWhite && board[row - 1, column - 2].Piece.Name[1] == 'K')
                {
                    return true;
                }
            }

            if (row - 2 >= 0 && column + 1 < 8)
            {
                if (board[row - 2, column + 1].Piece == null)
                {

                }
                else if (board[row - 2, column + 1].Piece.IsWhite != cell.Piece.IsWhite && board[row - 2, column + 1].Piece.Name[1] == 'K')
                {
                    return true;
                }
            }

            if (row - 2 >= 0 && column - 1 >= 0)
            {
                if (board[row - 2, column - 1].Piece == null)
                {

                }
                else if (board[row - 2, column - 1].Piece.IsWhite != cell.Piece.IsWhite && board[row - 2, column - 1].Piece.Name[1] == 'K')
                {
                    return true;
                }
            }

            return false;
        }

        public static List<Move> GetKnightMovesFromMagicBitboards(int square, ulong knightMask, ulong ownBlockers)
        {
            ulong knightBlockers = knightMask & ownBlockers;
            ulong movesInBinary = KnightMovesHelper.KnightBlockerMovesToBinaryMoves[square, (int)((knightBlockers * KnightMovesHelper.MagicNumbersForKnight[square]) >> (56))];
            return KnightMovesHelper.KnightMovesBinaryToActualMoves[(int)(movesInBinary % KnightMovesHelper.HashKeyForKnightMoves)] ?? new List<Move>();
        }

        #endregion
    }
}
