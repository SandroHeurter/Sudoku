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
            int[] startX = { 0, 12, 0, 12, 6 };
            int[] startY = { 0, 0, 12, 12, 6 };

            var maxX = startX.Max() + 9;
            var maxY = startY.Max() + 9;

            var allSquares = boards.SelectMany(board => board.GetChildren()).SelectMany(box => box.GetChildren()).Cast<SquareLeaf>().ToList();

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var boardIndex = GetBoardIndex(x, y);
                    if (boardIndex == -1)
                    {
                        boardBuilder.BuildSpacer(1);
                    }
                    else
                    {
                        var square = allSquares.FirstOrDefault(s =>
                            s.Coordinate.X + startX[boardIndex] == x &&
                            s.Coordinate.Y + startY[boardIndex] == y);

                        if (square != null)
                        {
                            boardBuilder.BuildSquare(square);
                        }
                        else
                        {
                            boardBuilder.BuildSpacer(1);
                        }
                    }

                    if ((x + 1) % 3 == 0 && x < maxX - 1)
                    {
                        boardBuilder.BuildDivider(false);
                    }
                }

                boardBuilder.BuildRow();

                if ((y + 1) % 3 == 0 && y < maxY - 1)
                {
                    boardBuilder.BuildDivider(true);
                }
            }

            return boardBuilder.GetResult();
        }

        private int GetBoardIndex(int x, int y)
        {
            if (x < 9 && y < 9) return 0;         // Top-left grid
            if (x >= 12 && y < 9) return 1;       // Top-right grid
            if (x < 9 && y >= 12) return 2;       // Bottom-left grid
            if (x >= 12 && y >= 12) return 3;     // Bottom-right grid
            if (x >= 6 && x < 15 && y >= 6 && y < 15) return 4; // Center grid
            return -1;
        }
    }
}
