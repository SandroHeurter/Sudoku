using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;

namespace Sudoku.Domain.Builders
{
    public class BoardBuilder : IBuilder
    {
        private Board board = new();

        public BoardBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            board = new Board();
        }

        public void BuildDivider(bool horizontal)
        {
            board.Add(new Divider(horizontal));
        }

        public void BuildSpacer(int size)
        {
            board.Add(new Spacer(size));
        }

        public void BuildSquare(SquareLeaf square)
        {
            board.Add(new Square(square));
        }

        public void BuildRow()
        {
            board.Add(new Row());
        }

        public Board GetResult()
        {
            var result = board;
            Reset();
            return result;
        }
    }
}