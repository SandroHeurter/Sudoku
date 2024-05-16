using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Tests.TestData
{
    public static class SudokuData
    {
        private static readonly List<IComponent> boxes = new()
        {
            new BoxComposite(new List<IComponent>
            {
                new SquareLeaf(false, "0", new Coordinate(0, 0)), new SquareLeaf(false, "3", new Coordinate(1, 0)),
                new SquareLeaf(false, "4", new Coordinate(0, 1)), new SquareLeaf(false, "0", new Coordinate(1, 1))
            }),
            new BoxComposite(new List<IComponent>
            {
                new SquareLeaf(false, "4", new Coordinate(2, 0)), new SquareLeaf(false, "0", new Coordinate(3, 0)),
                new SquareLeaf(false, "0", new Coordinate(2, 1)), new SquareLeaf(false, "2", new Coordinate(3, 1))
            }),
            new BoxComposite(new List<IComponent>
            {
                new SquareLeaf(false, "1", new Coordinate(0, 2)), new SquareLeaf(false, "0", new Coordinate(1, 2)),
                new SquareLeaf(false, "0", new Coordinate(0, 3)), new SquareLeaf(false, "2", new Coordinate(1, 3))
            }),
            new BoxComposite(new List<IComponent>
            {
                new SquareLeaf(false, "0", new Coordinate(2, 2)), new SquareLeaf(false, "3", new Coordinate(3, 2)),
                new SquareLeaf(false, "1", new Coordinate(2, 3)), new SquareLeaf(false, "0", new Coordinate(3, 3))
            })
        };

        public static BaseSudoku BaseSudoku
        {
            get
            {
                var copies = new List<IComponent>();

                boxes.ForEach(box =>
                {
                    var squares = new List<IComponent>();
                    box
                        .GetChildren()
                        .Cast<SquareLeaf>()
                        .ToList()
                        .ForEach(square =>
                    {
                        squares.Add(new SquareLeaf(square.Locked, square.Value, square.Coordinate));
                    });

                    copies.Add(new BoxComposite(squares));
                });

                return new NormalSudoku(new List<IComponent> { new SudokuComposite(copies) });
            }
        }
    }
}