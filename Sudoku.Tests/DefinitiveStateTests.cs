using Moq;
using NUnit.Framework;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;
using Sudoku.Tests.TestData;

namespace Sudoku.Tests
{
    [TestFixture]
    public class DefinitiveStateTests
    {
        private SquareLeaf originalSquare;
        private BaseSudoku sudoku;

        [SetUp]
        public void Setup()
        {
            sudoku = SudokuData.BaseSudoku;
            originalSquare = new SquareLeaf(false, "9", new Coordinate(0, 0));
        }

        [Test]
        public void Square_Set_Value()
        {
            var state = new DefinitiveState();

            state.SetValue("9", originalSquare);

            Assert.AreEqual("9", originalSquare.Value);
        }

        [Test]
        public void Square_Has_Value()
        {
            var state = new DefinitiveState();

            var result = state.HasSquareValue(originalSquare);

            Assert.IsTrue(result);
        }

        [Test]
        public void Square_Has_No_Value()
        {

            var state = new DefinitiveState();

            originalSquare.Value = "0";

            var result = state.HasSquareValue(originalSquare);

            Assert.IsFalse(result);
        }

        [Test]
        public void Square_Check_Equal()
        {
            var state = new DefinitiveState();
            var square = new SquareLeaf(false, "9", new Coordinate(0, 0));

            var result = state.Check(originalSquare, square);

            Assert.IsTrue(result);
        }

        [Test]
        public void Square_Check_Different()
        {
            var state = new DefinitiveState();
            var square = new SquareLeaf(false, "8", new Coordinate(0, 0));

            var result = state.Check(originalSquare, square);

            Assert.IsFalse(result);
        }
    }
}