using Sudoku.Domain.Models.Parts;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IPrintVisitor
    {
        public void Visit(Row row);
        public void Visit(Square square);
        public void Visit(Spacer spacer);
        public void Visit(Divider divider);
    }
}