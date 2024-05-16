using System.Collections.Generic;
using System.Text;
using Sudoku.Terminal.Controllers;
using Sudoku.Terminal.Models;

namespace Sudoku.Terminal.Views
{
    public abstract class View
    {
        private readonly Input[] inputs;

        public IEnumerable<Input> Inputs => (Input[])inputs.Clone();

        internal View(Input[] inputs)
        {
            this.inputs = inputs;
        }

        public abstract void Print(StringBuilder stringBuilder);
    }

    public abstract class View<T> : View where T : Controller<T>
    {
        protected readonly T Controller;

        protected View(T controller, params Input[] inputs) : base(inputs)
        {
            Controller = controller;
        }
    }
}