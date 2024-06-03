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
                for (int x = 0; x < 21; x++)
                {
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

                if ((y + 1) % 3 == 0 && y < 20)
                {
                    boardBuilder.BuildDivider(true);
                }
            }

            return boardBuilder.GetResult();
        }
    }
}
