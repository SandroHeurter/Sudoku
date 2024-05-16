using System;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Factories
{
    public class SudokuJigsawFactory : IAbstractSudokuFactory
    {
        public BaseSudoku CreateSudoku(string data)
        {
            throw new NotImplementedException();
        }
    }
}