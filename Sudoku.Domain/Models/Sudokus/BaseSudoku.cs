using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utils;

namespace Sudoku.Domain.Models.Sudokus
{
    public abstract class BaseSudoku
    {
        public List<IComponent> Components { get; }

        protected BaseSudoku(List<IComponent> sudokus)
        {
            Components = sudokus;
        }

        public virtual List<SquareLeaf> GetSquares()
        {
            return Components.SelectMany(box => box.Find(square => !square.Composite()))
                .Cast<SquareLeaf>()
                .OrderBy(square => square.Coordinate.Y)
                .ThenBy(square => square.Coordinate.X)
                .Distinct(new SquareComparer())
                .ToList();
        }

        public int MaxValue()
        {
            return Components.First()
                .GetChildren()
                .Max(box => box.GetChildren().Count());
        }

        public Board Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

        private IEnumerable<IComponent> GetSudokus()
        {
            return Components
                .Where(sudoku => sudoku.GetChildren().Count() > 2)
                .ToList();
        }

        public abstract IStrategy GetSolverStrategy();

        public virtual bool ValidateSudoku(State state, bool setValid = false)
        {
            GetSquares()
                .ForEach(square => square.IsValid = true);

            return GetSudokus()
                .All(sudoku => sudoku.Valid(state, setValid));
        }
    }
}