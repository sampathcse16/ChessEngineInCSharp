using System.Collections.Generic;

namespace ChessEngine.Helpers
{
    public class BoardHelper
    {
        public static Dictionary<string, short> PieceValueDictionary = new Dictionary<string, short>
        {
            {"WP",1},
            {"WB",3},
            {"WN",3},
            {"WR",5},
            {"WQ",9},
            {"WK",1000},
            {"BP",1},
            {"BB",3},
            {"BN",3},
            {"BR",5},
            {"BQ",9},
            {"BK",1000}
        };

        public static Cell[,] GetBoard(string[,] boardInStringFormat)
        {
            Cell[,] board = new Cell[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cell cell = new Cell();
                    cell.Position = new Position { Row = i, Column = j };
                    board[i, j] = cell;
                }
            }

            int maxRowNumber = 7;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int row = maxRowNumber - i;
                    int column = j;
                    string cellString = boardInStringFormat[i, j].Trim();

                    if (!string.IsNullOrEmpty(cellString))
                    {
                        string pieceName = cellString.Trim();
                        Piece piece = new Piece { Name = pieceName, Value = PieceValueDictionary[pieceName], IsWhite = pieceName.StartsWith("W") };
                        board[row, column].Piece = piece;
                    }
                }
            }

            return board;
        }
    }
}
