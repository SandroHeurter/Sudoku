using System.Linq;
using System.Text;
using Sudoku.Terminal.Views;
using Sudoku.Terminal.Views.Visitors;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;

namespace Sudoku.Terminal.Controllers
{
    public class DefinitiveController : GameController<DefinitiveController>
    {
        public DefinitiveController(App app) : base(app)
        {
            App.game.sudoku?.GetSquares().First().Select();
        }

        public override View<DefinitiveController> CreateView()
        {
            App.game.SwitchState(new DefinitiveState());
            return new GameView<DefinitiveController>(this);
        }

        public override void Update()
        {

        }

        public override IPrintVisitor Visitor(StringBuilder builder)
        {
            return new PrintDefinitiveVisitor(builder);
        }

        public override void Switch()
        {
            App.OpenController<HintController>();
        }
    }
}