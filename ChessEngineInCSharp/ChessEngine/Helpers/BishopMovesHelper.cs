using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine.Pieces;
using UI.Services;

namespace ChessEngine.Helpers
{
    public class BishopMovesHelper
    {
        static ulong one = 1;

        static int maxRowNumber = 7;

        public static ulong[,] AllPossibleMovesFromAllSquares { get; set; }

        public static List<Move>[,] BinaryToActualMoves { get; set; }

        public static ulong[,] BlockerMovesToBinaryMoves { get; set; }

        public static HashSet<ulong>[] AllBinaryMoves { get; set; }

        public static Dictionary<ulong, ulong>[] BishopBlockerMovesToBinaryMovesDictionary { get; set; }

        public static ulong HashKeyForBishopMoves = 59351;

        public static ulong[] MagicNumbersForBlockers =
        {
            9007233631813760
            ,5478774082501640244
            ,4620706412362768384
            ,4573969463115776
            ,1317450500576186528
            ,6765570007500804
            ,287006911373824
            ,2377900878397976576
            ,578163075493037124
            ,1254577261830272
            ,78830586772335616
            ,54711701282625664
            ,306527357773021472
            ,72093328174236177
            ,24822580952191488
            ,2306045456861435010
            ,2308104979516269328
            ,562950494519952
            ,2260632481115408
            ,1450194273017270784
            ,1153273351817658448
            ,562984339660800
            ,2395916103454298432
            ,5514805117440
            ,4504183743971585
            ,4785635097592064
            ,19997636692128
            ,3315848972354
            ,5774179879891502080
            ,844429258949120
            ,2450029674136478276
            ,7494344459686003233
            ,4755942496434536512
            ,18023207559495944
            ,586593868663755776
            ,240518503684
            ,35753723707648
            ,36802861794984064
            ,18036663623107584
            ,216243176629862720
            ,1154047997789620512
            ,576461304208819350
            ,721555176816672
            ,4620838387845695488
            ,184647602439242112
            ,26405593235700
            ,3659742707190112
            ,3332899883346
            ,2882307062216278024
            ,2456150681170494464
            ,72374597529600000
            ,2305851811757556016
            ,580973148174821376
            ,2882866739119439936
            ,27022700801228800
            ,1443579671169221312
            ,328775966971429378
            ,2253998841144368
            ,18017835575937024
            ,9072581615624
            ,576460825854763072
            ,4506213115250952
            ,2305985398121244936
            ,10445969305744
        };

        public static ulong[] MagicNumbersForActualMoves =
        {
            82195126146827329
            ,432556705626460696
            ,288450278779535426
            ,1134704627681317
            ,36310297832849448
            ,4611721271588159492
            ,55310013381492738
            ,90092067238182914
            ,9288742984484865
            ,1158647765878726821
            ,185984866276380689
            ,36188228352754176
            ,4909334484072579
            ,2958196735715049616
            ,5336908497177084496
            ,432392861632331780
            ,144396663253925891
            ,1157460426120779778
            ,738660742814794305
            ,72064960108626177
            ,450518309592400480
            ,2328396208912992789
            ,36372396164272130
            ,580680115816970
            ,1729946312285358089
            ,74313791933915457
            ,2269426496094742
            ,4630830715964494343
            ,6350431716359931972
            ,291046242820032520
            ,2324000622066008336
            ,2522315133905076356
            ,47292473373917190
            ,2307252893503194375
            ,2377909417598275587
            ,565424064333842
            ,1155182109139141123
            ,14636836634755360
            ,653408978894065729
            ,144397358839365736
            ,72347899752644681
            ,145273385189521
            ,563774872355854
            ,72062578415011844
            ,577024324255352902
            ,4647733524374241349
            ,91765550230880772
            ,22676328920653988
            ,1126177473954848
            ,18085351663075330
            ,283678299918408
            ,1729382259058873420
            ,18650191125102594
            ,9579117101058049
            ,229690316042684417
            ,218495572101825570
            ,48379053236224
            ,35201552025156
            ,24189797993728
            ,9007199290523662
            ,576462128974790721
            ,2691625939935520
            ,598983561407644424
            ,72075740811694081
        };

        public static void UpdateAllPossibleMovesFromAllSquares()
        {
            AllPossibleMovesFromAllSquares = new ulong[8, 8];
            string[,] boardInStringFormat =
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

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int row = maxRowNumber - i;
                    int column = j;

                    boardInStringFormat[row, column] = "WB";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringFormat);
                    Cell cell = board[i, j];
                    List<Move> moves = Bishop.GetMovesFromCache(board, cell);
                    ulong bishopMoves = 0;

                    foreach (Move move in moves)
                    {
                        int square = move.To.Row * 8 + move.To.Column;
                        bishopMoves = bishopMoves | one << square;
                    }

                    AllPossibleMovesFromAllSquares[i, j] = bishopMoves;
                    boardInStringFormat[row, column] = "  ";
                    MovesHelper.GetBinaryString(bishopMoves);
                }
            }
        }

        public static void UpdateAllPossibleMovesForAllBlockers()
        {
            BlockerMovesToBinaryMoves = new ulong[64, 1 << 14];
            BinaryToActualMoves = new List<Move>[64, 1 << 12];
            BishopBlockerMovesToBinaryMovesDictionary = new Dictionary<ulong, ulong>[64];
            AllBinaryMoves = new HashSet<ulong>[64];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;
                    BishopBlockerMovesToBinaryMovesDictionary[square] = new Dictionary<ulong, ulong>();
                    AllBinaryMoves[square] = new HashSet<ulong>();
                    ulong allBishopMoves = AllPossibleMovesFromAllSquares[i, j];
                    string[,] boardInStringArray = MovesHelper.GetBinaryToBoardInStringArray(allBishopMoves);
                    boardInStringArray[7 - i, j] = "WB";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringArray);
                    GenerateAllBlockers(allBishopMoves, 0, i, j, board);
                }
            }
        }

        public static void GenerateAllBlockers(ulong allBishopMoves, int index, int row, int column, Cell[,] board)
        {
            if (index > 63)
            {
                UpdateActualMoves(allBishopMoves, row, column, board);
                return;
            }
            int currentRow = (index / 8);
            int currentColumn = index % 8;

            GenerateAllBlockers(allBishopMoves, index + 1, row, column, board);

            if ((allBishopMoves & (one << index)) > 0)
            {
                Piece piece = board[currentRow, currentColumn].Piece;
                board[currentRow, currentColumn].Piece = null;
                GenerateAllBlockers(allBishopMoves & ~(one << index), index + 1, row, column, board);
                board[currentRow, currentColumn].Piece = piece;
            }
        }

        public static void UpdateActualMoves(ulong bishopBlockers, int row, int column, Cell[,] board)
        {
            int square = row * 8 + column;
            Cell cell = board[row, column];
            List<Move> moves = Bishop.GetMovesFromCache(board, cell);
            ulong binaryMoves = 0;

            foreach (Move move in moves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                binaryMoves = binaryMoves | one << currentSquare;
            }

            int index = (int)((binaryMoves * MagicNumbersForActualMoves[square]) >> (64 - 12));
            BinaryToActualMoves[square, index] = moves;
            AllBinaryMoves[square].Add(binaryMoves);

            List<Move> killerMoves = moves.Where(x => board[x.To.Row, x.To.Column].Piece != null).ToList();
            ulong killerBinaryRookMoves = 0;

            foreach (Move move in killerMoves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                killerBinaryRookMoves = killerBinaryRookMoves | one << currentSquare;
            }

            index = (int)((killerBinaryRookMoves * MagicNumbersForActualMoves[square]) >> (64 - 12));
            BinaryToActualMoves[square, index] = killerMoves;
            AllBinaryMoves[square].Add(killerBinaryRookMoves);
            
            int indexForBlocker = (int)((bishopBlockers * MagicNumbersForBlockers[square]) >> (64 - 14));

            BlockerMovesToBinaryMoves[square, indexForBlocker] = binaryMoves;
            BishopBlockerMovesToBinaryMovesDictionary[square][bishopBlockers] = binaryMoves;
        }

        public static void UpdateAllPossibleMovesForOwnBlockers()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int square = i * 8 + j;
                    AllBinaryMoves[square] = new HashSet<ulong>();
                    ulong allMoves = AllPossibleMovesFromAllSquares[i, j];
                    string[,] boardInStringArray = MovesHelper.GetBinaryToBoardInStringArray(allMoves, "WP");
                    boardInStringArray[7 - i, j] = "WB";
                    Cell[,] board = BoardHelper.GetBoard(boardInStringArray);
                    GenerateOwnBlockers(allMoves, 0, i, j, board);
                }
            }
        }

        public static void GenerateOwnBlockers(ulong allMoves, int index, int row, int column, Cell[,] board)
        {
            if (index > 63)
            {
                UpdateActualMovesForOwnBlockers(allMoves, row, column, board);
                return;
            }
            int currentRow = (index / 8);
            int currentColumn = index % 8;

            GenerateOwnBlockers(allMoves, index + 1, row, column, board);

            if ((allMoves & (one << index)) > 0)
            {
                Piece piece = board[currentRow, currentColumn].Piece;
                board[currentRow, currentColumn].Piece = null;
                GenerateOwnBlockers(allMoves & ~(one << index), index + 1, row, column, board);
                board[currentRow, currentColumn].Piece = piece;
            }
        }

        public static void UpdateActualMovesForOwnBlockers(ulong blockers, int row, int column, Cell[,] board)
        {
            int square = row * 8 + column;
            Cell cell = board[row, column];
            List<Move> moves = Bishop.GetMovesFromCache(board, cell);
            ulong binaryMoves = 0;

            foreach (Move move in moves)
            {
                int currentSquare = move.To.Row * 8 + move.To.Column;
                binaryMoves = binaryMoves | one << currentSquare;
            }

            int index = (int)((binaryMoves * MagicNumbersForActualMoves[square]) >> (64 - 12));
            BinaryToActualMoves[square, index] = moves;
            AllBinaryMoves[square].Add(binaryMoves);
        }
    }
}
