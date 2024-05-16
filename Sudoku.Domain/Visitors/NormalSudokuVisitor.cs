using System.Linq;
using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;
using System;

namespace Sudoku.Domain.Visitors
{
    public class NormalSudokuVisitor : IVisitor
    {
        public Board Visit(BaseSudoku sudoku)
        {
            var squares = sudoku.GetSquares();
            var boardBuilder = new BoardBuilder();

            var boxes = sudoku.Components
                .SelectMany(box => box.Find(subBox => subBox.Composite()))
                .ToList();

            var totalWidth = squares.Max(squareLeaf => squareLeaf.Coordinate.X) + 1;

            var firstBox = sudoku.Components
                .Find(box => box.Composite())
                .GetChildren()
                .Count();

            var nextHorizontal = Convert.ToInt32(Math.Floor(Math.Sqrt(firstBox)));
            var nextVertical = Convert.ToInt32(Math.Ceiling(Math.Sqrt(firstBox)));

            for (var i = 0; i < squares.Count; ++i)
            {
                var square = squares[i];
                var nextSquare = i + 1 > squares.Count - 1 ? null : squares[i + 1];
                var downLeaf = squares.FirstOrDefault(squareLeaf => squareLeaf.Coordinate.Y == square.Coordinate.Y + 1 && squareLeaf.Coordinate.X == square.Coordinate.X);
                var box = boxes.First(q => q.GetChildren().Contains(square));

                boardBuilder.BuildSquare(square);

                if (
                    nextSquare?.Coordinate.Y == square.Coordinate.Y &&
                    !box.GetChildren().Contains(nextSquare!))
                {
                    if (square.IsEmpty() && nextSquare.IsEmpty())
                    {
                        boardBuilder.BuildSpacer(1);
                    }
                    else
                    {
                        boardBuilder.BuildDivider(false);
                    }
                }

                if (nextSquare?.Coordinate.Y == square.Coordinate.Y)
                {
                    continue;
                }

                boardBuilder.BuildRow();

                if ((square.Coordinate.Y + 1) % nextHorizontal != 0 || downLeaf == null)
                {
                    continue;
                }

                var currentSquares = squares
                    .Where(squareLeaf => squareLeaf.Coordinate.Y == square.Coordinate.Y)
                    .ToList();

                var nextSquares =
                    squares
                        .Where(squareLeaf => squareLeaf.Coordinate.Y == square.Coordinate.Y + 1)
                        .ToList();

                for (var wall = 0; wall < totalWidth; ++wall)
                {
                    if (!currentSquares[wall].IsEmpty() && !nextSquares[wall].IsEmpty())
                    {
                        boardBuilder.BuildDivider(true);
                    }
                    else
                    {
                        boardBuilder.BuildSpacer(3);
                    }

                    if ((wall + 1) % nextVertical == 0)
                    {
                        boardBuilder.BuildSpacer(1);
                    }
                }

                boardBuilder.BuildRow();
            }

            return boardBuilder.GetResult();
        }
    }
}