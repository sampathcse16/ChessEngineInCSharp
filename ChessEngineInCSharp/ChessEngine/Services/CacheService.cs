using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessEngine;

namespace UI.Services
{
    public class CacheService
    {
        public static Move[] AllPossibleMoves { get; set; }

        public void InitializeAllPossibleMovesFromEachCellOnBoard()
        {
            if (AllPossibleMoves != null)
            {
                return;
            }

            int fromCounter = 0;
            Move[] allMoves = new Move[6500];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++, fromCounter++)
                {
                    int toCounter = 0;

                    for (int k = 0; k < 8; k++)
                    {
                        for (int l = 0; l < 8; l++, toCounter++)
                        {
                            if (i == k && j == l)
                            {
                                continue;
                            }

                            int moveId = fromCounter * 100 + toCounter;
                            allMoves[moveId] = new Move
                            {
                                From = new Position { Row = i, Column = j },
                                To = new Position { Row = k, Column = l }
                            };

                            
                        }
                    }
                }
            }

            AllPossibleMoves = allMoves;
        }
    }
}
