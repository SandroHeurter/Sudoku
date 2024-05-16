using System.Drawing;
using System.Text;
using Pastel;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;

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
    }
}