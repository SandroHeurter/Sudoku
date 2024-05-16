#nullable enable

using Sudoku.Domain.Utils;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IContext
    {
        public State? GetState();
        public Board ConstructBoard();
        public BaseSudoku? Sudoku();
        public IStrategy? GetStrategy();
        public void SwitchState(State newState);
        public void SetSudoku(BaseSudoku? sudoku);
        public void SetStrategy(IStrategy newStrategy);
        public void EnterValue(string value, SquareLeaf square);
    }
}