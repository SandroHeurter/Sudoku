using System.Linq;
using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;
using System;

namespace Sudoku.Domain.Visitors
{
    public class SamuraiSudokuVisitor : IVisitor
    {
        public Board Visit(BaseSudoku sudoku)
        {
            var samuraiSudoku = sudoku as SamuraiSudoku;
            var boardBuilder = new BoardBuilder();

            if (samuraiSudoku == null) return boardBuilder.GetResult();

            var squares = samuraiSudoku.GetSquares();

            for (int y = 0; y < 21; y++)
            {
                
                if (y >= 9 && y < 12)
                {
                    boardBuilder.BuildSpacer(12); 
                }

                for (int x = 0; x < 21; x++)
                {
                 
                    if ((y < 6 && x == 9) || (y >= 15 && y < 21 && x == 9))
                    {
                        boardBuilder.BuildSpacer(6); 
                    }

                    var square = squares.FirstOrDefault(s => s.Coordinate.X == x && s.Coordinate.Y == y);
                    if (square != null)
                    {
                        boardBuilder.BuildSquare(square);
                    }
                    else
                    {
                        boardBuilder.BuildSpacer(1);
                    }

                    if ((x + 1) % 3 == 0 && x < 20)
                    {
                        boardBuilder.BuildDivider(false);
                    }
                }

                boardBuilder.BuildRow();
            }

            return boardBuilder.GetResult();
        }
    }
}
