using System;
using System.Collections.Generic;
using System.Text;
using ChessEngine.Helpers;

namespace ChessEngine.Engine
{
    public class ChessEngine
    {
        public static int[] depthValues = { 0, 2, 2, 4, 4, 6, 6, 8, 8 };
        public static Move[] AllPossibleMoves { get; set; }
        public static int NodesEvaluated { get; set; }

        public static int GetScoreUsingMinMaxForKillingMoves(Node node, Cell[,] board, int depth, bool maximizingPlayer, int cost, Move lastMove, Piece lastCapturedPiece)
        {
            NodesEvaluated++;

            if (maximizingPlayer)
            {
                int value = cost;
                List<Move> moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, true);
                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>(node.Moves.Count);

                foreach (Move move in moves)
                {
                    if (board[move.To.Row, move.To.Column].Piece == null)
                    {
                        continue;
                    }

                    int currentMoveCost = cost;
                    int moveId = GetMoveId(move);
                    Node childNode = new Node { MoveId = moveId };
                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;
                    int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
                    board[move.To.Row, move.To.Column].MaximizingPlayerAttacks.Add(moveIdWithPieceId);

                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost + ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
                    }

                    int currentMoveEvaluationValue = GetScoreUsingMinMaxForKillingMoves(childNode, board, depth, !maximizingPlayer, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    node.Costs.Add(moveId, currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Max(value, currentMoveEvaluationValue);
                }

                return value;
            }
            else
            {
                int value = cost;
                List<Move> moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, false);
                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>(node.Moves.Count);

                foreach (Move move in moves)
                {
                    if (board[move.To.Row, move.To.Column].Piece == null)
                    {
                        continue;
                    }

                    int currentMoveCost = cost;
                    int moveId = GetMoveId(move);
                    Node childNode = new Node { MoveId = moveId };
                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;
                    int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
                    board[move.To.Row, move.To.Column].MinimizingPlayerAttacks.Add(moveIdWithPieceId);
                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost - ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
                    }

                    int currentMoveEvaluationValue = GetScoreUsingMinMaxForKillingMoves(childNode, board, depth, !maximizingPlayer, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    node.Costs.Add(GetMoveId(move), currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Min(value, currentMoveEvaluationValue);
                }

                return value;
            }
        }

        public static int GetBestMoveUsingMinMax(Node node, Cell[,] board, int depth, bool maximizingPlayer, int cost, Move lastMove, Piece lastCapturedPiece)
        {
            NodesEvaluated++;

            if (depth == 0)
            {
                List<Move> moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion1(board, maximizingPlayer);
                int value = cost;

                foreach (Move move in moves)
                {
                    Piece currentPiece = board[move.From.Row, move.From.Column].Piece;
                    Piece opponentPiece = board[move.To.Row, move.To.Column].Piece;

                    if (opponentPiece?.Name == "BK")
                    {
                        return cost + BoardHelper.PieceValueDictionary["BK"];
                    }
                    else
                    {
                        if (opponentPiece != null && lastCapturedPiece != null)
                        {
                            cost += opponentPiece.Value;
                            MakeMove(board, move);
                            int currentMoveEvaluationCost = GetScoreUsingMinMaxForKillingMoves(node, board, depth,
                                !maximizingPlayer, cost, move, opponentPiece);
                            value = Math.Max(value, currentMoveEvaluationCost);
                            RevertMove(board, move, opponentPiece);
                        }
                    }
                }

                return value;
            }

            if (maximizingPlayer)
            {
                int value = Int32.MinValue;
                List<Move> moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion1(board, true);
                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>(node.Moves.Count);

                foreach (Move move in moves)
                {
                    int currentMoveCost = cost + moves.Count;
                    int moveId = GetMoveId(move);
                    Node childNode = new Node { MoveId = moveId };
                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;
                    int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
                    board[move.To.Row, move.To.Column].MaximizingPlayerAttacks.Add(moveIdWithPieceId);

                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost + ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
                    }

                    int currentMoveEvaluationValue = GetBestMoveUsingMinMax(childNode, board, depth - 1, !maximizingPlayer, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    node.Costs.Add(moveId, currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Max(value, currentMoveEvaluationValue);
                }

                return value;
            }
            else
            {
                int value = Int32.MaxValue;
                List<Move> moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion1(board, false);
                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>(node.Moves.Count);

                foreach (Move move in moves)
                {
                    int currentMoveCost = cost + (moves.Count * -1);
                    int moveId = GetMoveId(move);
                    Node childNode = new Node { MoveId = moveId };
                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;
                    int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
                    board[move.To.Row, move.To.Column].MinimizingPlayerAttacks.Add(moveIdWithPieceId);
                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost - ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
                    }

                    int currentMoveEvaluationValue = GetBestMoveUsingMinMax(childNode, board, depth - 1, !maximizingPlayer, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    node.Costs.Add(GetMoveId(move), currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Min(value, currentMoveEvaluationValue);
                }

                return value;
            }
        }

        public static void MakeMove(Cell[,] board, Move move)
        {
            Piece piece = board[move.From.Row, move.From.Column].Piece;
            board[move.From.Row, move.From.Column].Piece = null;
            board[move.To.Row, move.To.Column].Piece = piece;
        }

        public static void RevertMove(Cell[,] board, Move move, Piece opponentPiece)
        {
            Piece piece = board[move.To.Row, move.To.Column].Piece;
            board[move.From.Row, move.From.Column].Piece = piece;
            board[move.To.Row, move.To.Column].Piece = opponentPiece;
        }

        public static int GetMoveId(Move move)
        {
            int moveId = (move.From.Row * 8 + move.From.Column) * 100 + (move.To.Row * 8 + move.To.Column);
            return moveId;
        }
    }
}
