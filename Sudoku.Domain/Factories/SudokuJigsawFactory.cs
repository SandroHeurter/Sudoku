using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Sudokus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Domain.Factories
{
    public class SudokuJigsawFactory : IAbstractSudokuFactory
    {
        public BaseSudoku CreateSudoku(string sudokuData)
        {
            var size = 9; // For Jigsaw Sudoku
            var boardData = sudokuData.Split('=').Skip(1).Select(d => d.Split('J')).ToList();
            var boxes = GenerateBoxes(size, boardData);
            var sudoku = new SudokuComposite(boxes);

            return new JigsawSudoku(new List<IComponent> { sudoku });
        }

        private List<IComponent> GenerateBoxes(int size, List<string[]> boardData)
        {
            var boxes = new List<IComponent>[size];
            for (var i = 0; i < size; i++)
                boxes[i] = new List<IComponent>();

            var coordinates = Enumerable.Range(0, size * size)
                .Select(index => new Coordinate(index % size, index / size))
                .ToList();

            for (var i = 0; i < boardData.Count; i++)
            {
                var value = boardData[i][0];
                var boxIndex = int.Parse(boardData[i][1]);
                var coordinate = coordinates[i];
                var square = new SquareLeaf(value != "0", value, coordinate);

                boxes[boxIndex].Add(square);
            }

            return boxes.Select(b => new BoxComposite(b)).Cast<IComponent>().ToList();
        }
    }
}
