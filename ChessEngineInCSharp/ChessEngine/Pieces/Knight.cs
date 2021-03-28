﻿using System.Collections.Generic;

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
    }
}
