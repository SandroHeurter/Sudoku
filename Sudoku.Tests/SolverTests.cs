using System.Linq;
using NUnit.Framework;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.States;
using Sudoku.Domain.Strategies;
using Sudoku.Tests.TestData;

namespace Sudoku.Tests
{
    [TestFixture]
    public class SolverTests
    {
        private BaseSudoku sudoku;

        [SetUp]
        public void Setup()
        {
            sudoku = SudokuData.BaseSudoku;
        }

        [Test]
        public void Solve_Sudoku()
        {
            var strategy = new BackTrackStrategy();

            var success = strategy.Solve(sudoku, new DefinitiveState());
            var count = sudoku.Components
                .SelectMany(box => box.Find(square => !square.Composite()))
                .Cast<SquareLeaf>()
                .Count(square => square.Value.Equals("0"));

            Assert.AreEqual(0, count);
            Assert.IsTrue(success);
        }



        [Test]
        public void Solve_Invalid_Sudoku()
        {
            var strategy = new BackTrackStrategy();

            sudoku.Components
                .SelectMany(box => box.Find(square => !square.Composite()))
                .Cast<SquareLeaf>().First().Value = "4";

            var success = strategy.Solve(sudoku, new DefinitiveState());

            Assert.IsFalse(success);
        }
    }
}