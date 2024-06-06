using System.Linq;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utils;
using System.Collections.Generic;
using Sudoku.Domain.Strategies;

namespace Sudoku.Domain.Models.Sudokus
{
    public class SamuraiSudoku : BaseSudoku
    {
        public SamuraiSudoku(List<IComponent> sudokus) : base(sudokus)
        {
        }

        public override IStrategy GetSolverStrategy()
        {
            return new BackTrackStrategy();
        }

        public override bool ValidateSudoku(State state, bool setValid = false)
        {
            var grids = new List<List<SquareLeaf>>
            {
                GetGridSquares(0, 0),
                GetGridSquares(12, 0),
                GetGridSquares(0, 12),
                GetGridSquares(12, 12),
                GetGridSquares(6, 6)
            };

            bool isValid = true;

            foreach (var grid in grids)
            {
                var squares = grid.OrderBy(s => s.Coordinate.Y).ThenBy(s => s.Coordinate.X).ToList();
                isValid &= ValidateGrid(squares, state, setValid);
            }

            return isValid;
        }

        private List<SquareLeaf> GetGridSquares(int startX, int startY)
        {
            var squares = new List<SquareLeaf>();

            for (int y = startY; y < startY + 9; y++)
            {
                for (int x = startX; x < startX + 9; x++)
                {
                    var square = Components
                        .SelectMany(c => c.GetChildren())
                        .OfType<SquareLeaf>()
                        .FirstOrDefault(s => s.Coordinate.X == x && s.Coordinate.Y == y);

                    if (square != null)
                    {
                        squares.Add(square);
                    }
                }
            }

            return squares;
        }

        private bool ValidateGrid(List<SquareLeaf> squares, State state, bool setValid)
        {
            foreach (var square in squares)
            {
                if (!square.Locked && state.HasSquareValue(square))
                {
                    var sameRow = squares.Where(s => s.Coordinate.Y == square.Coordinate.Y && s != square);
                    var sameColumn = squares.Where(s => s.Coordinate.X == square.Coordinate.X && s != square);
                    var sameBox = squares.Where(s =>
                        s.Coordinate.X / 3 == square.Coordinate.X / 3 &&
                        s.Coordinate.Y / 3 == square.Coordinate.Y / 3 &&
                        s != square);

                    if (sameRow.Any(s => s.Value == square.Value) ||
                        sameColumn.Any(s => s.Value == square.Value) ||
                        sameBox.Any(s => s.Value == square.Value))
                    {
                        if (setValid)
                        {
                            square.IsValid = false;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
