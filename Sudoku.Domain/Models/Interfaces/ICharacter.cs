using System.Text;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface ICharacter
    {
        public void Accept(IPrintVisitor visitor);
    }
}