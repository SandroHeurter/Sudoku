using System;
using System.Drawing;
using System.Linq;
using System.Text;
using Pastel;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Terminal.Views.Visitors
{
    public class PrintDefinitiveVisitor : IPrintVisitor
    {
        private StringBuilder stringBuilder;

        public PrintDefinitiveVisitor(StringBuilder stringBuilder)
        {
            this.stringBuilder = stringBuilder;
        }

        public void Visit(Square square)
        {
            var color = $"{Color.FromName("White").ToArgb():x6}";

            var text =
                square.SquareLeaf.Value.Equals("0") ||
                square.SquareLeaf.Value.Equals("")
                    ? "   "
                    : $" {square.SquareLeaf.Value} ";

            if (square.SquareLeaf.Selected)
            {
                color = $"{Color.FromName("MediumSlateBlue").ToArgb():x6}";
                text =
                    square.SquareLeaf.Value.Equals("0") ||
                    square.SquareLeaf.Value.Equals("")
                        ? " O "
                        : $" {square.SquareLeaf.Value} ";
            }
            else if (square.SquareLeaf.Locked)
            {
                color = $"{Color.FromName("Orange").ToArgb():x6}";
            }

            color =
                square.SquareLeaf.Valid() ||
                square.SquareLeaf.Locked ||
                square.SquareLeaf.Selected
                    ? color : $"{Color.FromName("Crimson").ToArgb():x6}";

            stringBuilder.Append(text.Pastel(color));
        }

        public void Visit(Divider divider)
        {
            stringBuilder.Append(divider.Horizontal ? "---" : "|");
        }

        public void Visit(Row row)
        {
            stringBuilder.AppendLine();
        }

        public void Visit(Spacer spacer)
        {
            stringBuilder.Append(new string(' ', spacer.Size));
        }

        public void Visit(SamuraiSudoku sudoku)
        {
            var boards = sudoku.Components.Cast<SudokuComposite>().ToList();
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
                        stringBuilder.Append("   "); // Use three spaces for an empty cell
                    }
                    else
                    {
                        var square = allSquares.FirstOrDefault(s =>
                            s.Coordinate.X + startX[boardIndex] == x &&
                            s.Coordinate.Y + startY[boardIndex] == y);

                        if (square != null)
                        {
                            Visit(new Square(square));
                        }
                        else
                        {
                            stringBuilder.Append("   "); // Use three spaces for an empty cell
                        }
                    }

                    if ((x + 1) % 3 == 0 && x < maxX - 1)
                    {
                        stringBuilder.Append("|");
                    }
                }

                stringBuilder.AppendLine();

                if ((y + 1) % 3 == 0 && y < maxY - 1)
                {
                    if (ShouldAddHorizontalDivider(y))
                    {
                        stringBuilder.AppendLine(new string('-', maxX * 3 + (maxX / 3 - 1))); // Adjusted for divider size
                    }
                    else
                    {
                        stringBuilder.AppendLine(new string(' ', maxX * 3 + (maxX / 3 - 1))); // Adjusted for spacer size
                    }
                }

                // Add extra spaces for alignment
                if (y == 8 || y == 11)
                {
                    stringBuilder.AppendLine(new string(' ', 36));
                }
            }
        }

        private int GetBoardIndex(int x, int y)
        {
            if (x < 9 && y < 9) return 0;
            if (x >= 12 && y < 9) return 1;
            if (x < 9 && y >= 12) return 2;
            if (x >= 12 && y >= 12) return 3;
            if (x >= 6 && x < 15 && y >= 6 && y < 15) return 4;
            return -1;
        }

        private bool ShouldAddHorizontalDivider(int y)
        {
            // Only add horizontal dividers where needed, excluding specific rows
            return (y + 1) % 3 == 0 && y != 8 && y != 11 && y != 17 && y != 20;
        }
    }
}
