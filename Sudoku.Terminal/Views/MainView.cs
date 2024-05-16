using System;
using System.Text;
using Sudoku.Terminal.Controllers;
using Sudoku.Terminal.Models;

namespace Sudoku.Terminal.Views
{
    public class MainView : View<MainController>
    {
        public MainView(MainController controller) : base(
            controller,
            new Input((int)ConsoleKey.Enter, controller.ChooseFormat),
            new Input((int)ConsoleKey.UpArrow, () => controller.MoveSelection(-1)),
            new Input((int)ConsoleKey.DownArrow, () => controller.MoveSelection(1))
        )
        {
        }

        public override void Print(StringBuilder stringBuilder)
        {
            stringBuilder.Append("Select a format:");
            stringBuilder.AppendLine();

            foreach (var (format, selected) in Controller.GetFormats())
            {
                stringBuilder.Append($"{(selected ? ">" : " ")} {format}");
                stringBuilder.AppendLine();
            }
        }
    }
}