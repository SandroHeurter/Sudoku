using System;
using System.Text;
using Sudoku.Terminal.Controllers;
using Sudoku.Terminal.Models;
using Sudoku.Domain.Models;

namespace Sudoku.Terminal.Views
{
    public class GameView<T> : View<T> where T : GameController<T>
    {
        public GameView(T controller) : base(controller,
            new Input((int)ConsoleKey.D1, () => controller.EnterValue("1")),
            new Input((int)ConsoleKey.D2, () => controller.EnterValue("2")),
            new Input((int)ConsoleKey.D3, () => controller.EnterValue("3")),
            new Input((int)ConsoleKey.D4, () => controller.EnterValue("4")),
            new Input((int)ConsoleKey.D5, () => controller.EnterValue("5")),
            new Input((int)ConsoleKey.D6, () => controller.EnterValue("6")),
            new Input((int)ConsoleKey.D7, () => controller.EnterValue("7")),
            new Input((int)ConsoleKey.D8, () => controller.EnterValue("8")),
            new Input((int)ConsoleKey.D9, () => controller.EnterValue("9")),
            new Input((int)ConsoleKey.D0, () => controller.EnterValue("0")),
            new Input((int)ConsoleKey.LeftArrow, () => controller.Move(new Coordinate(-1, 0))),
            new Input((int)ConsoleKey.UpArrow, () => controller.Move(new Coordinate(0, -1))),
            new Input((int)ConsoleKey.RightArrow, () => controller.Move(new Coordinate(1, 0))),
            new Input((int)ConsoleKey.DownArrow, () => controller.Move(new Coordinate(0, 1))),
            new Input((int)ConsoleKey.M, controller.Switch),
            new Input((int)ConsoleKey.C, controller.ValidateSudoku),
            new Input((int)ConsoleKey.S, controller.Solve)
        )
        {
        }

        public override void Print(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("Controls:");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("M -> Switch between modes.");
            stringBuilder.AppendLine("C -> Validate the sudoku.");
            stringBuilder.AppendLine("S -> Solve the sudoku.");
            stringBuilder.AppendLine();

            var visitor = Controller.Visitor(stringBuilder);
            var boxes = Controller.GetBoard().boxes;

            foreach (var box in boxes)
            {
                box.Accept(visitor);
            }
        }
    }
}