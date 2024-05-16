using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Sudoku.Parser;
using Sudoku.Tests.TestData;

namespace Sudoku.Tests
{
    [TestFixture]
    public class FileReaderTests
    {
        private SudokuParser sudokuParser;
        private static string path = "../../../Formats/";
        private static List<string> formats = SudokuFormatData.Formats
            .Select(td => td.Format)
            .ToList();

        [OneTimeSetUp]
        public void Setup()
        {
            sudokuParser = new SudokuParser();
        }

        [Test, TestCaseSource(nameof(formats))]
        public void Parser_Can_Parse(string type)
        {
            var input = sudokuParser.Parse(type, path);

            Assert.NotNull(input);
        }
    }
}