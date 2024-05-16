using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utils;

namespace Sudoku.Domain.Models
{
    public class BoxComposite : IComponent
    {
        public List<IComponent> Children { get; }

        public BoxComposite(List<IComponent> children)
        {
            Children = children;
        }

        public bool Valid(State state, bool setValid)
        {
            var children = Find(child => !child.Composite()).Cast<SquareLeaf>();
            var squareLeaves = children as SquareLeaf[] ?? children.ToArray();

            var doubles = squareLeaves
                .GroupBy(square => square.Value)
                .Where(square => square.Count() > 1 && !square.Key.Equals("0"))
                .Select(square => square.Key);

            return doubles.Any();
        }

        public bool Composite()
        {
            return true;
        }

        public IEnumerable<IComponent> Find(Func<IComponent, bool> search)
        {
            return GetChildren()
            .Descendants(descendant => descendant.GetChildren())
            .Where(search);
        }

        public IEnumerable<IComponent> GetChildren()
        {
            return Children;
        }
    }
}