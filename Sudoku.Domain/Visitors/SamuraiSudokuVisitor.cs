using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Domain.Visitors
{
    public class SamuraiSudokuVisitor : IVisitor
    {
        public Board Visit(BaseSudoku sudoku)
        {
            if (!(sudoku is SamuraiSudoku samuraiSudoku))
            {
                throw new ArgumentException("Deze visitor verwacht een SamuraiSudoku object.");
            }

            var boardBuilder = new BoardBuilder();

            // Assuming the samuraiSudoku.Components holds each of the five SudokuComposite sections.
            var compositeSudokus = samuraiSudoku.Components.OfType<SudokuComposite>().ToList();

            foreach (var composite in compositeSudokus)
            {
                var boxes = composite.GetChildren().SelectMany(box => box.GetChildren()).Cast<SquareLeaf>().ToList();
                foreach (var box in boxes)
                {
                    // Here, use the builder's method to build a square from a SquareLeaf
                    boardBuilder.BuildSquare(box);
                }

                // Assume that BoardBuilder has these methods to build rows and dividers
                boardBuilder.BuildRow(); // Add a new row for visual separation
            }

            // Finalize building the board and return it.
            return boardBuilder.GetResult();
        }
    }
}
