using System.Linq;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utils;

namespace Sudoku.Domain.Strategies
{
    public class BackTrackStrategy : IStrategy
    {
        public bool Solve(BaseSudoku sudoku, State state)
        {
            var square = sudoku
                .GetSquares()
                .FirstOrDefault(square =>
                   square.Value.Equals("0") && !square.Locked
                );

            if (square == null)
            {
                return true;
            }

            var max = sudoku.MaxValue();

            for (var i = 1; i <= max; ++i)
            {
                square.Value = $"{i}";

                if (sudoku.ValidateSudoku(state) && Solve(sudoku, state))
                {
                    return true;
                }

                square.Value = "0";
            }

            return false;
        }
    }
}