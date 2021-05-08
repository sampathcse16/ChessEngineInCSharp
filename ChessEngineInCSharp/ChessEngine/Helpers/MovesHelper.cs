using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine.Pieces;
using UI.Services;

namespace ChessEngine.Helpers
{
    public class MovesHelper
    {
        private static ulong one = 1;

        static int maxRowNumber = 7;

        public static List<Move> GetAllMovesForCurrentTurnWithOptimizationVersion1(Cell[,] board, bool isWhiteTurn)
        {
            List<Move> moves = new List<Move>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Piece == null)
                    {
                        continue;
                    }

                    Piece piece = board[i, j].Piece;

                    if (piece.IsWhite != isWhiteTurn)
                    {
                        continue;
                    }

                    switch (piece.Name)
                    {
                        case "WP":
                        case "BP":
                            moves.AddRange(Pawn.GetMoves(board, board[i, j]));
                            break;
                        case "WB":
                        case "BB":
                            moves.AddRange(Bishop.GetMoves(board, board[i, j]));
                            break;
                        case "WN":
                        case "BN":
                            moves.AddRange(Knight.GetMoves(board, board[i, j]));
                            break;
                        case "WR":
                        case "BR":
                            moves.AddRange(Rook.GetMoves(board, board[i, j]));
                            break;
                        case "WQ":
                        case "BQ":
                            moves.AddRange(Queen.GetMoves(board, board[i, j]));
                            break;
                        case "WK":
                        case "BK":
                            moves.AddRange(King.GetMoves(board, board[i, j]));
                            break;
                    }
                }
            }

            return moves;
        }

        public static List<Move> GetAllMovesForCurrentTurnWithOptimizationVersion2(Cell[,] board, bool isWhiteTurn)
        {
            List<Move> moves = new List<Move>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Piece == null)
                    {
                        continue;
                    }

                    Piece piece = board[i, j].Piece;

                    if (piece.IsWhite != isWhiteTurn)
                    {
                        continue;
                    }

                    switch (piece.Name)
                    {
                        case "WP":
                        case "BP":
                            Pawn.AddMoves(board, board[i, j], moves);
                            break;
                        case "WB":
                        case "BB":
                            Bishop.AddMoves(board, board[i, j], moves);
                            break;
                        case "WN":
                        case "BN":
                            Knight.AddMoves(board, board[i, j], moves);
                            break;
                        case "WR":
                        case "BR":
                            Rook.AddMoves(board, board[i, j], moves);
                            break;
                        case "WQ":
                        case "BQ":
                            Queen.AddMoves(board, board[i, j], moves);
                            break;
                        case "WK":
                        case "BK":
                            King.AddMoves(board, board[i, j], moves);
                            break;
                    }
                }
            }

            return moves;
        }

        public static List<Move> GetAllMovesForCurrentTurnWithOptimizationVersion3(Cell[,] board, bool isWhiteTurn)
        {
            List<Move> moves = new List<Move>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Piece == null)
                    {
                        continue;
                    }

                    Piece piece = board[i, j].Piece;

                    if (piece.IsWhite != isWhiteTurn)
                    {
                        continue;
                    }

                    switch (piece.Name)
                    {
                        case "WP":
                        case "BP":
                            moves.AddRange(Pawn.GetMovesFromCache(board, board[i, j]));
                            break;
                        case "WB":
                        case "BB":
                            moves.AddRange(Bishop.GetMovesFromCache(board, board[i, j]));
                            break;
                        case "WN":
                        case "BN":
                            moves.AddRange(Knight.GetMovesFromCache(board, board[i, j]));
                            break;
                        case "WR":
                        case "BR":
                            moves.AddRange(Rook.GetMovesFromCache(board, board[i, j]));
                            break;
                        case "WQ":
                        case "BQ":
                            moves.AddRange(Queen.GetMovesFromCache(board, board[i, j]));
                            break;
                        case "WK":
                        case "BK":
                            moves.AddRange(King.GetMovesFromCache(board, board[i, j]));
                            break;
                    }
                }
            }

            return moves;
        }

        public static List<Move> GetAllMovesForCurrentTurnUsingBitboards(Cell[,] board, bool isWhiteTurn)
        {
            MovesContainer movesContainer = new MovesContainer();
            movesContainer.MovesList = new List<List<Move>>();

            List<Move> moves = new List<Move>();
            ulong occupancy = Engine.ChessEngine.OccupancyForWhite | Engine.ChessEngine.OccupancyForBlack;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece piece = board[i, j].Piece;

                    if (piece == null)
                    {
                        continue;
                    }

                    if (piece.IsWhite != isWhiteTurn)
                    {
                        continue;
                    }

                    int square = i * 8 + j;

                    switch (piece.Id)
                    {
                        case 1:
                        case 7:
                            movesContainer.MovesList.Add(Pawn.GetMovesFromCache(board, board[i, j]));
                            break;
                        case 2:
                            movesContainer.MovesList.Add(Bishop.GetMovesUsingMagicBitboards(square, BishopMovesHelper.AllPossibleMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 8:
                            movesContainer.MovesList.Add(Bishop.GetMovesUsingMagicBitboards(square, BishopMovesHelper.AllPossibleMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForBlack));
                            break;
                        case 3:
                            movesContainer.MovesList.Add(Knight.GettMovesFromMagicBitboards(square, KnightMovesHelper.AllPossibleMovesFromAllSquares[i, j], Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 9:
                            movesContainer.MovesList.Add(Knight.GettMovesFromMagicBitboards(square, KnightMovesHelper.AllPossibleMovesFromAllSquares[i, j], Engine.ChessEngine.OccupancyForBlack));
                            break;
                        case 4:
                            movesContainer.MovesList.Add(Rook.GetMovesFromMagicBitboards(square, RookMovesHelper.AllPossibleRookMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 10:
                            movesContainer.MovesList.Add(Rook.GetMovesFromMagicBitboards(square, RookMovesHelper.AllPossibleRookMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForBlack));
                            break;
                        case 5:
                            movesContainer.MovesList.Add(Rook.GetMovesFromMagicBitboards(square, RookMovesHelper.AllPossibleRookMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForWhite));
                            movesContainer.MovesList.Add(Bishop.GetMovesUsingMagicBitboards(square, BishopMovesHelper.AllPossibleMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 11:
                            movesContainer.MovesList.Add(Rook.GetMovesFromMagicBitboards(square, RookMovesHelper.AllPossibleRookMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForBlack));
                            movesContainer.MovesList.Add(Bishop.GetMovesUsingMagicBitboards(square, BishopMovesHelper.AllPossibleMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForBlack));
                            break;
                        case 6:
                            movesContainer.MovesList.Add(King.GetMovesUsingMagicBitboards(square, KingMovesHelper.AllPossibleMovesFromAllSquares[i, j], Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 12:
                            movesContainer.MovesList.Add(King.GetMovesUsingMagicBitboards(square, KingMovesHelper.AllPossibleMovesFromAllSquares[i,j], Engine.ChessEngine.OccupancyForBlack));
                            break;
                    }
                }
            }

            return moves;
        }

        public static List<Move> GetKillerMovesForCurrentTurnUsingBitboards(Cell[,] board, bool isWhiteTurn)
        {
            MovesContainer movesContainer = new MovesContainer();
            movesContainer.MovesList = new List<List<Move>>();

            List<Move> moves = new List<Move>();
            ulong occupancy = Engine.ChessEngine.OccupancyForWhite | Engine.ChessEngine.OccupancyForBlack;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece piece = board[i, j].Piece;

                    if (piece == null)
                    {
                        continue;
                    }

                    if (piece.IsWhite != isWhiteTurn)
                    {
                        continue;
                    }

                    int square = i * 8 + j;

                    switch (piece.Id)
                    {
                        case 1:
                        case 7:
                            movesContainer.MovesList.Add(Pawn.GetKillingMovesFromCache(board, board[i, j]));
                            break;
                        case 2:
                            movesContainer.MovesList.Add(Bishop.GetMovesUsingMagicBitboards(square, BishopMovesHelper.AllPossibleMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 8:
                            movesContainer.MovesList.Add(Bishop.GetMovesUsingMagicBitboards(square, BishopMovesHelper.AllPossibleMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForBlack));
                            break;
                        case 3:
                            movesContainer.MovesList.Add(Knight.GettMovesFromMagicBitboards(square, KnightMovesHelper.AllPossibleMovesFromAllSquares[i, j], Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 9:
                            movesContainer.MovesList.Add(Knight.GettMovesFromMagicBitboards(square, KnightMovesHelper.AllPossibleMovesFromAllSquares[i, j], Engine.ChessEngine.OccupancyForBlack));
                            break;
                        case 4:
                            movesContainer.MovesList.Add(Rook.GetMovesFromMagicBitboards(square, RookMovesHelper.AllPossibleRookMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 10:
                            movesContainer.MovesList.Add(Rook.GetMovesFromMagicBitboards(square, RookMovesHelper.AllPossibleRookMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForBlack));
                            break;
                        case 5:
                            movesContainer.MovesList.Add(Rook.GetMovesFromMagicBitboards(square, RookMovesHelper.AllPossibleRookMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForWhite));
                            movesContainer.MovesList.Add(Bishop.GetMovesUsingMagicBitboards(square, BishopMovesHelper.AllPossibleMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 11:
                            movesContainer.MovesList.Add(Rook.GetMovesFromMagicBitboards(square, RookMovesHelper.AllPossibleRookMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForBlack));
                            movesContainer.MovesList.Add(Bishop.GetMovesUsingMagicBitboards(square, BishopMovesHelper.AllPossibleMovesFromAllSquares[i, j], occupancy, Engine.ChessEngine.OccupancyForBlack));
                            break;
                        case 6:
                            movesContainer.MovesList.Add(King.GetMovesUsingMagicBitboards(square, KingMovesHelper.AllPossibleMovesFromAllSquares[i, j], Engine.ChessEngine.OccupancyForWhite));
                            break;
                        case 12:
                            movesContainer.MovesList.Add(King.GetMovesUsingMagicBitboards(square, KingMovesHelper.AllPossibleMovesFromAllSquares[i, j], Engine.ChessEngine.OccupancyForBlack));
                            break;
                    }
                }
            }

            return moves;
        }

        public static List<Move> GetAllKillingMovesForCurrentTurnWithOptimizationVersion3(Cell[,] board, bool isWhiteTurn)
        {
            CacheService cacheService = new CacheService();
            cacheService.InitializeAllPossibleMovesFromEachCellOnBoard();
            List<Move> moves = new List<Move>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Piece == null)
                    {
                        continue;
                    }

                    Piece piece = board[i, j].Piece;

                    if (piece.IsWhite != isWhiteTurn)
                    {
                        continue;
                    }

                    switch (piece.Name)
                    {
                        case "WP":
                        case "BP":
                            moves.AddRange(Pawn.GetKillingMovesFromCache(board, board[i, j]));
                            break;
                        case "WB":
                        case "BB":
                            moves.AddRange(Bishop.GetKillingMovesFromCache(board, board[i, j]));
                            break;
                        case "WN":
                        case "BN":
                            moves.AddRange(Knight.GetKillingMovesFromCache(board, board[i, j]));
                            break;
                        case "WR":
                        case "BR":
                            moves.AddRange(Rook.GetKillingMovesFromCache(board, board[i, j]));
                            break;
                        case "WQ":
                        case "BQ":
                            moves.AddRange(Queen.GetKillingMovesFromCache(board, board[i, j]));
                            break;
                        case "WK":
                        case "BK":
                            moves.AddRange(King.GetKillingMovesFromCache(board, board[i, j]));
                            break;
                    }
                }
            }

            return moves;
        }

        public static string GetBinaryString(ulong number)
        {
            string binaryString = string.Empty;

            for (int i = 7; i >= 0; i--)
            {
                string rowString = string.Empty;

                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;

                    if ((number & one << square) > 0)
                    {
                        rowString += 1;
                    }
                    else
                    {
                        rowString += 0;
                    }
                }

                binaryString += rowString;
            }

            return binaryString;
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static ulong Get2sComplement(ulong number)
        {
            ulong onesComplement = ~number;
            ulong twosComplement = onesComplement + 1;

            return twosComplement;
        }

        public static long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        public static string[,] GetBinaryToBoardInStringArray(ulong allRookMoves, string peiceName = "BP")
        {
            string[,] boardInStringArray =
            {
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
                {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
            };

            for (int k = 0; k < 64; k++)
            {
                int row = maxRowNumber - (k / 8);
                int column = k % 8;

                if ((allRookMoves & (one << k)) > 0)
                {
                    boardInStringArray[row, column] = peiceName;
                }
            }

            return boardInStringArray;
        }
    }
}
