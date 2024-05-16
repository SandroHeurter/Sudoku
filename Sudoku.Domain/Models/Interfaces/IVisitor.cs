using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IVisitor
    {
        public Board Visit(BaseSudoku sudoku);
    }
}