using System;
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

            var nextHorizontal = (int)Math.Floor(Math.Sqrt(firstBox));
            var nextVertical = (int)Math.Ceiling(Math.Sqrt(firstBox));

            for (var i = 0; i < squares.Count; ++i)
            {
                var square = squares[i];
                var nextSquare = i + 1 > squares.Count - 1 ? null : squares[i + 1];
                var downLeaf = squares.FirstOrDefault(squareLeaf => squareLeaf.Coordinate.Y == square.Coordinate.Y + 1 && squareLeaf.Coordinate.X == square.Coordinate.X);
                var box = boxes.FirstOrDefault(q => q.GetChildren().Contains(square));

                boardBuilder.BuildSquare(square);

                if (nextSquare?.Coordinate.Y == square.Coordinate.Y &&
                    !box?.GetChildren().Contains(nextSquare!) == true)
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

                if ((square.Coordinate.Y + 1) % nextHorizontal != 0 || downLeaf == null)
                {
                    boardBuilder.BuildRow();
                    continue;
                }

                boardBuilder.BuildRow();
            }

            return boardBuilder.GetResult();
        }
    }
}
