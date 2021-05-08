using System.Collections.Generic;

namespace ChessEngine.Helpers
{
    public class BoardHelper
    {
        private static ulong one = 1;

        public static Dictionary<string, int> PieceValueDictionary = new Dictionary<string, int>
        {
            {"WP",1000},
            {"WB",3000},
            {"WN",3000},
            {"WR",5000},
            {"WQ",9000},
            {"WK",100000},
            {"BP",1000},
            {"BB",3000},
            {"BN",3000},
            {"BR",5000},
            {"BQ",9000},
            {"BK",100000}
        };

        public static Dictionary<string, int> PieceMinValueDictionary = new Dictionary<string, int>
        {
            {"WP",1},
            {"WB",3},
            {"WN",3},
            {"WR",5},
            {"WQ",9},
            {"WK",-10000},
            {"BP",1},
            {"BB",3},
            {"BN",3},
            {"BR",5},
            {"BQ",9},
            {"BK",-10000}
        };

        public static Dictionary<string, int> PieceIdDictionary = new Dictionary<string, int>
        {
            {"WP",1},
            {"WB",2},
            {"WN",3},
            {"WR",4},
            {"WQ",5},
            {"WK",6},
            {"BP",7},
            {"BB",8},
            {"BN",9},
            {"BR",10},
            {"BQ",11},
            {"BK",12}
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
                        Piece piece = new Piece
                        {
                            Id = PieceIdDictionary[pieceName],
                            Name = pieceName,
                            Value = PieceValueDictionary[pieceName],
                            MinValue = PieceMinValueDictionary[pieceName],
                            IsWhite = pieceName.StartsWith("W")
                        };
                        board[row, column].Piece = piece;
                    }
                }
            }

            return board;
        }

        public static ulong GetOccupancy(Cell[,] board)
        {
            ulong occupancy = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;

                    if (board[i, j].Piece != null)
                    {
                        occupancy = occupancy | one << square;
                    }
                }
            }

            return occupancy;
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

        public static string[,] GetBoardAsStringArray(Cell[,] board)
        {
            string[,] boardAsStringArray = new string[8, 8];
            int maxRowNumber = 7;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int row = maxRowNumber - i;
                    int column = j;
                    boardAsStringArray[i, j] = board[row, column].Piece?.Name ?? "  ";
                }
            }

            return boardAsStringArray;
        }
    }
}
