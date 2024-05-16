using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Utils;
using System;

namespace Sudoku.Domain.Factories
{
    public class SudokuNormalFactory : IAbstractSudokuFactory
    {
        public virtual BaseSudoku CreateSudoku(string sudokuData)
        {
            return CreateSudokuOfSize(sudokuData);
        }

        private BaseSudoku CreateSudokuOfSize(string sudokuData)
        {
            var size = Convert.ToInt32(Math.Round(Math.Sqrt(sudokuData.Length)));
            var boxes = GenerateBoxes(size, sudokuData);
            var sudoku = new SudokuComposite(boxes);

            return new NormalSudoku(new List<IComponent> { sudoku });
        }

        private List<IComponent> GenerateBoxes(int size, string sudokuData)
        {
            var boards = GenerateBoards(sudokuData, size).ToList();

            var boxWidth = Convert.ToInt32(Math.Ceiling(Math.Sqrt(size)));
            var boxHeight = Convert.ToInt32(Math.Floor(Math.Sqrt(size)));

            var boxes = new List<IComponent>();

            var currentBox = 0;
            var maxWidth = boxWidth;
            var maxHeight = boxHeight;

            for (var i = 0; i < size; i++)
            {
                var minX = maxWidth - boxWidth;
                var minY = maxHeight - boxHeight;

                var leaves = new List<IComponent>();
                leaves.AddRange(GetBoxBoards(boards, new Coordinate(minX, minY), new Coordinate(maxWidth, maxHeight)));

                boxes.Add(new BoxComposite(leaves));

                currentBox++;

                if (currentBox == boxHeight)
                {
                    maxWidth = boxWidth;
                    maxHeight += boxHeight;
                    currentBox = 0;
                    continue;
                }

                maxWidth += boxWidth;
            }

            return boxes;
        }

        private IEnumerable<SquareLeaf> GenerateBoards(string sudokuData, int size)
        {
            var boards = new List<SquareLeaf>();
            var output = GetStringChunks(sudokuData, 1).ToList();
            var dataPointCount = sudokuData.Length;

            for (var y = 0; y < size; ++y)
            {
                for (var x = 0; x < size; ++x)
                {
                    var content = output[sudokuData.Length - dataPointCount--];
                    boards.Add(new SquareLeaf(content != "0", content, new Coordinate(x, y)));
                }
            }

            return boards;
        }

        protected virtual IEnumerable<SquareLeaf> GetBoxBoards(IEnumerable<SquareLeaf> squares, Coordinate minCoordinate, Coordinate maxCoordinate)
        {
            return squares.Where(square => square.Coordinate.X >= minCoordinate.X)
                .Where(square => square.Coordinate.X < maxCoordinate.X)
                .Where(square => square.Coordinate.Y >= minCoordinate.Y)
                .Where(square => square.Coordinate.Y < maxCoordinate.Y)
                .ToList();
        }

        private IEnumerable<string> GetStringChunks(string data, int chunkSize)
        {
            for (var i = 0; i < data.Length; i += chunkSize) yield return data.Substring(i, chunkSize);
        }
    }
}