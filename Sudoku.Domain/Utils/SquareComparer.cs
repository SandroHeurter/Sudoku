#nullable enable

using System;
using System.Collections.Generic;
using Sudoku.Domain.Models;

namespace Sudoku.Domain.Utils
{
    public class SquareComparer : IEqualityComparer<SquareLeaf>
    {
        public bool Equals(SquareLeaf? x, SquareLeaf? y)
        {
            return x!.Coordinate.Y == y!.Coordinate.Y && x!.Coordinate.X == y!.Coordinate.X;
        }

        public int GetHashCode(SquareLeaf square)
        {
            return HashCode.Combine(square.Locked, square.Selected, square.Coordinate);
        }
    }
}