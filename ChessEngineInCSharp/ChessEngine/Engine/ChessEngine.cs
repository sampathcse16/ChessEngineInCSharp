using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ChessEngine.Helpers;
using ChessEngine.Pieces;

namespace ChessEngine.Engine
{
    public class ChessEngine
    {
        public static ulong one = 1;
        public static int[] depthValues = { 0, 2, 2, 4, 4, 6, 6, 8, 8, 9, 9, 10, 10 };
        public static Move[] AllPossibleMoves { get; set; }
        public static int NodesEvaluated { get; set; }
        public static int CachedMovesCounter { get; set; }
        public static Dictionary<long, BoardState> TranspositionTable { get; set; }
        public static Dictionary<long, BoardState> TranspositionTableForMaximizer { get; set; }
        public static Dictionary<long, BoardState> TranspositionTableForMinimizer { get; set; }
        public static Dictionary<long, BoardState> TranspositionTableForMaximizerMoves { get; set; }
        public static Dictionary<long, BoardState> TranspositionTableForMinimizerMoves { get; set; }
        public static Dictionary<int, int>[] LevelTranspositionTable { get; set; }
        public static int SearchDeapth { get; set; }
        public static ulong OccupancyForWhite { get; set; }
        public static ulong OccupancyForBlack { get; set; }


        public static void InitializeTranspositionTables(int depth)
        {
            SearchDeapth = depth;
            TranspositionTable = new Dictionary<long, BoardState>();
            TranspositionTableForMaximizer = new Dictionary<long, BoardState>();
            TranspositionTableForMinimizer = new Dictionary<long, BoardState>();
            TranspositionTableForMaximizerMoves = new Dictionary<long, BoardState>();
            TranspositionTableForMinimizerMoves = new Dictionary<long, BoardState>();
            LevelTranspositionTable = new Dictionary<int, int>[depth + 1];

            for (int i = 0; i < LevelTranspositionTable.Length; i++)
            {
                LevelTranspositionTable[i] = new Dictionary<int, int>();
            }
        }

        public static int GetScoreUsingMinMaxForKillingMoves(Node node, Cell[,] board, int alpha, int beta, int depth, bool maximizingPlayer, int cost, Move lastMove, Piece lastCapturedPiece)
        {
            NodesEvaluated++;

            if (depth < -15)
            {

            }

            if (maximizingPlayer)
            {
                int value = Int32.MinValue;
                List<Move> moves = MovesHelper.GetAllKillingMovesForCurrentTurnWithOptimizationVersion3(board, true);
                moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece.Value).ToList();
                int finalMaxCostAfterAllMoves = cost + GetMaxCostBenefit(board, moves);

                if (finalMaxCostAfterAllMoves < -999)
                {
                    return finalMaxCostAfterAllMoves;
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>();

                foreach (Move move in moves)
                {
                    if (board[move.To.Row, move.To.Column].Piece == null)
                    {
                        continue;
                    }
                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;

                    if (backedupOpponentPiece?.Name == "BK")
                    {
                        return cost + BoardHelper.PieceValueDictionary["BK"];
                    }

                    int currentMoveCost = cost;
                    int moveId = GetMoveId(move);
                    Node childNode = new Node { MoveId = moveId };
                    int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
                    board[move.To.Row, move.To.Column].MaximizingPlayerAttacks.Add(moveIdWithPieceId);

                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost + ((backedupOpponentPiece.Value));
                    }

                    int currentMoveEvaluationValue = GetScoreUsingMinMaxForKillingMoves(childNode, board, alpha, beta, depth - 1, !maximizingPlayer, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    node.Costs.Add(moveId, currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Max(value, currentMoveEvaluationValue);
                    value = Math.Max(value, cost);
                    alpha = Math.Max(alpha, value);

                    if (alpha >= beta)
                    {
                        break;
                    }

                    if (value > 999)
                    {
                        return value;
                    }
                }

                if (value == cost)
                {
                    node.Moves.Clear();
                    node.ChildNodes.Clear();
                    node.Costs.Clear();
                }

                return value;
            }
            else
            {
                int value = Int32.MaxValue;
                List<Move> moves = MovesHelper.GetAllKillingMovesForCurrentTurnWithOptimizationVersion3(board, false);
                moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece.Value).ToList();
                int finalMaxCostAfterAllMoves = cost - GetMaxCostBenefit(board, moves);

                if (finalMaxCostAfterAllMoves > 999)
                {
                    return finalMaxCostAfterAllMoves;
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>();

                foreach (Move move in moves)
                {
                    if (board[move.To.Row, move.To.Column].Piece == null)
                    {
                        continue;
                    }

                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;

                    if (backedupOpponentPiece?.Name == "WK")
                    {
                        return cost - BoardHelper.PieceValueDictionary["WK"];
                    }

                    int currentMoveCost = cost;
                    int moveId = GetMoveId(move);
                    Node childNode = new Node { MoveId = moveId };
                    int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
                    board[move.To.Row, move.To.Column].MinimizingPlayerAttacks.Add(moveIdWithPieceId);
                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost - ((backedupOpponentPiece.Value));
                    }

                    int currentMoveEvaluationValue = GetScoreUsingMinMaxForKillingMoves(childNode, board, alpha, beta, depth - 1, !maximizingPlayer, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    node.Costs.Add(GetMoveId(move), currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);

                    value = Math.Min(value, currentMoveEvaluationValue);
                    value = Math.Min(value, cost);
                    beta = Math.Min(beta, value);

                    if (alpha >= beta)
                    {
                        break;
                    }

                    if (value < -999)
                    {
                        return value;
                    }
                }

                if (value == cost)
                {
                    node.Moves.Clear();
                    node.ChildNodes.Clear();
                    node.Costs.Clear();
                }

                return value;
            }
        }

        public static int GetBestMoveUsingMinMax(Node node, Cell[,] board, int depth, bool maximizingPlayer, long zorbistKey, int cost, Move lastMove, Piece lastCapturedPiece)
        {
            NodesEvaluated++;

            if (depth == 0)
            {
                List<Move> moves = MovesHelper.GetAllKillingMovesForCurrentTurnWithOptimizationVersion3(board, maximizingPlayer);
                moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece.Value).ToList();
                int finalMaxCostAfterAllMoves = cost + GetMaxCostBenefit(board, moves);

                if (finalMaxCostAfterAllMoves < -99)
                {
                    return finalMaxCostAfterAllMoves;
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>(node.Moves.Count);
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
                            int moveId = GetMoveId(move);
                            Node childNode = new Node { MoveId = moveId };
                            MakeMove(board, move);
                            int currentMoveEvaluationCost = GetScoreUsingMinMaxForKillingMoves(childNode, board, Int32.MinValue, Int32.MaxValue, depth,
                                !maximizingPlayer, cost + opponentPiece.Value, move, opponentPiece);
                            value = Math.Max(value, currentMoveEvaluationCost);
                            RevertMove(board, move, opponentPiece);
                            node.Costs.Add(GetMoveId(move), currentMoveEvaluationCost);
                            node.ChildNodes.Add(childNode);
                        }
                    }
                }

                if (value == cost)
                {
                    node.Moves.Clear();
                    node.ChildNodes.Clear();
                    node.Costs.Clear();
                }

                return value;
            }

            if (maximizingPlayer)
            {
                int value = Int32.MinValue;
                List<Move> moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, true);
                moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0).ToList();
                int finalMaxCostAfterAllMoves = cost + board[moves[0].To.Row, moves[0].To.Column].Piece?.Value ?? 0;

                if (finalMaxCostAfterAllMoves < -99)
                {
                    return finalMaxCostAfterAllMoves + moves.Count;
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>(node.Moves.Count);

                foreach (Move move in moves)
                {
                    int currentMoveCost = cost + moves.Count + GetOpeningMoveCost(move);
                    int moveId = GetMoveId(move);
                    Node childNode = new Node { MoveId = moveId };
                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;
                    int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
                    board[move.To.Row, move.To.Column].MaximizingPlayerAttacks.Add(moveIdWithPieceId);

                    long zorbistKeyBackup = zorbistKey;
                    zorbistKey = GetUpdatedZorbistKey(board, move, zorbistKey);
                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost + ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
                    }

                    int currentMoveEvaluationValue = GetBestMoveUsingMinMax(childNode, board, depth - 1, !maximizingPlayer, zorbistKey, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    zorbistKey = zorbistKeyBackup;
                    node.Costs.Add(moveId, currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Max(value, currentMoveEvaluationValue);
                }

                //if (!TranspositionTable.ContainsKey(zorbistKey))
                //{
                //    TranspositionTable.Add(zorbistKey, new BoardState { Node = node, IsToBeOrderedBasedOnCost = true });
                //}

                return value;
            }
            else
            {
                int value = Int32.MaxValue;
                List<Move> moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, false);
                moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0).ToList();
                int finalMaxCostAfterAllMoves = cost - board[moves[0].To.Row, moves[0].To.Column].Piece?.Value ?? 0;

                if (finalMaxCostAfterAllMoves > 99)
                {
                    return finalMaxCostAfterAllMoves - moves.Count;
                }

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

                    long zorbistKeyBackup = zorbistKey;
                    zorbistKey = GetUpdatedZorbistKey(board, move, zorbistKey);
                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost - ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
                    }

                    int currentMoveEvaluationValue = GetBestMoveUsingMinMax(childNode, board, depth - 1, !maximizingPlayer, zorbistKey, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    zorbistKey = zorbistKeyBackup;
                    node.Costs.Add(GetMoveId(move), currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Min(value, currentMoveEvaluationValue);
                }

                //if (!TranspositionTable.ContainsKey(zorbistKey))
                //{
                //    TranspositionTable.Add(zorbistKey, new BoardState { Node = node, IsToBeOrderedBasedOnCost = true });
                //}

                return value;
            }
        }

        public static int GetBestMoveUsingAlphaBeta(Node node, Cell[,] board, int depth, int alpha, int beta, bool maximizingPlayer, long zorbistKey, int cost, Move lastMove, Piece lastCapturedPiece)
        {
            NodesEvaluated++;

            if (depth == 0)
            {
                if (TranspositionTable.ContainsKey(zorbistKey))
                {
                    return TranspositionTable[zorbistKey].Cost;
                }

                List<Move> moves = MovesHelper.GetAllKillingMovesForCurrentTurnWithOptimizationVersion3(board, maximizingPlayer);
                moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece.Value).ToList();
                int finalMaxCostAfterAllMoves = cost + GetMaxCostBenefit(board, moves);

                if (finalMaxCostAfterAllMoves < -99)
                {
                    return finalMaxCostAfterAllMoves;
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>(node.Moves.Count);
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
                        //if (opponentPiece != null && lastCapturedPiece != null)
                        {
                            int moveId = GetMoveId(move);
                            Node childNode = new Node { MoveId = moveId };
                            MakeMove(board, move);
                            int currentMoveEvaluationCost = GetScoreUsingMinMaxForKillingMoves(childNode, board, Int32.MinValue, Int32.MaxValue, depth,
                                !maximizingPlayer, cost + (opponentPiece?.Value ?? 0), move, opponentPiece);
                            value = Math.Max(value, currentMoveEvaluationCost);
                            RevertMove(board, move, opponentPiece);
                            node.Costs.Add(GetMoveId(move), currentMoveEvaluationCost);
                            node.ChildNodes.Add(childNode);
                        }
                    }

                    if (value > 99)
                    {
                        return value;
                    }
                }

                if (value == cost)
                {
                    node.Moves.Clear();
                    node.ChildNodes.Clear();
                    node.Costs.Clear();
                }

                return value;
            }

            if (maximizingPlayer)
            {
                int value = Int32.MinValue;
                List<Move> moves = null;
                int maxCostInOneMove = 0;

                if (TranspositionTable.ContainsKey(zorbistKey))
                {
                    BoardState boardState = TranspositionTable[zorbistKey];

                    if (!boardState.IsKillingMoves)
                    {
                        moves = TranspositionTable[zorbistKey].Node.Moves;

                        if (boardState.IsToBeOrderedBasedOnCost)
                        {
                            moves = moves.OrderByDescending(x => boardState.Node.Costs[GetMoveId(x)]).ToList();
                            boardState.Node.Moves = moves;
                            boardState.IsToBeOrderedBasedOnCost = false;
                        }
                    }

                    maxCostInOneMove = moves?.Max(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0) ?? 0;
                }
                else
                {
                    moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, true);
                    moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0).ToList();
                    maxCostInOneMove = board[moves[0].To.Row, moves[0].To.Column].Piece?.Value ?? 0;
                }

                int finalMaxCostAfterAllMoves = cost + maxCostInOneMove;

                if (finalMaxCostAfterAllMoves < -99)
                {
                    bool isOpponenKingIsInCheck = IsOpponentKingIsInCheck(board, moves);

                    if (!isOpponenKingIsInCheck)
                    {
                        return finalMaxCostAfterAllMoves + moves.Count;
                    }
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>(node.Moves.Count);

                foreach (Move move in moves)
                {
                    if (depth == 6
                        && move.From.Row == 0 && move.From.Column == 4
                        && move.To.Row == 1 && move.To.Column == 3)
                    {

                    }

                    if (depth == 4 && move.From.Row == 2 && move.From.Column == 2
                        && move.To.Row == 3 && move.To.Column == 0)
                    {

                    }

                    if (depth == 2)
                    {

                    }

                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;

                    if (backedupOpponentPiece?.Name == "BK")
                    {
                        return cost + BoardHelper.PieceValueDictionary["BK"];
                    }

                    int currentMoveCost = cost + moves.Count + GetOpeningMoveCost(move);
                    int moveId = GetMoveId(move);
                    Node childNode = new Node { MoveId = moveId };

                    int moveIdWithPieceId = (depth * 10000) + moveId;
                    board[move.To.Row, move.To.Column].MaximizingPlayerAttacks.Add(moveIdWithPieceId);

                    long zorbistKeyBackup = zorbistKey;
                    zorbistKey = GetUpdatedZorbistKey(board, move, zorbistKey);
                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost + ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
                    }

                    int currentMoveEvaluationValue = GetBestMoveUsingAlphaBeta(childNode, board, depth - 1, alpha, beta, !maximizingPlayer, zorbistKey, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    zorbistKey = zorbistKeyBackup;
                    node.Costs.Add(moveId, currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Max(value, currentMoveEvaluationValue);

                    if (depth == 6)
                    {

                    }

                    if (depth == 4)
                    {

                    }

                    if (depth == 2)
                    {

                    }

                    alpha = Math.Max(alpha, value);

                    if (alpha >= beta)
                    {
                        break;
                    }

                    if (value > 99 && value >= ((cost / 100) * 100))
                    {
                        return value;
                    }
                }

                return value;
            }
            else
            {
                int value = Int32.MaxValue;
                List<Move> moves = null;
                int maxCostInOneMove = 0;

                if (TranspositionTable.ContainsKey(zorbistKey))
                {
                    BoardState boardState = TranspositionTable[zorbistKey];

                    if (!boardState.IsKillingMoves)
                    {
                        moves = TranspositionTable[zorbistKey].Node.Moves;

                        if (boardState.IsToBeOrderedBasedOnCost)
                        {
                            moves = moves.OrderBy(x => boardState.Node.Costs[GetMoveId(x)]).ToList();
                            boardState.Node.Moves = moves;
                            boardState.IsToBeOrderedBasedOnCost = false;
                        }
                    }

                    maxCostInOneMove = moves?.Max(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0) ?? 0;
                }
                else
                {
                    moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, false);
                    moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0).ToList();
                    maxCostInOneMove = board[moves[0].To.Row, moves[0].To.Column].Piece?.Value ?? 0;
                }


                int finalMaxCostAfterAllMoves = cost - maxCostInOneMove;

                if (finalMaxCostAfterAllMoves > 99)
                {
                    bool isOpponenKingIsInCheck = IsOpponentKingIsInCheck(board, moves);

                    if (!isOpponenKingIsInCheck)
                    {
                        return finalMaxCostAfterAllMoves - moves.Count;
                    }
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>(node.Moves.Count);

                foreach (Move move in moves)
                {
                    if (depth == 5 && move.From.Row == 1 && move.From.Column == 1
                        && move.To.Row == 0 && move.To.Column == 0)
                    {

                    }

                    if (depth == 3 && move.From.Row == 0 && move.From.Column == 0
                        && move.To.Row == 1 && move.To.Column == 1)
                    {

                    }

                    if (depth == 1)
                    {

                    }

                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;

                    if (backedupOpponentPiece?.Name == "WK")
                    {
                        return cost - BoardHelper.PieceValueDictionary["WK"];
                    }

                    int currentMoveCost = cost + (moves.Count * -1);
                    int moveId = GetMoveId(move);
                    Node childNode = new Node { MoveId = moveId };
                    int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
                    board[move.To.Row, move.To.Column].MinimizingPlayerAttacks.Add(moveIdWithPieceId);

                    long zorbistKeyBackup = zorbistKey;
                    zorbistKey = GetUpdatedZorbistKey(board, move, zorbistKey);
                    MakeMove(board, move);

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost - ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
                    }

                    int currentMoveEvaluationValue = GetBestMoveUsingAlphaBeta(childNode, board, depth - 1, alpha, beta, !maximizingPlayer, zorbistKey, currentMoveCost, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    zorbistKey = zorbistKeyBackup;
                    node.Costs.Add(GetMoveId(move), currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Min(value, currentMoveEvaluationValue);

                    if (depth == 5)
                    {

                    }

                    if (depth == 3)
                    {

                    }

                    if (depth == 1)
                    {

                    }

                    beta = Math.Min(beta, value);

                    if (alpha >= beta)
                    {
                        break;
                    }

                    if (value < -99 && value <= ((cost / 100) * 100))
                    {
                        return value;
                    }
                }

                return value;
            }
        }

        public static int GetBestMoveUsingAlphaBetaVersion1(Node node, Cell[,] board, int depth, int alpha, int beta, bool maximizingPlayer, long zorbistKey, int cost, Move lastMove, Piece lastCapturedPiece, Stack<Move> movesStatck, HashSet<int> whiteMoves, HashSet<int> blackMoves)
        {
            NodesEvaluated++;

            //if (maximizingPlayer)
            //{
            //    if (TranspositionTableForMinimizer.ContainsKey(zorbistKey))
            //    {
            //        if (TranspositionTableForMinimizer[zorbistKey].Depth >= depth + 1)
            //        {
            //            return TranspositionTableForMinimizer[zorbistKey].Cost;
            //        }
            //    }
            //}
            //else
            //{
            //    if (TranspositionTableForMaximizer.ContainsKey(zorbistKey))
            //    {
            //        if (TranspositionTableForMaximizer[zorbistKey].Depth >= depth + 1)
            //        {
            //            return TranspositionTableForMaximizer[zorbistKey].Cost;
            //        }
            //    }
            //}

            if (depth == 0)
            {
                List<Move> moves = MovesHelper.GetAllKillingMovesForCurrentTurnWithOptimizationVersion3(board, maximizingPlayer);
                moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece.Value).ToList();
                int finalMaxCostAfterAllMoves = cost + GetMaxCostBenefit(board, moves);

                if (finalMaxCostAfterAllMoves < -999)
                {
                    return finalMaxCostAfterAllMoves;
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>();
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
                        //if (opponentPiece != null && lastCapturedPiece != null)
                        {
                            int moveId = GetMoveId(move);
                            Node childNode = new Node { MoveId = moveId };
                            MakeMove(board, move);
                            int currentMoveEvaluationCost = GetScoreUsingMinMaxForKillingMoves(childNode, board, Int32.MinValue, Int32.MaxValue, depth,
                                !maximizingPlayer, cost + (opponentPiece?.Value ?? 0), move, opponentPiece);
                            value = Math.Max(value, currentMoveEvaluationCost);
                            RevertMove(board, move, opponentPiece);
                            node.Costs.Add(GetMoveId(move), currentMoveEvaluationCost);
                            node.ChildNodes.Add(childNode);
                        }
                    }

                    if (value > 999)
                    {
                        return value;
                    }
                }

                if (value == cost)
                {
                    node.Moves.Clear();
                    node.ChildNodes.Clear();
                    node.Costs.Clear();
                }

                return value;
            }

            if (maximizingPlayer)
            {
                int value = Int32.MinValue;
                List<Move> moves = null;
                int maxCostInOneMove = 0;

                if (TranspositionTableForMaximizerMoves.ContainsKey(zorbistKey))
                {
                    moves = TranspositionTableForMaximizerMoves[zorbistKey].Node.Moves;
                }
                else
                {
                    moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, true);
                    //List<Move> movesToCompare = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, true);

                    //if (moves.Count != movesToCompare.Count)
                    //{

                    //}
                    moves = moves.OrderByDescending(x => GetMoveCostFromLevelTranspositionTable(x, depth, board, whiteMoves, blackMoves, maximizingPlayer)).ToList();
                }

               
                maxCostInOneMove = board[moves[0].To.Row, moves[0].To.Column].Piece?.Value ?? 0;
                int finalMaxCostAfterAllMoves = cost + maxCostInOneMove;

                if (finalMaxCostAfterAllMoves < -999)
                {
                    bool isOpponenKingIsInCheck = IsOpponentKingIsInCheck(board, moves);

                    if (!isOpponenKingIsInCheck)
                    {
                        return finalMaxCostAfterAllMoves + moves.Count;
                    }
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>();

                if (!TranspositionTableForMaximizerMoves.ContainsKey(zorbistKey))
                {
                    TranspositionTableForMaximizerMoves.Add(zorbistKey, new BoardState { Maximizer = true, Node = node, IsToBeOrderedBasedOnCost = true });
                }

                foreach (Move move in moves)
                {
                    if (depth == 6
                        && move.From.Row == 0 && move.From.Column == 1
                        && move.To.Row == 2 && move.To.Column == 2)
                    {

                    }

                    if (depth == 4 && move.From.Row == 2 && move.From.Column == 2
                        && move.To.Row == 4 && move.To.Column == 3)
                    {

                    }

                    if (depth == 2 && move.From.Row == 0 && move.From.Column == 5
                        && move.To.Row == 4 && move.To.Column == 1)
                    {

                    }

                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;

                    if (backedupOpponentPiece?.Name == "BK")
                    {
                        return cost + BoardHelper.PieceValueDictionary["BK"];
                    }

                    int currentMoveCost = cost/* + moves.Count + GetOpeningMoveCost(move) * depth + GetMovePositionCost(board, move, depth) * depth*/;
                    int moveId = GetMoveId(move);
                    int reverseMoveId = GetReverseMoveId(move);
                    Node childNode = new Node { MoveId = moveId };

                    long zorbistKeyBackup = zorbistKey;
                    zorbistKey = GetUpdatedZorbistKey(board, move, zorbistKey);
                    UpdatedOccupancy(board, move);
                    MakeMove(board, move);
                    
                    //if (!IsOccupancyValid(board))
                    //{

                    //}
                    
                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost + ((backedupOpponentPiece.Value) + (5 * depthValues[depth]));
                    }

                    movesStatck.Push(move);
                    whiteMoves.Add(reverseMoveId);
                    int currentMoveEvaluationValue = GetBestMoveUsingAlphaBetaVersion1(childNode, board, depth - 1, alpha, beta, !maximizingPlayer, zorbistKey, currentMoveCost, move, backedupOpponentPiece, movesStatck, whiteMoves, blackMoves);
                    whiteMoves.Remove(reverseMoveId);
                    //if (!TranspositionTableForMaximizer.ContainsKey(zorbistKey))
                    //{
                    //    TranspositionTableForMaximizer.Add(zorbistKey, new BoardState { Maximizer = maximizingPlayer, Cost = currentMoveEvaluationValue, BoardAsString = BoardHelper.GetBoardAsString(board), MovesList = movesStatck.ToList(), Depth = depth });
                    //}

                    movesStatck.Pop();
                    RevertOccupancy(board, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);

                    //if (!IsOccupancyValid(board))
                    //{

                    //}

                    zorbistKey = zorbistKeyBackup;
                    node.Costs.Add(moveId, currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Max(value, currentMoveEvaluationValue);

                    if (depth == 6)
                    {

                    }

                    if (depth == 4)
                    {

                    }

                    if (depth == 2)
                    {

                    }

                    alpha = Math.Max(alpha, value);

                    if (alpha >= beta)
                    {
                        break;
                    }

                    //if (value > 0)
                    //{
                    //    return value;
                    //}
                }

                return value;
            }
            else
            {
                int value = Int32.MaxValue;
                List<Move> moves = null;
                int maxCostInOneMove = 0;

                if (TranspositionTableForMinimizerMoves.ContainsKey(zorbistKey))
                {
                    moves = TranspositionTableForMinimizerMoves[zorbistKey].Node.Moves;
                }
                else
                {
                    moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, false);
                    //List<Move> movesToCompare = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, false);

                    //if (moves.Count != movesToCompare.Count)
                    //{

                    //}
                    moves = moves.OrderBy(x => GetMoveCostFromLevelTranspositionTable(x, depth, board, whiteMoves, blackMoves, maximizingPlayer)).ToList();
                }

                maxCostInOneMove = board[moves[0].To.Row, moves[0].To.Column].Piece?.Value ?? 0;

                int finalMaxCostAfterAllMoves = cost - maxCostInOneMove;

                if (finalMaxCostAfterAllMoves > 999)
                {
                    bool isOpponenKingIsInCheck = IsOpponentKingIsInCheck(board, moves);

                    if (!isOpponenKingIsInCheck)
                    {
                        return finalMaxCostAfterAllMoves - moves.Count;
                    }
                }

                node.Moves = moves;
                node.ChildNodes = new List<Node>();
                node.Costs = new Dictionary<int, int>();

                if (!TranspositionTableForMinimizerMoves.ContainsKey(zorbistKey))
                {
                    TranspositionTableForMinimizerMoves.Add(zorbistKey, new BoardState { Maximizer = maximizingPlayer, Node = node, IsToBeOrderedBasedOnCost = true });
                }

                foreach (Move move in moves)
                {
                    if (depth == 5 && move.From.Row == 7 && move.From.Column == 3
                        && move.To.Row == 5 && move.To.Column == 3)
                    {

                    }

                    if (depth == 3 && move.From.Row == 5 && move.From.Column == 3
                        && move.To.Row == 3 && move.To.Column == 1)
                    {

                    }

                    if (depth == 1 && move.From.Row == 7 && move.From.Column == 3
                        && move.To.Row == 6 && move.To.Column == 3)
                    {

                    }

                    Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;

                    if (backedupOpponentPiece?.Name == "WK")
                    {
                        return cost - BoardHelper.PieceValueDictionary["WK"];
                    }

                    int currentMoveCost = cost /*+ (moves.Count * -1) + ((7 - move.To.Row) * -1)*/;
                    int moveId = GetMoveId(move);
                    int reverseMoveId = GetReverseMoveId(move);
                    Node childNode = new Node { MoveId = moveId, IsWhite = true };
                    
                    long zorbistKeyBackup = zorbistKey;
                    zorbistKey = GetUpdatedZorbistKey(board, move, zorbistKey);

                    UpdatedOccupancy(board, move);
                    MakeMove(board, move);

                    //if (!IsOccupancyValid(board))
                    //{

                    //}

                    if (backedupOpponentPiece != null)
                    {
                        currentMoveCost = currentMoveCost - ((backedupOpponentPiece.Value) + (5 * depthValues[depth]));
                    }

                    movesStatck.Push(move);
                    blackMoves.Add(reverseMoveId);
                    int currentMoveEvaluationValue = GetBestMoveUsingAlphaBetaVersion1(childNode, board, depth - 1, alpha, beta, !maximizingPlayer, zorbistKey, currentMoveCost, move, backedupOpponentPiece, movesStatck, whiteMoves, blackMoves);
                    blackMoves.Remove(reverseMoveId);

                    //if (!TranspositionTableForMinimizer.ContainsKey(zorbistKey))
                    //{
                    //    TranspositionTableForMinimizer.Add(zorbistKey, new BoardState { Maximizer = maximizingPlayer, Cost = currentMoveEvaluationValue, BoardAsString = BoardHelper.GetBoardAsString(board), MovesList = movesStatck.ToList(), Depth = depth });
                    //}

                    movesStatck.Pop();
                    RevertOccupancy(board, move, backedupOpponentPiece);
                    RevertMove(board, move, backedupOpponentPiece);
                    zorbistKey = zorbistKeyBackup;
                    node.Costs.Add(moveId, currentMoveEvaluationValue);
                    node.ChildNodes.Add(childNode);
                    value = Math.Min(value, currentMoveEvaluationValue);

                    if (depth == 5)
                    {

                    }

                    if (depth == 3)
                    {

                    }

                    if (depth == 1)
                    {

                    }

                    beta = Math.Min(beta, value);

                    if (alpha >= beta)
                    {
                        break;
                    }

                    //if (value < -999 /*&& value <= ((cost / 100) * 100)*/)
                    //{
                    //    return value;
                    //}
                }

                return value;
            }
        }

        //public static int GetBestMoveUsingAlphaBetaExperiment(Node node, Cell[,] board, int depth, int alpha, int beta, bool maximizingPlayer, long zorbistKey, int cost, Move lastMove, Piece lastCapturedPiece)
        //{
        //    NodesEvaluated++;

        //    if (depth == 0)
        //    {
        //        if (TranspositionTable.ContainsKey(zorbistKey))
        //        {
        //            return TranspositionTable[zorbistKey].Cost;
        //        }

        //        List<Move> moves = MovesHelper.GetAllKillingMovesForCurrentTurnWithOptimizationVersion3(board, maximizingPlayer);
        //        moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece.Value).ToList();
        //        int finalMaxCostAfterAllMoves = cost + GetMaxCostBenefit(board, moves);

        //        if (finalMaxCostAfterAllMoves < -99)
        //        {
        //            return finalMaxCostAfterAllMoves;
        //        }

        //        node.Moves = moves;
        //        node.ChildNodes = new List<Node>();
        //        node.Costs = new Dictionary<int, int>(node.Moves.Count);
        //        int value = cost;

        //        foreach (Move move in moves)
        //        {
        //            Piece currentPiece = board[move.From.Row, move.From.Column].Piece;
        //            Piece opponentPiece = board[move.To.Row, move.To.Column].Piece;

        //            if (opponentPiece?.Name == "BK")
        //            {
        //                return cost + BoardHelper.PieceValueDictionary["BK"];
        //            }
        //            else
        //            {
        //                //if (opponentPiece != null && lastCapturedPiece != null)
        //                {
        //                    int moveId = GetMoveId(move);
        //                    Node childNode = new Node { MoveId = moveId };
        //                    MakeMove(board, move);
        //                    int currentMoveEvaluationCost = GetScoreUsingMinMaxForKillingMoves(childNode, board, depth,
        //                        !maximizingPlayer, cost + (opponentPiece?.Value ?? 0), move, opponentPiece);
        //                    value = Math.Max(value, currentMoveEvaluationCost);
        //                    RevertMove(board, move, opponentPiece);
        //                    node.Costs.Add(GetMoveId(move), currentMoveEvaluationCost);
        //                    node.ChildNodes.Add(childNode);
        //                }
        //            }

        //            if (value > 99)
        //            {
        //                return value;
        //            }
        //        }

        //        if (value == cost)
        //        {
        //            node.Moves.Clear();
        //            node.ChildNodes.Clear();
        //            node.Costs.Clear();
        //        }

        //        return value;
        //    }

        //    if (maximizingPlayer)
        //    {
        //        int value = Int32.MinValue;
        //        List<Move> moves = null;
        //        int maxCostInOneMove = 0;

        //        if (TranspositionTable.ContainsKey(zorbistKey))
        //        {
        //            BoardState boardState = TranspositionTable[zorbistKey];

        //            if (!boardState.IsKillingMoves)
        //            {
        //                moves = TranspositionTable[zorbistKey].Node.Moves;

        //                if (boardState.IsToBeOrderedBasedOnCost)
        //                {
        //                    moves = moves.OrderByDescending(x => boardState.Node.Costs[GetMoveId(x)]).ToList();
        //                    boardState.Node.Moves = moves;
        //                    boardState.IsToBeOrderedBasedOnCost = false;
        //                }
        //            }

        //            maxCostInOneMove = moves?.Max(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0) ?? 0;
        //        }
        //        else
        //        {
        //            moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, true);
        //            moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0).ToList();
        //            maxCostInOneMove = board[moves[0].To.Row, moves[0].To.Column].Piece?.Value ?? 0;
        //        }

        //        int finalMaxCostAfterAllMoves = cost + maxCostInOneMove;

        //        if (finalMaxCostAfterAllMoves < -99)
        //        {
        //            bool isOpponenKingIsInCheck = IsOpponentKingIsInCheck(board, moves);

        //            if (!isOpponenKingIsInCheck)
        //            {
        //                return finalMaxCostAfterAllMoves + moves.Count;
        //            }
        //        }

        //        node.Moves = moves;
        //        node.ChildNodes = new List<Node>();
        //        node.Costs = new Dictionary<int, int>(node.Moves.Count);

        //        foreach (Move move in moves)
        //        {
        //            if (depth == 6
        //                && move.From.Row == 0 && move.From.Column == 4
        //                && move.To.Row == 1 && move.To.Column == 3)
        //            {

        //            }

        //            if (depth == 4 && move.From.Row == 2 && move.From.Column == 2
        //                && move.To.Row == 3 && move.To.Column == 0)
        //            {

        //            }

        //            if (depth == 2)
        //            {

        //            }

        //            Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;

        //            if (backedupOpponentPiece?.Name == "BK")
        //            {
        //                return cost + BoardHelper.PieceValueDictionary["BK"];
        //            }

        //            int currentMoveCost = cost + moves.Count + GetOpeningMoveCost(move);
        //            int moveId = GetMoveId(move);
        //            Node childNode = new Node { MoveId = moveId };

        //            int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
        //            board[move.To.Row, move.To.Column].MaximizingPlayerAttacks.Add(moveIdWithPieceId);

        //            long zorbistKeyBackup = zorbistKey;
        //            zorbistKey = GetUpdatedZorbistKey(board, move, zorbistKey);
        //            MakeMove(board, move);

        //            if (backedupOpponentPiece != null)
        //            {
        //                currentMoveCost = currentMoveCost + ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
        //            }

        //            int currentMoveEvaluationValue = GetBestMoveUsingAlphaBetaExperiment(childNode, board, depth - 1, alpha, beta, !maximizingPlayer, zorbistKey, currentMoveCost, move, backedupOpponentPiece);
        //            RevertMove(board, move, backedupOpponentPiece);
        //            zorbistKey = zorbistKeyBackup;
        //            node.Costs.Add(moveId, currentMoveEvaluationValue);
        //            node.ChildNodes.Add(childNode);
        //            value = Math.Max(value, currentMoveEvaluationValue);

        //            if (depth == 6)
        //            {

        //            }

        //            if (depth == 4)
        //            {

        //            }

        //            if (depth == 2)
        //            {

        //            }

        //            alpha = Math.Max(alpha, value);

        //            if (alpha >= beta)
        //            {
        //                break;
        //            }

        //            if (value > -50)
        //            {
        //                return value;
        //            }
        //        }

        //        return value;
        //    }
        //    else
        //    {
        //        int value = Int32.MaxValue;
        //        List<Move> moves = null;
        //        int maxCostInOneMove = 0;

        //        if (TranspositionTable.ContainsKey(zorbistKey))
        //        {
        //            BoardState boardState = TranspositionTable[zorbistKey];

        //            if (!boardState.IsKillingMoves)
        //            {
        //                moves = TranspositionTable[zorbistKey].Node.Moves;

        //                if (boardState.IsToBeOrderedBasedOnCost)
        //                {
        //                    moves = moves.OrderBy(x => boardState.Node.Costs[GetMoveId(x)]).ToList();
        //                    boardState.Node.Moves = moves;
        //                    boardState.IsToBeOrderedBasedOnCost = false;
        //                }
        //            }

        //            maxCostInOneMove = moves?.Max(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0) ?? 0;
        //        }
        //        else
        //        {
        //            moves = MovesHelper.GetAllMovesForCurrentTurnWithOptimizationVersion3(board, false);
        //            moves = moves.OrderByDescending(x => board[x.To.Row, x.To.Column].Piece?.Value ?? 0).ToList();
        //            maxCostInOneMove = board[moves[0].To.Row, moves[0].To.Column].Piece?.Value ?? 0;
        //        }


        //        int finalMaxCostAfterAllMoves = cost - maxCostInOneMove;

        //        if (finalMaxCostAfterAllMoves > 99)
        //        {
        //            bool isOpponenKingIsInCheck = IsOpponentKingIsInCheck(board, moves);

        //            if (!isOpponenKingIsInCheck)
        //            {
        //                return finalMaxCostAfterAllMoves - moves.Count;
        //            }
        //        }

        //        node.Moves = moves;
        //        node.ChildNodes = new List<Node>();
        //        node.Costs = new Dictionary<int, int>(node.Moves.Count);

        //        foreach (Move move in moves)
        //        {
        //            if (depth == 5 && move.From.Row == 1 && move.From.Column == 1
        //                && move.To.Row == 0 && move.To.Column == 0)
        //            {

        //            }

        //            if (depth == 3 && move.From.Row == 0 && move.From.Column == 0
        //                && move.To.Row == 1 && move.To.Column == 1)
        //            {

        //            }

        //            if (depth == 1)
        //            {

        //            }

        //            Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;

        //            if (backedupOpponentPiece?.Name == "WK")
        //            {
        //                return cost - BoardHelper.PieceValueDictionary["WK"];
        //            }

        //            int currentMoveCost = cost + (moves.Count * -1);
        //            int moveId = GetMoveId(move);
        //            Node childNode = new Node { MoveId = moveId };
        //            int moveIdWithPieceId = (BoardHelper.PieceIdDictionary[board[move.From.Row, move.From.Column].Piece.Name] * 10000) + moveId;
        //            board[move.To.Row, move.To.Column].MinimizingPlayerAttacks.Add(moveIdWithPieceId);

        //            long zorbistKeyBackup = zorbistKey;
        //            zorbistKey = GetUpdatedZorbistKey(board, move, zorbistKey);
        //            MakeMove(board, move);

        //            if (backedupOpponentPiece != null)
        //            {
        //                currentMoveCost = currentMoveCost - ((backedupOpponentPiece.Value) + (10 * depthValues[depth]));
        //            }

        //            int currentMoveEvaluationValue = GetBestMoveUsingAlphaBetaExperiment(childNode, board, depth - 1, alpha, beta, !maximizingPlayer, zorbistKey, currentMoveCost, move, backedupOpponentPiece);
        //            RevertMove(board, move, backedupOpponentPiece);
        //            zorbistKey = zorbistKeyBackup;
        //            node.Costs.Add(GetMoveId(move), currentMoveEvaluationValue);
        //            node.ChildNodes.Add(childNode);
        //            value = Math.Min(value, currentMoveEvaluationValue);

        //            if (depth == 5)
        //            {

        //            }

        //            if (depth == 3)
        //            {

        //            }

        //            if (depth == 1)
        //            {

        //            }

        //            beta = Math.Min(beta, value);

        //            if (alpha >= beta)
        //            {
        //                break;
        //            }

        //            if (value < -99 && value <= ((cost / 100) * 100))
        //            {
        //                return value;
        //            }
        //        }

        //        return value;
        //    }
        //}

        public static void UpdateOccupancies(Cell[,] board)
        {
            OccupancyForWhite = 0;
            OccupancyForBlack = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;

                    if (board[i, j].Piece != null)
                    {
                        if (board[i, j].Piece.IsWhite)
                        {
                            OccupancyForWhite = OccupancyForWhite | one << square;
                        }
                        else
                        {
                            OccupancyForBlack = OccupancyForBlack | one << square;
                        }
                    }
                }
            }
        }

        public static void MakeMove(Cell[,] board, Move move)
        {
            Piece piece = board[move.From.Row, move.From.Column].Piece;
            board[move.From.Row, move.From.Column].Piece = null;
            board[move.To.Row, move.To.Column].Piece = piece;

            if (piece.Name == "WK")
            {
                if (move.From.Row == 0 && move.From.Column == 4)
                {
                    if (move.To.Row == 0 && move.To.Column == 6)
                    {
                        board[0, 5].Piece = board[0, 7].Piece;
                        board[0, 7].Piece = null;
                    }
                    else if (move.To.Row == 0 && move.To.Column == 2)
                    {
                        board[0, 3].Piece = board[0, 0].Piece;
                        board[0, 0].Piece = null;
                    }
                }
            }

            if (piece.Name == "BK")
            {
                if (move.From.Row == 7 && move.From.Column == 4)
                {
                    if (move.To.Row == 7 && move.To.Column == 6)
                    {
                        board[7, 5].Piece = board[7, 7].Piece;
                        board[7, 7].Piece = null;
                    }
                    else if (move.To.Row == 7 && move.To.Column == 2)
                    {
                        board[7, 3].Piece = board[7, 0].Piece;
                        board[7, 0].Piece = null;
                    }
                }
            }
        }

        public static void RevertMove(Cell[,] board, Move move, Piece opponentPiece)
        {
            Piece piece = board[move.To.Row, move.To.Column].Piece;

            if (piece.Name == "WK")
            {
                if (move.From.Row == 0 && move.From.Column == 4)
                {
                    if (move.To.Row == 0 && move.To.Column == 6)
                    {
                        board[0, 7].Piece = board[0, 5].Piece;
                        board[0, 5].Piece = null;
                    }
                    else if (move.To.Row == 0 && move.To.Column == 2)
                    {
                        board[0, 0].Piece = board[0, 3].Piece;
                        board[0, 3].Piece = null;
                    }
                }
            }

            if (piece.Name == "BK")
            {
                if (move.From.Row == 7 && move.From.Column == 4)
                {
                    if (move.To.Row == 7 && move.To.Column == 6)
                    {
                        board[7, 7].Piece = board[7, 5].Piece;
                        board[7, 5].Piece = null;
                    }
                    else if (move.To.Row == 7 && move.To.Column == 2)
                    {
                        board[7, 0].Piece = board[7, 3].Piece;
                        board[7, 3].Piece = null;
                    }
                }
            }

            board[move.From.Row, move.From.Column].Piece = piece;
            board[move.To.Row, move.To.Column].Piece = opponentPiece;
        }

        public static void RevertOccupancy(Cell[,] board, Move move, Piece opponentPiece)
        {
            Piece piece = board[move.To.Row, move.To.Column].Piece;
            int toSquare = move.From.Row * 8 + move.From.Column;
            int fromSquare = move.To.Row * 8 + move.To.Column;

            if (piece.IsWhite)
            {
                OccupancyForWhite = OccupancyForWhite & ~(one << fromSquare);
                OccupancyForWhite = OccupancyForWhite | (one << toSquare);

                if (opponentPiece != null)
                {
                    OccupancyForBlack = OccupancyForBlack | (one << fromSquare);
                }
            }
            else
            {
                OccupancyForBlack = OccupancyForBlack & ~(one << fromSquare);
                OccupancyForBlack = OccupancyForBlack | (one << toSquare);

                if (opponentPiece != null)
                {
                    OccupancyForWhite = OccupancyForWhite | (one << fromSquare);
                }
            }

            if (piece.Name == "WK")
            {
                if (move.From.Row == 0 && move.From.Column == 4)
                {
                    if (move.To.Row == 0 && move.To.Column == 6)
                    {
                        toSquare = 0 * 8 + 7;
                        fromSquare = 0 * 8 + 5;

                        OccupancyForWhite = OccupancyForWhite & ~(one << fromSquare);
                        OccupancyForWhite = OccupancyForWhite | (one << toSquare);
                    }
                    else if (move.To.Row == 0 && move.To.Column == 2)
                    {
                        toSquare = 0 * 8 + 0;
                        fromSquare = 0 * 8 + 3;

                        OccupancyForWhite = OccupancyForWhite & ~(one << fromSquare);
                        OccupancyForWhite = OccupancyForWhite | (one << toSquare);
                    }
                }
            }

            if (piece.Name == "BK")
            {
                if (move.From.Row == 7 && move.From.Column == 4)
                {
                    if (move.To.Row == 7 && move.To.Column == 6)
                    {
                        toSquare = 7 * 8 + 7;
                        fromSquare = 7 * 8 + 5;

                        OccupancyForBlack = OccupancyForBlack & ~(one << fromSquare);
                        OccupancyForBlack = OccupancyForBlack | (one << toSquare);
                    }
                    else if (move.To.Row == 7 && move.To.Column == 2)
                    {
                        toSquare = 7 * 8 + 0;
                        fromSquare = 7 * 8 + 3;

                        OccupancyForBlack = OccupancyForBlack & ~(one << fromSquare);
                        OccupancyForBlack = OccupancyForBlack | (one << toSquare);
                    }
                }
            }
        }

        public static int GetMoveId(Move move)
        {
            if (move != null)
            {
                int moveId = (move.From.Row * 8 + move.From.Column) * 100 + (move.To.Row * 8 + move.To.Column);
                return moveId;
            }
            else
            {
                return 0;
            }
        }

        public static int GetReverseMoveId(Move move)
        {
            int moveId = (move.To.Row * 8 + move.To.Column) * 100 + (move.From.Row * 8 + move.From.Column);
            return moveId;
        }

        public static int GetMaxCostBenefit(Cell[,] board, List<Move> moves)
        {
            int maxCostBenifit = 0;

            foreach (Move move in moves)
            {
                Piece opponentPiece = board[move.To.Row, move.To.Column].Piece;
                maxCostBenifit = Math.Max(maxCostBenifit, opponentPiece?.Value ?? 0);
            }

            return maxCostBenifit;
        }

        public static long GetUpdatedZorbistKey(Cell[,] board, Move move, long zorbistKey)
        {
            Piece currentPiece = board[move.From.Row, move.From.Column].Piece;
            Piece opponentPiece = board[move.To.Row, move.To.Column].Piece;
            zorbistKey ^= board[move.From.Row, move.From.Column].ZorbistKeys[currentPiece.Name];

            if (opponentPiece != null)
            {
                zorbistKey ^= board[move.To.Row, move.To.Column].ZorbistKeys[opponentPiece.Name];
            }

            zorbistKey ^= board[move.To.Row, move.To.Column].ZorbistKeys[currentPiece.Name];

            if (currentPiece.Name == "WK")
            {
                if (move.From.Row == 0 && move.From.Column == 4)
                {
                    if (move.To.Row == 0 && move.To.Column == 2)
                    {
                        zorbistKey ^= board[0, 0].ZorbistKeys["WR"];
                        zorbistKey ^= board[0, 3].ZorbistKeys["WR"];
                    }
                    else if (move.To.Row == 0 && move.To.Column == 6)
                    {
                        zorbistKey ^= board[0, 7].ZorbistKeys["WR"];
                        zorbistKey ^= board[0, 5].ZorbistKeys["WR"];
                    }
                }
            }

            if (currentPiece.Name == "BK")
            {
                if (move.From.Row == 7 && move.From.Column == 4)
                {
                    if (move.To.Row == 7 && move.To.Column == 2)
                    {
                        zorbistKey ^= board[7, 0].ZorbistKeys["BR"];
                        zorbistKey ^= board[7, 3].ZorbistKeys["BR"];
                    }
                    else if (move.To.Row == 7 && move.To.Column == 6)
                    {
                        zorbistKey ^= board[7, 7].ZorbistKeys["BR"];
                        zorbistKey ^= board[7, 5].ZorbistKeys["BR"];
                    }
                }
            }

            return zorbistKey;
        }

        public static void UpdatedOccupancy(Cell[,] board, Move move)
        {
            Piece currentPiece = board[move.From.Row, move.From.Column].Piece;
            Piece opponentPiece = board[move.To.Row, move.To.Column].Piece;
            int fromSquare = move.From.Row * 8 + move.From.Column;
            int toSquare = move.To.Row * 8 + move.To.Column;

            if (currentPiece.IsWhite)
            {
                OccupancyForWhite = OccupancyForWhite & ~(one << fromSquare);
                OccupancyForWhite = OccupancyForWhite | (one << toSquare);

                if (opponentPiece != null)
                {
                    OccupancyForBlack = OccupancyForBlack & ~(one << toSquare);
                }
            }
            else
            {
                OccupancyForBlack = OccupancyForBlack & ~(one << fromSquare);
                OccupancyForBlack = OccupancyForBlack | (one << toSquare);

                if (opponentPiece != null)
                {
                    OccupancyForWhite = OccupancyForWhite & ~(one << toSquare);
                }
            }

            if (currentPiece.Name == "WK")
            {
                if (move.From.Row == 0 && move.From.Column == 4)
                {
                    if (move.To.Row == 0 && move.To.Column == 2)
                    {
                        fromSquare = 0 * 8 + 0;
                        toSquare = 0 * 8 + 3;

                        OccupancyForWhite = OccupancyForWhite & ~(one << fromSquare);
                        OccupancyForWhite = OccupancyForWhite | (one << toSquare);
                    }
                    else if (move.To.Row == 0 && move.To.Column == 6)
                    {
                        fromSquare = 0 * 8 + 7;
                        toSquare = 0 * 8 + 5;

                        OccupancyForWhite = OccupancyForWhite & ~(one << fromSquare);
                        OccupancyForWhite = OccupancyForWhite | (one << toSquare);
                    }
                }
            }

            if (currentPiece.Name == "BK")
            {
                if (move.From.Row == 7 && move.From.Column == 4)
                {
                    if (move.To.Row == 7 && move.To.Column == 2)
                    {
                        fromSquare = 7 * 8 + 0;
                        toSquare = 7 * 8 + 3;

                        OccupancyForBlack = OccupancyForBlack & ~(one << fromSquare);
                        OccupancyForBlack = OccupancyForBlack | (one << toSquare);
                    }
                    else if (move.To.Row == 7 && move.To.Column == 6)
                    {
                        fromSquare = 7 * 8 + 7;
                        toSquare = 7 * 8 + 5;

                        OccupancyForBlack = OccupancyForBlack & ~(one << fromSquare);
                        OccupancyForBlack = OccupancyForBlack | (one << toSquare);
                    }
                }
            }
        }

        public static int GetOpeningMoveCost(Move move)
        {
            if (move.To.Row == 3 && move.To.Column == 3)
            {
                return 10;
            }

            if (move.To.Row == 3 && move.To.Column == 4)
            {
                return 10;
            }

            if (move.From.Row == 0 && move.From.Column == 1)
            {
                return 10;
            }

            if (move.From.Row == 0 && move.From.Column == 6)
            {
                return 10;
            }

            if (move.From.Row == 0 && move.From.Column == 2)
            {
                return 10;
            }

            if (move.From.Row == 0 && move.From.Column == 5)
            {
                return 10;
            }

            if (move.From.Row == 0 && move.From.Column == 4
            && move.To.Row == 0 && move.To.Column == 6)
            {
                return 20;
            }

            if (move.From.Row == 0 && move.From.Column == 4
                                   && move.To.Row == 0 && move.To.Column == 2)
            {
                return 20;
            }

            if (move.From.Row == 0 && move.From.Column == 4
                                   && move.To.Row == 0 && move.To.Column == 5)
            {
                return -20;
            }

            if (move.From.Row == 0 && move.From.Column == 4
                                   && move.To.Row == 0 && move.To.Column == 3)
            {
                return -20;
            }

            if (move.From.Row == 0 && move.From.Column == 4
                                   && move.To.Row == 1 && move.To.Column == 3)
            {
                return -20;
            }

            if (move.From.Row == 0 && move.From.Column == 4
                                   && move.To.Row == 1 && move.To.Column == 4)
            {
                return -520;
            }

            return 0;
        }

        public static bool IsOpponentKingIsInCheck(Cell[,] board, List<Move> moves)
        {
            foreach (Move move in moves)
            {
                bool isOpponetKingInCheck = false;

                if (board[move.To.Row, move.To.Column] == null)
                {
                    break;
                }

                Piece currentPiece = board[move.From.Row, move.From.Column].Piece;

                if (currentPiece.Name[1] == 'K')
                {
                    continue;
                }

                Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;
                MakeMove(board, move);

                switch (currentPiece.Name)
                {
                    case "WP":
                    case "BP":
                        isOpponetKingInCheck = Pawn.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                        break;
                    case "WB":
                    case "BB":
                        isOpponetKingInCheck = Bishop.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                        break;
                    case "WN":
                    case "BN":
                        isOpponetKingInCheck = Knight.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                        break;
                    case "WR":
                    case "BR":
                        isOpponetKingInCheck = Rook.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                        break;
                    case "WQ":
                    case "BQ":
                        isOpponetKingInCheck = Queen.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                        break;
                }

                RevertMove(board, move, backedupOpponentPiece);

                if (isOpponetKingInCheck)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsOpponentKingIsInCheckWithThisMove(Cell[,] board, Move move)
        {
            bool isOpponetKingInCheck = false;

            Piece currentPiece = board[move.From.Row, move.From.Column].Piece;

            if (currentPiece.Name[1] == 'K')
            {
                return false;
            }

            Piece backedupOpponentPiece = board[move.To.Row, move.To.Column].Piece;
            MakeMove(board, move);

            switch (currentPiece.Name)
            {
                case "WP":
                case "BP":
                    isOpponetKingInCheck = Pawn.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                    break;
                case "WB":
                case "BB":
                    isOpponetKingInCheck = Bishop.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                    break;
                case "WN":
                case "BN":
                    isOpponetKingInCheck = Knight.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                    break;
                case "WR":
                case "BR":
                    isOpponetKingInCheck = Rook.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                    break;
                case "WQ":
                case "BQ":
                    isOpponetKingInCheck = Queen.IsOpponentKingIsInCheck(board, board[move.To.Row, move.To.Column]);
                    break;
            }

            RevertMove(board, move, backedupOpponentPiece);

            if (isOpponetKingInCheck)
            {
                return true;
            }

            return false;
        }

        public static int GetMoveCostFromLevelTranspositionTable(Move move, int depth, Cell[,] board, HashSet<int> whiteMoves, HashSet<int> blackMoves, bool maximizer)
        {
            Piece piece = board[move.From.Row, move.From.Column].Piece;
            int moveId = GetMoveId(move);
            int moveCostForCurrentLevel = piece.MinValue;
            bool isOpponentKingInCheck = IsOpponentKingIsInCheckWithThisMove(board, move);

            if (maximizer)
            {
                if (board[move.To.Row, move.To.Column].Piece != null)
                {
                    return board[move.To.Row, move.To.Column].Piece.Value;
                }

                if (whiteMoves.Contains(moveId))
                {
                    return -100;
                }

                if (isOpponentKingInCheck)
                {
                    return 50000;
                }

                if (piece.Name[1] != 'P')
                {
                    if (move.To.Row + 1 < 8 && move.To.Column + 1 < 8 && board[move.To.Row + 1, move.To.Column + 1].Piece != null)
                    {
                        if (!board[move.To.Row + 1, move.To.Column + 1].Piece.IsWhite
                            && board[move.To.Row + 1, move.To.Column + 1].Piece.Name[1] == 'P')
                        {
                            return piece.Value * -1;
                        }
                    }

                    if (move.To.Row + 1 < 8 && move.To.Column - 1 >= 0 && board[move.To.Row + 1, move.To.Column - 1].Piece != null)
                    {
                        if (!board[move.To.Row + 1, move.To.Column - 1].Piece.IsWhite
                            && board[move.To.Row + 1, move.To.Column - 1].Piece.Name[1] == 'P')
                        {
                            return piece.Value * -1;
                        }
                    }
                }

                if (Game.LastMovedPieceForWhite == piece)
                {
                    return -500;
                }

                return GetMoveCostFromPieceSquareTable(piece, move);
            }

            if (board[move.To.Row, move.To.Column].Piece != null)
            {
                return board[move.To.Row, move.To.Column].Piece.Value * -1;
            }

            if (blackMoves.Contains(moveId))
            {
                return 100;
            }

            if (isOpponentKingInCheck)
            {
                return -50000;
            }

            if (piece.Name[1] != 'P')
            {
                if (move.To.Row - 1 >= 0 && move.To.Column + 1 < 8 && board[move.To.Row - 1, move.To.Column + 1].Piece != null)
                {
                    if (board[move.To.Row - 1, move.To.Column + 1].Piece.IsWhite
                        && board[move.To.Row - 1, move.To.Column + 1].Piece.Name[1] == 'P')
                    {
                        return piece.Value;
                    }
                }

                if (move.To.Row - 1 >= 0 && move.To.Column - 1 >= 0 && board[move.To.Row - 1, move.To.Column - 1].Piece != null)
                {
                    if (board[move.To.Row - 1, move.To.Column - 1].Piece.IsWhite
                        && board[move.To.Row - 1, move.To.Column - 1].Piece.Name[1] == 'P')
                    {
                        return piece.Value;
                    }
                }
            }

            return (moveCostForCurrentLevel + 7 - move.To.Row) * -1;
        }

        public static int GetMoveCostFromTranspositionTable(Node node, Move move)
        {
            int moveId = GetMoveId(move);

            if (node.Costs.ContainsKey(moveId))
            {
                return node.Costs[moveId];
            }

            return 0;
        }

        public static int GetMovePositionCost(Cell[,] board, Move move, int depth)
        {
            if (SearchDeapth != depth)
            {
                return 0;
            }

            if (Game.TotalMovesPlayed == 0)
            {
                if (move.From.Row == 1 && move.From.Column == 4
                                       && move.From.Row == 3 && move.From.Column == 4)
                {
                    return 10;
                }
            }

            Piece piece = board[move.From.Row, move.From.Column].Piece;

            if (piece == Game.LastMovedPieceForWhite)
            {
                return -20;
            }

            if (Game.TotalMovesPlayed < 10)
            {
                if (board[move.From.Row, move.From.Column].Piece.Name[1] == 'Q')
                {
                    return -10;
                }
            }

            if (piece.Name[1] != 'K')
            {
                return (move.To.Row * board[move.From.Row, move.From.Column].Piece.MinValue);
            }

            if (move.From.Row == 0 && move.From.Column == 4)
            {
                if (move.To.Row == 0 && move.To.Column == 6
                || move.To.Row == 0 && move.To.Column == 2)
                {
                    return 100;
                }
            }

            return 0;
        }
        
        public static string GetMoveTree(Node node)
        {
            string moveTree = string.Empty;
            Move move = null;

            while (node != null)
            {
                if (node.IsWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[GetMoveId(x)]).FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[GetMoveId(x)]).FirstOrDefault();
                }

                if (move == null)
                {
                    break;
                }

                moveTree += $"From:{move.From.Row}, {move.From.Column}->To:{move.To.Row}, {move.To.Column}->{node.Costs[GetMoveId(move)]}\n";
                node = node.ChildNodes?.FirstOrDefault(x => x.MoveId == GetMoveId(move));
            }

            return moveTree;
        }

        public static List<Move> GetMoveTreeList(Node node)
        {
            List<Move> moves = new List<Move>();
            string moveTree = string.Empty;
            Move move = null;

            while (node != null)
            {
                if (node.IsWhite)
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(GetMoveId(x)))
                        ?.OrderByDescending(x => node.Costs[GetMoveId(x)]).FirstOrDefault();
                }
                else
                {
                    move = node.Moves?.Where(x => node.Costs.ContainsKey(GetMoveId(x)))
                        ?.OrderBy(x => node.Costs[GetMoveId(x)]).FirstOrDefault();
                }

                if (move == null)
                {
                    break;
                }

                moves.Add(move);
                node = node.ChildNodes?.FirstOrDefault(x => x.MoveId == GetMoveId(move));
            }

            return moves;
        }

        public static int GetMoveCostFromPieceSquareTable(Piece piece, Move move)
        {
            switch (piece.Name)
            {
                case "WP":
                    return PieceSquareTable.Pawn[move.To.Row, move.To.Column] -
                         PieceSquareTable.Pawn[move.From.Row, move.From.Column];
                case "WN":
                    return PieceSquareTable.Knight[move.To.Row, move.To.Column] -
                           PieceSquareTable.Knight[move.From.Row, move.From.Column];
                case "WB":
                    return PieceSquareTable.Bishop[move.To.Row, move.To.Column] -
                           PieceSquareTable.Bishop[move.From.Row, move.From.Column];
                case "WR":
                    return PieceSquareTable.Rook[move.To.Row, move.To.Column] -
                           PieceSquareTable.Rook[move.From.Row, move.From.Column];
                case "WQ":
                    return PieceSquareTable.Queen[move.To.Row, move.To.Column] -
                           PieceSquareTable.Queen[move.From.Row, move.From.Column];
                case "WK":
                    return PieceSquareTable.King[move.To.Row, move.To.Column] -
                           PieceSquareTable.King[move.From.Row, move.From.Column];
            }

            return 0;
        }

        public static bool IsOccupancyValid(Cell[,] board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;

                    if (board[i, j].Piece != null)
                    {
                        if (board[i, j].Piece.Name[0] == 'W')
                        {
                            if ((OccupancyForWhite & (one << square)) == 0)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if ((OccupancyForBlack & (one << square)) == 0)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if ((OccupancyForWhite & (one << square)) != 0)
                        {
                            return false;
                        }

                        if ((OccupancyForBlack & (one << square)) != 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
