namespace Sudoku.Domain.Models.Interfaces
{
    public interface IBuilder
    {
        public void Reset();
        public void BuildRow();
        public void BuildDivider(bool horizontal);
        public void BuildSquare(SquareLeaf square);
    }
}