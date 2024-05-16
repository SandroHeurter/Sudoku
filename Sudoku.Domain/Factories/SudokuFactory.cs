#nullable enable

using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Factories
{
    public class SudokuFactory
    {
        private readonly Dictionary<string, Type> sudokuFactories = new();

        public void AddSudokuFactory(string key, Type abstractSudokuFactory)
        {
            sudokuFactories.Add(key, abstractSudokuFactory);
        }

        public IAbstractSudokuFactory? CreateSudokuFactory(string type)
        {
            return (IAbstractSudokuFactory?)Activator.CreateInstance(sudokuFactories[type]);
        }
    }
}