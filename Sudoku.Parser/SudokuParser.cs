using System.IO;
using Sudoku.Domain.Factories;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Parser.Parsers;

namespace Sudoku.Parser
{
    public class SudokuParser : ISudokuParser
    {
        private readonly SudokuFactory sudokuFactory;

        public SudokuParser()
        {
            sudokuFactory = new SudokuFactory();
            sudokuFactory.AddSudokuFactory("4x4", typeof(SudokuNormalFactory));
            sudokuFactory.AddSudokuFactory("6x6", typeof(SudokuNormalFactory));
            sudokuFactory.AddSudokuFactory("9x9", typeof(SudokuNormalFactory));
            sudokuFactory.AddSudokuFactory("jigsaw", typeof(SudokuJigsawFactory));
            sudokuFactory.AddSudokuFactory("samurai", typeof(SudokuSamuraiFactory));
        }

        public BaseSudoku Parse(string format, string path = "./Formats/")
        {
            if (format == "jigsaw")
            {
                var jigsawParser = new JigsawSudokuParser();
                return jigsawParser.Parse(format, path);
            }
            else if (format == "samurai")
            {
                var samuraiParser = new SamuraiSudokuParser();
                return samuraiParser.Parse(format, path);
            }

            var abstractSudokuFactory = this.sudokuFactory.CreateSudokuFactory(format);
            return abstractSudokuFactory?.CreateSudoku(File.ReadAllText(Path.Combine($"{path}puzzle.{format}")));
        }
    }
}
