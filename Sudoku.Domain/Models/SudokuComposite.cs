using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utils;

namespace Sudoku.Domain.Models
{
    public class SudokuComposite : IComponent
    {
        public List<IComponent> Boxes { get; }

        public SudokuComposite(List<IComponent> boxes)
        {
            Boxes = boxes;
        }

        public bool Valid(State state, bool setValid)
        {
            var squares = GetChildren().SelectMany(square => square.GetChildren()).Cast<SquareLeaf>().ToList();

            foreach (var square in squares.Where(square => !square.Locked && state.HasSquareValue(square)))
            {
                var rowColumn = squares.Where(childSquare => (childSquare.Coordinate.Y == square.Coordinate.Y || childSquare.Coordinate.X == square.Coordinate.X) && childSquare != square)
                    .FirstOrDefault(childSquare => state.Check(childSquare, square));

                if (rowColumn != null)
                {
                    if (setValid) square.IsValid = false;
                    else return false;
                }

                var box = Find(box => box.GetChildren().Contains(square)).First();

                if (box.GetChildren().Cast<SquareLeaf>()
                    .FirstOrDefault(childSquare =>
                        state.Check(childSquare, square) && childSquare != square) == null) continue;

                if (setValid) square.IsValid = false;
                else return false;
            }

            return true;
        }

        public bool Composite()
        {
            return true;
        }

        public IEnumerable<IComponent> Find(Func<IComponent, bool> search)
        {
            return GetChildren().Descendants(component => component.GetChildren()).Where(search);
        }

        public IEnumerable<IComponent> GetChildren()
        {
            return Boxes;
        }
    }
}