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
            var mergedComponents = MergeBoards(boards);
            return new SamuraiSudoku(mergedComponents);
        }

        private SudokuComposite GenerateBoard(string data)
        {
            var size = 9; 
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

        private List<IComponent> MergeBoards(List<IComponent> boards)
        {
            var size = 21;
            var squares = new SquareLeaf[size, size];

            // Coordinates for the 5 boards
            var coordinates = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(12, 0),
                new Coordinate(0, 12),
                new Coordinate(12, 12),
                new Coordinate(6, 6)
            };

            for (int b = 0; b < 5; b++)
            {
                var board = boards[b];
                var offsetX = coordinates[b].X;
                var offsetY = coordinates[b].Y;

                var boardSquares = board.GetChildren().SelectMany(box => box.GetChildren()).Cast<SquareLeaf>().ToList();

                foreach (var square in boardSquares)
                {
                    var x = square.Coordinate.X + offsetX;
                    var y = square.Coordinate.Y + offsetY;
                    squares[y, x] = new SquareLeaf(square.Locked, square.Value, new Coordinate(x, y));
                }
            }

            var mergedBoxes = new List<IComponent>();
            for (int boxRow = 0; boxRow < size / 3; boxRow++)
            {
                for (int boxCol = 0; boxCol < size / 3; boxCol++)
                {
                    var boxSquares = new List<IComponent>();
                    for (int row = 0; row < 3; row++)
                    {
                        for (int col = 0; col < 3; col++)
                        {
                            var x = boxCol * 3 + col;
                            var y = boxRow * 3 + row;
                            if (squares[y, x] != null)
                            {
                                boxSquares.Add(squares[y, x]);
                            }
                        }
                    }
                    if (boxSquares.Count > 0)
                    {
                        mergedBoxes.Add(new BoxComposite(boxSquares));
                    }
                }
            }

            return mergedBoxes;
        }
    }
}
