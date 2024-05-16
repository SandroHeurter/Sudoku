using System.Linq;
using NUnit.Framework;
using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Parts;

namespace Sudoku.Tests
{
    [TestFixture]
    public class BoardBuilderTests
    {
        private BoardBuilder boardBuilder;

        private static object[] squares =
        {
            new object[] { new SquareLeaf(true, "1", new Coordinate(0,0)) },
            new object[] { new SquareLeaf(false, "0", new Coordinate(0, 0)) },
            new object[] { new SquareLeaf(true, new Coordinate(0,0)) }
        };

        [SetUp]
        public void SetUp()
        {
            boardBuilder = new BoardBuilder();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Build_Vertical_Divider(bool horizontal)
        {
            boardBuilder.BuildDivider(horizontal);

            var result = boardBuilder.GetResult();

            Assert.That(result.boxes.Count(p => p.GetType() == typeof(Divider)), Is.EqualTo(1));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Build_Horizontal_Divider(bool horizontal)
        {
            boardBuilder.BuildDivider(horizontal);

            var product = boardBuilder.GetResult();
            var divider = product.boxes.Cast<Divider>().First();

            Assert.AreEqual(divider.Horizontal, horizontal);
        }


        [TestCase(1)]
        [TestCase(2)]
        public void Build_Spacer(int size)
        {
            boardBuilder.BuildSpacer(size);

            var result = boardBuilder.GetResult();
            var spacer = result.boxes.Cast<Spacer>().First();

            Assert.AreEqual(spacer.Size, size);
        }

        [TestCaseSource(nameof(squares))]
        public void Build_Empty_Square(SquareLeaf square)
        {
            boardBuilder.BuildSquare(square);

            var result = boardBuilder.GetResult();

            Assert.That(result.boxes.Count(p => p.GetType() == typeof(Square)), Is.EqualTo(1));
        }

        [TestCaseSource(nameof(squares))]
        public void Build_Square(SquareLeaf square)
        {
            boardBuilder.BuildSquare(square);

            var result = boardBuilder.GetResult();
            var builtSquare = result.boxes.Cast<Square>().First();

            Assert.AreEqual(square.Value, builtSquare.SquareLeaf.Value);
            Assert.AreEqual(square.Locked, builtSquare.SquareLeaf.Locked);
            Assert.AreEqual(square.Coordinate.X, builtSquare.SquareLeaf.Coordinate.X);
            Assert.AreEqual(square.Coordinate.Y, builtSquare.SquareLeaf.Coordinate.Y);
        }

        [Test]
        public void Build_Row()
        {
            boardBuilder.BuildRow();

            var result = boardBuilder.GetResult();

            Assert.That(result.boxes.Count(p => p.GetType() == typeof(Row)), Is.EqualTo(1));
        }
    }
}