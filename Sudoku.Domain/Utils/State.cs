#nullable enable

using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Utils;
using System.Collections.Generic;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Utils
{
    public abstract class State
    {
        protected IContext? Context;

        public void SetContext(IContext context)
        {
            Context = context;
        }

        public virtual void Select(Coordinate coordinate)
        {
            var orderedSquares = Context?.Sudoku()?.GetSquares();
            var currentSquare = orderedSquares?.FirstOrDefault(square => square.Selected);

            if (currentSquare == null || orderedSquares == null) return;

            var nextX = currentSquare.Coordinate.X + coordinate.X;
            var nextY = currentSquare.Coordinate.Y + coordinate.Y;

            var nextSquare = orderedSquares
                .FirstOrDefault(square =>
                    square.Coordinate.X == nextX &&
                    square.Coordinate.Y == nextY);

            if (nextSquare == null)
            {
                return;
            }

            currentSquare.UnSelect();
            nextSquare.Select();
        }

        public abstract void SetValue(string value, SquareLeaf square);
        public abstract Board? Construct();
        public abstract bool Check(SquareLeaf leftSquare, SquareLeaf rightSquare);
        public abstract bool HasSquareValue(SquareLeaf square);
    }
}
