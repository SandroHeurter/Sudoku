#nullable enable

using Sudoku.Domain.Models;
using Sudoku.Domain.Utils;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.States
{
    public class DefinitiveState : State
    {
        public override void SetValue(string value, SquareLeaf square)
        {
            square.Value = value;
        }

        public override Board? Construct()
        {
            return Context?.Sudoku()?.Accept(new NormalSudokuVisitor());
        }

        public override bool Check(SquareLeaf leftSquare, SquareLeaf rightSquare)
        {
            return leftSquare.Value == rightSquare.Value;
        }

        public override bool HasSquareValue(SquareLeaf square)
        {
            return !square.Value.Equals("0");
        }
    }
}