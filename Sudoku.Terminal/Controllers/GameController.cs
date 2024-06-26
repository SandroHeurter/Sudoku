using System.Linq;
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

        public virtual void EnterValue(string value)
        {
            var selectedSquare = App.game.sudoku?.GetSquares().FirstOrDefault(square => square.Selected);
            if (selectedSquare != null)
            {
                App.game.EnterValue(value, selectedSquare);
            }
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
