using System.IO;
using Sudoku.Domain.Factories;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Parser.Parsers
{
    public class JigsawSudokuParser : ISudokuParser
    {
        private readonly SudokuFactory sudokuFactory;

        public JigsawSudokuParser()
        {
            sudokuFactory = new SudokuFactory();
            sudokuFactory.AddSudokuFactory("jigsaw", typeof(SudokuJigsawFactory));
        }

        public BaseSudoku Parse(string format, string path = "./Formats/")
        {
            var abstractSudokuFactory = this.sudokuFactory.CreateSudokuFactory(format);
            var data = File.ReadAllText(Path.Combine($"{path}puzzle.{format}")).Replace("SumoCueV1", "").Trim();
            return abstractSudokuFactory?.CreateSudoku(data);
        }
    }
}
