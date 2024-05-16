using System.Collections.Generic;
using Sudoku.Domain.Models.Interfaces;
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
    }
}