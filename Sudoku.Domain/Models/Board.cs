using System.Collections.Generic;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models
{
    public class Board
    {
        public List<ICharacter> boxes { get; } = new();

        public void Add(ICharacter part)
        {
            boxes.Add(part);
        }
  
    }
}