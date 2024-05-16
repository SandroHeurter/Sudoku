using System;
using System.Collections.Generic;
using Sudoku.Domain.Utils;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IComponent
    {
        public bool Composite();
        public IEnumerable<IComponent> GetChildren();
        public bool Valid(State state, bool setValid);
        public IEnumerable<IComponent> Find(Func<IComponent, bool> search);
    }
}