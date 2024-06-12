using System.Linq;
using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Visitors
{
    public class JigsawSudokuVisitor : IVisitor
    {
        public Board Visit(BaseSudoku sudoku)
        {
            var jigsawSudoku = sudoku as JigsawSudoku;
            var boardBuilder = new BoardBuilder();

            if (jigsawSudoku == null) return boardBuilder.GetResult();

            var squares = jigsawSudoku.GetSquares();
            var maxY = squares.Max(s => s.Coordinate.Y);
            var maxX = squares.Max(s => s.Coordinate.X);

            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
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

                    if ((x + 1) % 3 == 0 && x < maxX)
                    {
                        boardBuilder.BuildDivider(false);
                    }
                }
                boardBuilder.BuildRow();

                if ((y + 1) % 3 == 0 && y < maxY)
                {
                    boardBuilder.BuildDivider(true);
                    boardBuilder.BuildRow();
                }
            }
            return boardBuilder.GetResult();
        }
    }
}
