using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Parser
{
    public interface ISudokuParser
    {
        public BaseSudoku Parse(string format, string path = "./Formats/");
    }
}