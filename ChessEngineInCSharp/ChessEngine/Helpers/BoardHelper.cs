using System.Collections.Generic;

namespace ChessEngine.Helpers
{
    public class BoardHelper
    {
        public static Dictionary<string, int> PieceValueDictionary = new Dictionary<string, int>
        {
            {"WP",100},
            {"WB",300},
            {"WN",300},
            {"WR",500},
            {"WQ",900},
            {"WK",10000},
            {"BP",100},
            {"BB",300},
            {"BN",300},
            {"BR",500},
            {"BQ",900},
            {"BK",10000}
        };

        public static Dictionary<string, int> PieceIdDictionary = new Dictionary<string, int>
        {
            {"WP",1},
            {"WB",2},
            {"WN",3},
            {"WR",4},
            {"WQ",5},
            {"WK",6},
            {"BP",1},
            {"BB",2},
            {"BN",3},
            {"BR",4},
            {"BQ",5},
            {"BK",6}
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
                    cell.MaximizingPlayerAttacks = new HashSet<int>();
                    cell.MinimizingPlayerAttacks = new HashSet<int>();
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

        public static string[,] GetInitialPositionOfBoardInStringFormat()
        {
            string[,] boardInStringFormat =
            {
                {"BR", "BN", "BB", "BQ", "BK", "BB", "BN", "BR"},
                {"BP", "BP", "BP", "BP", "BP", "BP", "BP", "BP"},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"WP", "WP", "WP", "WP", "WP", "WP", "WP", "WP"},
                {"WR", "WN", "WB", "WQ", "WK", "WB", "WN", "WR"}
            };

            return boardInStringFormat;
        }

        public static Cell[,] GetInitialPositionOfBoard()
        {
            Cell[,] board = BoardHelper.GetBoard(GetInitialPositionOfBoardInStringFormat());
            return board;
        }

        public static string GetBoardAsString(Cell[,] board)
        {
            string boardAsString = string.Empty;
            int maxRowNumber = 7;
            List<string> columns = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int row = maxRowNumber - i;
                    int column = j;

                    if (board[row, column].Piece == null)
                    {
                        columns.Add("  ");
                    }
                    else
                    {
                        columns.Add(board[row, column].Piece.Name);
                    }
                }

                boardAsString += string.Join(",", columns) + "\n";
                columns.Clear();
            }

            return boardAsString;
        }
    }
}
