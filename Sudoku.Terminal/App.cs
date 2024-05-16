using System;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Terminal.Controllers;
using Sudoku.Terminal.Views;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Parser;

namespace Sudoku.Terminal
{
    public class App : IObserver<IGame>
    {
        public readonly ISudokuParser parser;
        public IGame game;
        private IDisposable unSubscriber;
        private View view;
        private Controller controller;
        private bool running = true;

        public App(IGame game, ISudokuParser parser)
        {
            this.game = game;
            this.parser = parser;
        }

        public async Task Run(string[] args)
        {
            Console.Title = "DP1 - Sudoku";
            Console.OutputEncoding = Encoding.UTF8;

            OpenController<MainController>();

            unSubscriber = game.Subscribe(this);

            Input();

            await Task.CompletedTask;
        }

        public void OpenController<T>() where T : Controller<T>
        {
            var controller = (T)Activator.CreateInstance(typeof(T), this);

            this.controller = controller;
            view = controller?.CreateView();
            OnNext(game);
        }

        private void Input()
        {
            while (running)
            {
                var key = Console.ReadKey(true);

                foreach (var input in view.Inputs)
                {
                    if (input.Character != (int)key.Key) continue;
                    input.Action();
                }
            }
        }

        public void OnNext(IGame value)
        {
            game = value;

            Console.Clear();

            controller.Update();

            var output = new StringBuilder();

            view.Print(output);

            Console.CursorVisible = false;

            Console.SetCursorPosition(0, 0)
            ;
            Console.Write(output.ToString());
        }
        public void Exit()
        {
            OnCompleted();
            running = false;
            Environment.Exit(1);
        }
        public void OnCompleted()
        {
            unSubscriber.Dispose();
        }

        public void OnError(Exception error)
        {
            throw error;
        }
    }
}