using System.Collections.Generic;

namespace ChessEngine.Pieces
{
    public class Pawn
    {
        #region Optimization Version#1

        public static List<Move> GetMoves(Cell[,] board, Cell cell)
        {
            if (cell.Piece.IsWhite)
            {
                return GetWhiteMoves(board, cell);
            }

            return GetBlackMoves(board, cell);
        }

        private static List<Move> GetWhiteMoves(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            Piece piece = cell.Piece;

            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("P"))
            {
                return moves;
            }

            if (row == 7 || row == 0)
            {
                return moves;
            }

            if (board[row + 1, column].Piece == null)
            {
                moves.Add(new Move { From = cell.Position, To = board[row + 1, column].Position });
            }

            if (row == 1)
            {
                if (board[row + 1, column].Piece == null && board[row + 2, column].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = board[row + 2, column].Position });
                }
            }

            if (column + 1 < 8)
            {
                if (board[row + 1, column + 1].Piece != null && !board[row + 1, column + 1].Piece.IsWhite)
                {
                    moves.Add(new Move
                    {
                        From = cell.Position,
                        To = board[row + 1, column + 1].Position,
                        Cost = board[row + 1, column + 1].Piece.Value
                    });
                }
            }

            if (column - 1 >= 0)
            {
                if (board[row + 1, column - 1].Piece != null && !board[row + 1, column - 1].Piece.IsWhite)
                {
                    moves.Add(new Move
                    {
                        From = cell.Position,
                        To = board[row + 1, column - 1].Position,
                        Cost = board[row + 1, column - 1].Piece.Value
                    });
                }
            }

            return moves;
        }

        private static List<Move> GetBlackMoves(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            Piece piece = cell.Piece;

            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("P"))
            {
                return moves;
            }

            if (row == 0 || row == 7)
            {
                return moves;
            }

            if (board[row - 1, column].Piece == null)
            {
                moves.Add(new Move { From = cell.Position, To = board[row - 1, column].Position });
            }

            if (row == 6)
            {
                if (board[row - 1, column].Piece == null && board[row - 2, column].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = board[row - 2, column].Position });
                }
            }

            if (column + 1 < 8)
            {
                if (board[row - 1, column + 1].Piece != null && board[row - 1, column + 1].Piece.IsWhite)
                {
                    moves.Add(new Move
                    {
                        From = cell.Position,
                        To = board[row - 1, column + 1].Position,
                        Cost = board[row - 1, column + 1].Piece.Value
                    });
                }
            }

            if (column - 1 >= 0)
            {
                if (board[row - 1, column - 1].Piece != null && board[row - 1, column - 1].Piece.IsWhite)
                {
                    moves.Add(new Move
                    {
                        From = cell.Position,
                        To = board[row - 1, column - 1].Position,
                        Cost = board[row - 1, column - 1].Piece.Value
                    });
                }
            }

            return moves;
        }

        #endregion

        #region Optimization Version#2

        public static void AddMoves(Cell[,] board, Cell cell, List<Move> moves)
        {
            if (cell.Piece.IsWhite)
            {
                AddWhiteMoves(board, cell, moves);
            }

            AddBlackMoves(board, cell, moves);
        }

        private static void AddWhiteMoves(Cell[,] board, Cell cell, List<Move> moves)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;

            if (!cell.Piece.Name.EndsWith("P"))
            {
                return;
            }

            if (row == 7 || row == 0)
            {
                return;
            }

            if (board[row + 1, column].Piece == null)
            {
                moves.Add(new Move { From = cell.Position, To = board[row + 1, column].Position });
            }

            if (row == 1)
            {
                if (board[row + 1, column].Piece == null && board[row + 2, column].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = board[row + 2, column].Position });
                }
            }

            if (column + 1 < 8)
            {
                if (board[row + 1, column + 1].Piece != null && !board[row + 1, column + 1].Piece.IsWhite)
                {
                    moves.Add(new Move
                    {
                        From = cell.Position,
                        To = board[row + 1, column + 1].Position,
                        Cost = board[row + 1, column + 1].Piece.Value
                    });
                }
            }

            if (column - 1 >= 0)
            {
                if (board[row + 1, column - 1].Piece != null && !board[row + 1, column - 1].Piece.IsWhite)
                {
                    moves.Add(new Move
                    {
                        From = cell.Position,
                        To = board[row + 1, column - 1].Position,
                        Cost = board[row + 1, column - 1].Piece.Value
                    });
                }
            }
        }

        private static void AddBlackMoves(Cell[,] board, Cell cell, List<Move> moves)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;

            if (!cell.Piece.Name.EndsWith("P"))
            {
                return;
            }

            if (row == 0 || row == 7)
            {
                return;
            }

            if (board[row - 1, column].Piece == null)
            {
                moves.Add(new Move { From = cell.Position, To = board[row - 1, column].Position });
            }

            if (row == 6)
            {
                if (board[row - 1, column].Piece == null && board[row - 2, column].Piece == null)
                {
                    moves.Add(new Move { From = cell.Position, To = board[row - 2, column].Position });
                }
            }

            if (column + 1 < 8)
            {
                if (board[row - 1, column + 1].Piece != null && !board[row - 1, column + 1].Piece.IsWhite)
                {
                    moves.Add(new Move
                    {
                        From = cell.Position,
                        To = board[row - 1, column + 1].Position,
                        Cost = board[row - 1, column + 1].Piece.Value
                    });
                }
            }

            if (column - 1 >= 0)
            {
                if (board[row - 1, column - 1].Piece != null && !board[row - 1, column - 1].Piece.IsWhite)
                {
                    moves.Add(new Move
                    {
                        From = cell.Position,
                        To = board[row - 1, column - 1].Position,
                        Cost = board[row - 1, column - 1].Piece.Value
                    });
                }
            }
        }

        #endregion

    }
}
