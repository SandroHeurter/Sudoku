using System.IO;
using Sudoku.Domain.Factories;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Parser.Parsers
{
    public class SamuraiSudokuParser : ISudokuParser
    {
        private readonly SudokuFactory sudokuFactory;

        public SamuraiSudokuParser()
        {
            sudokuFactory = new SudokuFactory();
            sudokuFactory.AddSudokuFactory("samurai", typeof(SudokuSamuraiFactory));
        }

        public BaseSudoku Parse(string format, string path = "./Formats/")
        {
            var abstractSudokuFactory = this.sudokuFactory.CreateSudokuFactory(format);
            var data = File.ReadAllLines(Path.Combine($"{path}puzzle.{format}"));
            return abstractSudokuFactory?.CreateSudoku(string.Join(",", data));
        }
    }
}
