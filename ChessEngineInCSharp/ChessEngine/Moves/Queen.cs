using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Moves
{
    public class Queen
    {
        public static List<Move> GetMoves(Cell[,] board, Cell cell)
        {
            int row = cell.Position.Row;
            int column = cell.Position.Column;
            List<Move> moves = new List<Move>();

            if (!cell.Piece.Name.EndsWith("Q"))
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

            column = cell.Position.Column;

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
    }
}
