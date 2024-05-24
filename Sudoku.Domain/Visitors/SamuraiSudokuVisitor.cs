using System.Linq;
using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;
using System;

namespace Sudoku.Domain.Visitors
{
    public class SamuraiSudokuVisitor : IVisitor
    {
        public Board Visit(BaseSudoku sudoku)
        {
            var samuraiSudoku = sudoku as SamuraiSudoku;
            var boardBuilder = new BoardBuilder();

            if (samuraiSudoku == null) return boardBuilder.GetResult();

            var boards = samuraiSudoku.Components.Cast<SudokuComposite>().ToList();
            int[] startX = { 0, 18, 0, 18, 9 };
            int[] startY = { 0, 0, 18, 18, 9 };

            for (int i = 0; i < 5; i++)
            {
                var squares = boards[i].GetChildren().SelectMany(c => c.GetChildren()).Cast<SquareLeaf>().ToList();
                int offsetX = startX[i];
                int offsetY = startY[i];

                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        var square = squares.FirstOrDefault(s => s.Coordinate.X == x && s.Coordinate.Y == y);
                        if (square != null)
                        {
                            boardBuilder.BuildSquare(square);
                        }
                        else
                        {
                            boardBuilder.BuildSpacer(1);
                        }
                    }
                    boardBuilder.BuildRow();
                }
                boardBuilder.BuildSpacer(2); 
            }

            return boardBuilder.GetResult();
        }
    }
}
