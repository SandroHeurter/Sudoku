using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Sudokus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Domain.Factories
{
    public class SudokuSamuraiFactory : IAbstractSudokuFactory
    {
        public BaseSudoku CreateSudoku(string sudokuData)
        {
            var boardsData = sudokuData.Split(',');
            var boards = boardsData.Select(data => GenerateBoard(data)).Cast<IComponent>().ToList();
            return new SamuraiSudoku(boards);
        }

        private SudokuComposite GenerateBoard(string data)
        {
            var size = 9; // Elke Individuele Samurai Sudoku is een 9x9 grid
            var squares = new List<SquareLeaf>();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var index = i * size + j;
                    var value = data[index].ToString();
                    var coordinate = new Coordinate(j, i);
                    squares.Add(new SquareLeaf(value != "0", value, coordinate));
                }
            }

            var boxes = new List<IComponent>();
            for (int boxRow = 0; boxRow < 3; boxRow++)
            {
                for (int boxCol = 0; boxCol < 3; boxCol++)
                {
                    var boxSquares = new List<IComponent>();
                    for (int row = 0; row < 3; row++)
                    {
                        for (int col = 0; col < 3; col++)
                        {
                            var x = boxCol * 3 + col;
                            var y = boxRow * 3 + row;
                            boxSquares.Add(squares[y * size + x]);
                        }
                    }
                    boxes.Add(new BoxComposite(boxSquares));
                }
            }

            return new SudokuComposite(boxes);
        }
    }
}
