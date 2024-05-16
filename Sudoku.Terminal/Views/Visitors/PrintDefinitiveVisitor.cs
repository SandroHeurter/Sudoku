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

            if (square.SquareLeaf.Locked && !square.SquareLeaf.Selected)
            {
                color = $"{Color.FromName("Orange").ToArgb():x6}";
            }
            else if (square.SquareLeaf.Selected)
            {
                color = $"{Color.FromName("MediumSlateBlue").ToArgb():x6}";
                text =
                    square.SquareLeaf.Value.Equals("0") ||
                    square.SquareLeaf.Value.Equals("")
                        ? " O "
                        : $" {square.SquareLeaf.Value} ";
            }

            color =
                square.SquareLeaf.Valid() ||
                square.SquareLeaf.Locked ||
                square.SquareLeaf.Selected
                    ? color : $"{Color.FromName("Crimson").ToArgb():x6}";
            ;

            stringBuilder.Append(text.Pastel(color));
        }
        public void Visit(Divider divider)
        {
            stringBuilder.Append(divider.Horizontal ? " - " : "|");
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
            var stringBuilder = new StringBuilder();
            var boards = sudoku.Components.SelectMany(c => c.GetChildren()).ToList();

            foreach (var board in boards)
            {
                var squares = board.GetChildren().Cast<SquareLeaf>().ToList();
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        var square = squares.FirstOrDefault(s => s.Coordinate.X == x && s.Coordinate.Y == y);
                        if (square != null)
                        {
                            var value = square.Value != "0" ? square.Value : " ";
                            var color = square.Locked ? Color.Orange : Color.White;
                            stringBuilder.Append(value.Pastel(color));
                        }
                        else
                        {
                            stringBuilder.Append(" ");
                        }
                    }
                    stringBuilder.AppendLine();
                }
                stringBuilder.AppendLine();
            }

            Console.WriteLine(stringBuilder.ToString());
        }

    }

}