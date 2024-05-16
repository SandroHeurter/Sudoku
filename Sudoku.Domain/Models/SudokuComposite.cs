using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utils;

namespace Sudoku.Domain.Models
{
    public class SudokuComposite : IComponent
    {
        // Toevoegen van een eigenschap voor Boxes
        public List<IComponent> Boxes { get; private set; }

        // Constructor die de lijst van IComponent (Boxes) initialiseert
        public SudokuComposite(List<IComponent> boxes)
        {
            Boxes = boxes ?? new List<IComponent>(); // Voorkom null door een lege lijst te geven als boxes null is
        }

        public bool Valid(State state, bool setValid)
        {
            var squares = GetChildren().SelectMany(square => square.GetChildren()).Cast<SquareLeaf>().ToList();

            foreach (var square in squares.Where(square => !square.Locked && state.HasSquareValue(square)))
            {
                var rowColumn = squares
                    .Where(childSquare => (childSquare.Coordinate.Y == square.Coordinate.Y || childSquare.Coordinate.X == square.Coordinate.X) && childSquare != square)
                    .FirstOrDefault(childSquare => state.Check(childSquare, square));

                if (rowColumn != null)
                {
                    if (setValid) square.IsValid = false;
                    else return false;
                }

                var box = Find(box => box.GetChildren().Contains(square)).FirstOrDefault();

                if (box != null && box.GetChildren().Cast<SquareLeaf>()
                    .Any(childSquare => state.Check(childSquare, square) && childSquare != square))
                {
                    if (setValid) square.IsValid = false;
                    else return false;
                }
            }

            return true;
        }

        public bool Composite()
        {
            return true;
        }

        public IEnumerable<IComponent> Find(Func<IComponent, bool> search)
        {
            // Gebruik Descendants om te zoeken in geneste componenten
            return Boxes.Descendants(component => component.GetChildren()).Where(search);
        }

        public IEnumerable<IComponent> GetChildren()
        {
            // Teruggeven van de Boxes lijst
            return Boxes;
        }
    }
}
