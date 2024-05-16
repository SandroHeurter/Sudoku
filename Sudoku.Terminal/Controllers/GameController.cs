using System.Text;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Terminal.Controllers
{
    public abstract class GameController<T> : Controller<T> where T : Controller<T>
    {
        protected GameController(App app) : base(app)
        {
        }

        public abstract IPrintVisitor Visitor(StringBuilder builder);
        public abstract void Switch();

        public void Move(Coordinate coordinate)
        {
            App.game.SelectSquare(coordinate);
        }

        public void EnterValue(string value)
        {
            App.game.EnterValue(value);
        }

        public virtual void ValidateSudoku()
        {
            App.game.ValidateSudoku();
        }

        public virtual void Solve()
        {
            App.game.Solve();
        }
        public Board GetBoard()
        {
            return App.game.Board;
        }
    }
}