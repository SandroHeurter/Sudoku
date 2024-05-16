using Sudoku.Terminal.Views;

namespace Sudoku.Terminal.Controllers
{
    public abstract class Controller
    {
        protected readonly App App;

        internal Controller(App app)
        {
            App = app;
        }

        public abstract void Update();
    }

    public abstract class Controller<T> : Controller where T : Controller<T>
    {
        protected Controller(App app) : base(app)
        {
        }

        public abstract View<T> CreateView();
    }
}