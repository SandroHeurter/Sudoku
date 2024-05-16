using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Factories
{
    public interface IAbstractSudokuFactory
    {
        public BaseSudoku CreateSudoku(string data);
    }
}