#nullable enable

using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Utils;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.States
{
    public class HintState : State
    {
        public override void SetValue(string value, SquareLeaf square)
        {
            square.HintValue = value;
        }

        public override Board? Construct()
        {
            if (Context?.Sudoku() is SamuraiSudoku)
            {
                return Context?.Sudoku()?.Accept(new SamuraiSudokuVisitor());
            }
            return Context?.Sudoku()?.Accept(new NormalSudokuVisitor());
        }

        public override bool Check(SquareLeaf leftSquare, SquareLeaf rightSquare)
        {
            return HasSquareValue(leftSquare) && HasSquareValue(rightSquare) &&
                leftSquare.HintValue == rightSquare.HintValue ||
                !leftSquare.Value.Equals("0") && leftSquare.Value == rightSquare.HintValue ||
                !rightSquare.Value.Equals("0") && rightSquare.Value == leftSquare.HintValue;
        }

        public override bool HasSquareValue(SquareLeaf square)
        {
            return !square.HintValue.Equals("0");
        }
    }
}
