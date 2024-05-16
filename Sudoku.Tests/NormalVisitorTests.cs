using System.Linq;
using NUnit.Framework;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Parts;
using Sudoku.Domain.Visitors;
using Sudoku.Tests.TestData;

namespace Sudoku.Tests
{
    [TestFixture]
    public class NormalVisitorTests
    {
        private BaseSudoku sudoku;

        [SetUp]
        public void Setup()
        {
            sudoku = SudokuData.BaseSudoku;
        }

        [Test]
        public void Sudoku_Has_Correct_Squares()
        {
            var visitor = new NormalSudokuVisitor();

            var field = visitor.Visit(sudoku);
            var squares = field.boxes.Where(p => p is Square).ToList();

            Assert.AreEqual(31, field.boxes.Count);
            Assert.AreEqual(16, squares.Count);
        }
    }
}