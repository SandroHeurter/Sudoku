using Sudoku.Domain.Models.Parts;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models;

namespace Sudoku.Domain.Factories
{
    public class SudokuSamuraiFactory : IAbstractSudokuFactory
    {
        public BaseSudoku CreateSudoku(string data)
        {
            var gridsData = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (gridsData.Count != 5)
            {
                throw new InvalidOperationException("Er wordt verwacht dat er vijf grids zijn voor een Samurai Sudoku.");
            }

            for (int i = 0; i < gridsData.Count; i++)
            {
                // Het derde grid (met index 2) zal een andere lengte hebben
                if (i != 2 && gridsData[i].Length != 81)
                {
                    throw new InvalidOperationException("Elk buitenste Sudoku grid moet 81 karakters lang zijn.");
                }
            }

            var sudokuGrids = gridsData.Select(CreateSudokuGrid).ToList();
            SynchronizeOverlappingSquares(sudokuGrids);
            return new SamuraiSudoku(sudokuGrids.Cast<IComponent>().ToList());
        }




        private SudokuComposite CreateSudokuGrid(string gridData)
        {
            if (gridData.Length != 81) // 9x9 sudoku
            {
                throw new InvalidOperationException("Elk Sudoku grid moet 81 karakters lang zijn.");
            }

            var squares = new List<SquareLeaf>();

            for (int i = 0; i < gridData.Length; i++)
            {
                int x = i % 9;
                int y = i / 9;
                bool isFixed = gridData[i] != '0' && gridData[i] != '.';
                squares.Add(new SquareLeaf(isFixed, gridData[i].ToString(), new Coordinate(x, y)));
            }

            return new SudokuComposite(squares.Cast<IComponent>().ToList());
        }

        private void SynchronizeOverlappingSquares(List<SudokuComposite> sudokuGrids)
        {
            int centerIndex = 4;  // De index voor de centrale Sudoku in de lijst.
            List<Coordinate> centerOverlapAreas = new List<Coordinate>
    {
        new Coordinate(3, 3), new Coordinate(4, 3), new Coordinate(5, 3),
        new Coordinate(3, 4), new Coordinate(4, 4), new Coordinate(5, 4),
        new Coordinate(3, 5), new Coordinate(4, 5), new Coordinate(5, 5)
    };

            for (int i = 0; i < centerIndex; i++)
            {
                foreach (var coord in centerOverlapAreas)
                {
                    Coordinate cornerCoord = TransformToCornerCoordinate(coord, i);

                    var centerSquare = sudokuGrids[centerIndex].Boxes
                        .SelectMany(box => box.GetChildren())
                        .Cast<SquareLeaf>()
                        .FirstOrDefault(square => square.Coordinate.Equals(coord));

                    if (centerSquare == null) continue;

                    var cornerSquare = sudokuGrids[i].Boxes
                        .SelectMany(box => box.GetChildren())
                        .Cast<SquareLeaf>()
                        .FirstOrDefault(square => square.Coordinate.Equals(cornerCoord));

                    if (cornerSquare != null)
                    {
                        // Synchroniseer de waarde van de hoekvakje met die van het centrale vakje.
                        cornerSquare.Value = centerSquare.Value;
                    }
                }
            }
        }


        private Coordinate TransformToCornerCoordinate(Coordinate centerCoord, int cornerIndex)
        {
            int offsetX = 0;
            int offsetY = 0;

            switch (cornerIndex)
            {
                case 0:
                    offsetX = offsetY = 0;
                    break;
                case 1:
                    offsetX = 6;
                    offsetY = 0;
                    break;
                case 2:
                    offsetX = 0;
                    offsetY = 6;
                    break;
                case 3:
                    offsetX = offsetY = 6;
                    break;
            }

            return new Coordinate(centerCoord.X - 3 + offsetX, centerCoord.Y - 3 + offsetY);
        }
    }
}
