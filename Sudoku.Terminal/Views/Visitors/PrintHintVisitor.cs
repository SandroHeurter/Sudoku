using System.Drawing;
using System.Text;
using Pastel;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;

namespace Sudoku.Terminal.Views.Visitors
{
    public class PrintHintVisitor : IPrintVisitor
    {
        private StringBuilder stringBuilder;

        public PrintHintVisitor(StringBuilder stringBuilder)
        {
            this.stringBuilder = stringBuilder;
        }

        public void Visit(Square square)
        {
            var color = $"{Color.FromName("White").ToArgb():x6}";

            var value = square.SquareLeaf.Locked ||
                !square.SquareLeaf.Value.Equals("0")
                    ? square.SquareLeaf.Value
                    : square.SquareLeaf.HintValue;

            var content = value.Equals("0") || value.Equals("") ? "   " : $" {value} ";

            if (
                square.SquareLeaf.Locked &&
                !square.SquareLeaf.Selected ||
                !square.SquareLeaf.Value.Equals("0") &&
                !square.SquareLeaf.Selected
            )
            {
                color = $"{Color.FromName("Orange").ToArgb():x6}";
            }
            else if (square.SquareLeaf.Selected)
            {
                color = $"{Color.FromName("MediumSlateBlue").ToArgb():x6}";
                content = value.Equals("0") || value.Equals("") ? " H " : $" {value} ";
            }

            color =
                square.SquareLeaf.Valid() ||
                square.SquareLeaf.HintValue.Equals("0") ||
                square.SquareLeaf.Locked ||
                square.SquareLeaf.Selected
                    ? color
                    : $"{Color.FromName("Crimson").ToArgb():x6}";

            stringBuilder.Append(content.Pastel(color));
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
            stringBuilder.Append(new string(',', spacer.Size));
        }
    }
}