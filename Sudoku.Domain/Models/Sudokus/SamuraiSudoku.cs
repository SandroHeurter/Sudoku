using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Strategies;
using System.Collections.Generic;

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
